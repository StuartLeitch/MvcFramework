using System;
using System.Linq;

namespace Infrastructure.Core.BaseClasses
{
    public interface IEntityIdentity
    {
        int PrimaryKeyValue { get;}
    }
    
    public interface IEntityCreateUpdateDate
    {
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
    
    public interface IEntityCreateUpdateUser
    {
        int CreateUserId { get; set; }
        int? UpdateUserId { get; set; }
    }

    public interface IEntitySoftDelete
    {
        bool IsDeleted { get; set; }
    }

    public interface IEntityGuidToken
    {
        Guid Token { get; set; }
    }

}
