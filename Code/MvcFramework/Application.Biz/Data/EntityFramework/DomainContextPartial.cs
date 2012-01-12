using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Threading;
using Application.Core;
using Application.Core.BaseClasses;
using Application.Core.EventBroker.Interfaces;
using Application.Core.General;
using Ninject;

namespace Application.Biz.Data.EntityFramework
{
    public partial class DomainContext
    {
        private IEventBroker _eventBroker;

        // Allows connectionString to be set (work around R# EAP issue)
        public DomainContext(string connectionString)
            : base(connectionString)
        {
            QueryableExtensions.Includer = new DbIncluder();
        }

        public IEventBroker EventBroker
        {
            get
            {
                return this._eventBroker;
            }
        }

        protected ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)this).ObjectContext;
            }
        }

        public void Add<T>(T entity) where T : class, IEntityIdentity
        {
            this.Set<T>().Add(entity);
        }

        /// <summary>
        ///   WARNING: Caution calling this directly. You likely want to call SaveChangesWithHandling() in GeneralRepository
        ///   which handles and bubbles validation and exceptions.
        ///   Saves all changes made in this context to the underlying database.
        ///   If present, Create/Update date and user are set if needed.
        /// </summary>
        /// <returns>
        ///   The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref = "T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public override int SaveChanges()
        {
            foreach (var entity in this.ChangeTracker.Entries())
            {
                if (entity.Entity is IEntityGuidToken)
                {
                    // It should never be valid to save a default Guid.
                    ((IEntityGuidToken)entity.Entity).Token.ThrowIfDefault("An attempt was made to save an entity with an invalid Token (default)");
                }

                if (entity.State == EntityState.Added)
                {
                    var hasUpdateDate = entity.Entity as IEntityCreateUpdateDate;
                    if (hasUpdateDate != null)
                    {
                        hasUpdateDate.CreateDate = DateTime.Now;
                    }
                    var hasUpdateUser = entity.Entity as IEntityCreateUpdateUser;
                    if (hasUpdateUser != null)
                    {
                        hasUpdateUser.CreateUserId = this.EventBroker.DatabaseCreateUpdateDeleteUserId;
                    }
                }
            }

            foreach (var entity in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Modified))
            {
                var hasUpdateDate = entity.Entity as IEntityCreateUpdateDate;
                if (hasUpdateDate != null)
                {
                    hasUpdateDate.UpdateDate = DateTime.Now.AddDays(1);
                }
                var hasUpdateUser = entity.Entity as IEntityCreateUpdateUser;
                if (hasUpdateUser != null)
                {
                    hasUpdateUser.UpdateUserId = this.EventBroker.DatabaseCreateUpdateDeleteUserId;
                }
            }

            // Audit: Using ObjectContext
            var modifiedEntities = this.ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
            foreach (var entry in modifiedEntities)
            {
                var modifiedProps = entry.GetModifiedProperties();
                foreach (var propName in modifiedProps)
                {
                    var originalValue = entry.OriginalValues[propName];
                    var currentValue = entry.CurrentValues[propName];

                    if (!this.ShouldIgnoreProperty(propName) && this.IsPropertyChanged(originalValue, currentValue))
                    {
                        // log the data
                        var auditLog = new AuditLog();
                        auditLog.Operation = entry.State.ToString();
                        auditLog.EntityName = entry.EntityKey.EntitySetName;
                        auditLog.CreatedDateTimeUTC = DateTime.Now.ToUniversalTime();
                        auditLog.UserId = this.EventBroker.DatabaseCreateUpdateDeleteUserId;

                        auditLog.PropertyName = propName;

                        auditLog.CurrentValue = Convert.ToString(currentValue).TruncateWithEllipses(1000);
                        auditLog.OriginalValue = Convert.ToString(originalValue).TruncateWithEllipses(1000);

                        this.SetEntityPKs(entry.EntityKey, auditLog);

                        this.AuditLogs.Add(auditLog);
                    }
                }
            }

            return base.SaveChanges();
        }

        /// <summary>
        ///   Ninject will take care of setting this.  
        ///   When Testing, set using BrokerFactoryConcrete or BrokerFactoryMock from BaseTest
        /// </summary>
        [Inject]
        public void SetEventBrokerFactory(IBrokerFactory brokerFactory)
        {
            this._eventBroker = brokerFactory.GetGeneralBroker();
        }

        public void SoftDelete<T>(long id) where T : class, IEntityIdentity, IEntitySoftDelete
        {
            var entity = this.Set<T>().Find(id);

            entity.IsDeleted = true;
        }

        public void SoftDelete<T>(Guid token) where T : class, IEntityGuidToken, IEntitySoftDelete
        {
            var entity = this.Set<T>().SingleOrDefault(x => x.Token == token);

            entity.IsDeleted = true;
        }

        public void SoftDelete<T>(T entity) where T : class, IEntityIdentity, IEntitySoftDelete
        {
            entity.IsDeleted = true;
        }

        /// <summary>
        ///   Inserts or Updates depending on if the PK is default.
        ///   Operates within Unit of Work pattern.
        /// </summary>
        /// <note>
        ///   The entity must be fully fetched if updating. Do not use this method when only partially updating entities.
        /// </note>
        public void Upsert<T>(T entity) where T : class, IEntityIdentity
        {
            if (this.Entry(entity).State == EntityState.Detached)
            {
                this.Set<T>().Attach(entity);
                this.Entry(entity).State = entity.PrimaryKeyValue == 0 ? EntityState.Added : EntityState.Modified;
            }
        }

        private bool IsPropertyChanged(object originalValue, object currentValue)
        {
            var same = false;
            var currentValueComparable = currentValue as IComparable;
            var originalValueComparable = originalValue as IComparable;

            if (currentValueComparable == null && originalValueComparable == null)
            {
                //same = true;
            }
            else if (currentValueComparable != null)
            {
                same = currentValueComparable.CompareTo(originalValueComparable) == 0;
            }

            else if (originalValueComparable != null)
            {
                same = originalValueComparable.CompareTo(currentValueComparable) == 0;
            }

            return !same;
        }

        private void SetEntityPKs(EntityKey entityKey, AuditLog auditLog)
        {
            if (entityKey.IsTemporary)
            {
                return;
            }

            var pkValuesArray = entityKey.EntityKeyValues.Select(s => s.Value).ToArray();
            auditLog.EntityPKValues = string.Join(",", pkValuesArray);

            var pkNamesArray = entityKey.EntityKeyValues.Select(s => s.Key).ToArray();
            auditLog.EntityPKNames = string.Join(",", pkNamesArray);
        }

        private bool ShouldIgnoreProperty(string property)
        {
            return property == "UpdateUserId" || property == "UpdateDate";
        }
    }
}