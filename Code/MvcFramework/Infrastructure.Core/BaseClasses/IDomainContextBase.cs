using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Infrastructure.Core.EventBroker.Interfaces;
using Ninject;

namespace Infrastructure.Core.BaseClasses
{
    public interface IDomainContextBase
    {
        DbContextConfiguration Configuration { get; }

        IEventBroker EventBroker { get; }

        void Add<T>(T entity) where T : class, IEntityIdentity;

        void Dispose();

        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        bool Equals(object obj);

        int GetHashCode();

        Type GetType();

        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        int SaveChanges();

        DbSet Set(Type entityType);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        ///   Ninject will take care of setting this.  
        ///   When Testing, set using BrokerFactoryConcrete or BrokerFactoryMock from BaseTest
        /// </summary>
        [Inject]
        void SetEventBrokerFactory(IBrokerFactory brokerFactory);

        void SoftDelete<T>(long id) where T : class, IEntityIdentity, IEntitySoftDelete;

        void SoftDelete<T>(T entity) where T : class, IEntityIdentity, IEntitySoftDelete;

        void SoftDelete<T>(Guid token) where T : class, IEntityGuidToken, IEntitySoftDelete;

        string ToString();

        void Upsert<T>(T entity) where T : class, IEntityIdentity;
    }
}