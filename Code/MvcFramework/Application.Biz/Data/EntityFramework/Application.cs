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
    public partial class Application : IEntityIdentity 
    {
       	[DebuggerStepThrough]
    	public Application()
        {
            this.Roles = new HashSet<Role>();
            this.Users = new HashSet<User>();
        }
    
    
    	[Required]
        public int ApplicationID { get; set; }
    
    	[Required]
    	[StringLength(100)]
        public string Name { get; set; }
    
    	
        /// <summary>
        /// Read only access to the primary key - used for Upserts
        /// </summary>
    	public int PrimaryKeyValue
        {
            get
            {
                return ApplicationID;
            }
        }
    
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}