//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CPV_Mark3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
            this.AspNetRoles = new HashSet<AspNetRole>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public Nullable<bool> EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> PhoneNumberConfirmed { get; set; }
        public Nullable<bool> TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public Nullable<bool> LockoutEnabled { get; set; }
        public Nullable<int> AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string UserRole { get; set; }
        public byte[] Images { get; set; }
        public string EmpCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
