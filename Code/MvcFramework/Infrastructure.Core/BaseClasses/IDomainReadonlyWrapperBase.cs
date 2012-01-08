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
    public interface IDomainReadonlyWrapperBase
    {
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        bool NoTrackingEnabled { get; set; }
        DbContextConfiguration Configuration { get; }
    }
}
