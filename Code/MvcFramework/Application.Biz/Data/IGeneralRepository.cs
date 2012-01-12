using System;
using System.Linq;
using Application.Core.BaseClasses;
using Application.Core.EventBroker.Interfaces;
using Application.Biz.Data.EntityFramework;

namespace Application.Biz.Data
{
    public interface IGeneralRepository
    {
        IDomainContext Db { get; }

        IEventBroker EventBroker { get; }

        void Add<T>(T entity) where T : class, IEntityIdentity;

        bool SaveChangesWithHandling();

        /// <summary>
        ///   Only needed during testing - DomainContext will have brokerFactory injected by Ninject
        /// </summary>
        void SetEventBrokerFactory(IBrokerFactory brokerFactory);

        void SoftDelete<T>(int id) where T : class, IEntityIdentity, IEntitySoftDelete;

        void SoftDelete<T>(T entity) where T : class, IEntityIdentity, IEntitySoftDelete;

        void SoftDelete<T>(Guid token) where T : class, IEntityGuidToken, IEntitySoftDelete;

        void Upsert<T>(T entity) where T : class, IEntityIdentity;

        /// <summary>
        ///   Validates and
        /// </summary>
        /// <returns></returns>
        bool ValidateAndPropogateFailures();
    }
}