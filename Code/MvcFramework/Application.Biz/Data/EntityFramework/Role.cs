//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Application.Core.BaseClasses;

namespace Application.Biz.Data.EntityFramework
{
    public partial class Role : IEntityIdentity 
    {
       	[DebuggerStepThrough]
    	public Role()
        {
            this.Users = new HashSet<User>();
        }
    
    
    	[Required]
        public int RoleID { get; set; }
    
    	[Required]
    	[StringLength(256)]
        public string RoleName { get; set; }
    
    	[StringLength(256)]
        public string Description { get; set; }
    
    	[Required]
        public int ApplicationID { get; set; }
    
    	
        /// <summary>
        /// Read only access to the primary key - used for Upserts
        /// </summary>
    	public int PrimaryKeyValue
        {
            get
            {
                return RoleID;
            }
        }
    
        public virtual Application Application { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
