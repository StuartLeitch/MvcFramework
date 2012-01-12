using System;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using Application.Core.EventBroker;
using Application.Core.EventBroker.Interfaces;
using Common.Logging;

namespace Application.Core.BaseClasses
{
    public abstract class BaseGeneralRepository : IUseEventBroker
    {
        ILog _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Only needed during testing - DomainContext will have brokerFactory injected by Ninject
        /// </summary>
        public void SetEventBrokerFactory(IBrokerFactory brokerFactory)
        {
            this.DomainContext.SetEventBrokerFactory(brokerFactory);
        }

        public IEventBroker EventBroker
        {
            get
            {
                return this.DomainContext.EventBroker;
            }
        }

        /// <summary>
        ///   This must be set by the Command Repository in the Biz layer.
        /// </summary>
        protected IDomainContextBase DomainContext { private get; set; }

        /// <summary>
        ///   If needed, but consider using DbContext if possible.
        /// </summary>
        protected ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)this.DomainContext).ObjectContext;
            }
        }

        public void Add<T>(T entity) where T : class, IEntityIdentity
        {
            this.DomainContext.Add(entity);
        }

        public bool SaveChangesWithHandling()
        {
            var result = false;

            if (!this.ValidateAndPropogateFailures())
            {
                return false;
            }

            try
            {
                this.DomainContext.SaveChanges();
                result = true;
            }
            catch (DbUpdateConcurrencyException) {
                this.AddFailure(
                        new ValidationFailure(
                                "The record you are attempting to save has been modified by another user.  Please refresh and try again."));
            }
            catch (Exception ex)
            {
                this._log.Debug("Error during database save", ex);
                this.AddFailure(new ValidationFailure(ex));
            }

            return result;
        }

        /// <summary>
        ///   Validates and adds failures to the EventBroker
        /// </summary>
        /// <returns>True if no failures</returns>
        public bool ValidateAndPropogateFailures()
        {
            var validationErrors = this.DomainContext.GetValidationErrors().ToList();
            foreach (var dbEntityValidationResult in validationErrors.SelectMany(x => x.ValidationErrors))
            {
                this.AddFailure(new ValidationFailure(dbEntityValidationResult.ErrorMessage, dbEntityValidationResult.PropertyName));
            }

            return !validationErrors.Any();
        }

        /// <summary>
        ///   Inserts or Updates depending on if the PK is default.
        ///   Operates within Unit of Work pattern.
        /// </summary>
        /// <note>
        ///   Use for DETACHED entities only!
        ///   The entity must be fully fetched if updating. Do not use this method when only partially updating entities.
        /// </note>
        public void Upsert<T>(T entity) where T : class, IEntityIdentity
        {
            this.DomainContext.Upsert(entity);
        }

        public void SoftDelete<T>(int id) where T : class, IEntityIdentity, IEntitySoftDelete
        {
            this.DomainContext.SoftDelete<T>(id);
        }

        public void SoftDelete<T>(Guid token) where T : class, IEntityGuidToken, IEntitySoftDelete
        {
            this.DomainContext.SoftDelete<T>(token);
        }

        public void SoftDelete<T>(T entity) where T : class, IEntityIdentity, IEntitySoftDelete
        {
            this.DomainContext.SoftDelete<T>(entity);
        }

        // IMPROVE:  Turn this into a sample.
        //private static void UpdateJustSelectFieldsWithoutRoundTrip()
        //{
        //    using (var db = new StoreContext())
        //    {
        //        var product = new Product { ProductId = 1, Name = "Banana Modified" };
        //        db.Products.Attach(product);
        //        db.Entry(product).State = EntityState.Modified;
        //        db.Entry(product).Property(x => x.Name).IsModified = true;
        //        db.SaveChanges();
        //    }
        //    // Single round trip.
        //}
    }
}