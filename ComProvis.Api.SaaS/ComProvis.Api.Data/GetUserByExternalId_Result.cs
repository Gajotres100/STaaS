//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComProvis.Api.Data
{
    using System;
    
    public partial class GetUserByExternalId_Result
    {
        public int UserId { get; set; }
        public string ExternalId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Username { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public Nullable<bool> IsSuspended { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> LastChangeDate { get; set; }
    }
}
