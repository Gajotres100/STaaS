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
    
    public partial class SpGetOrderDemandByGuid
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public int CompanyId { get; set; }
        public int ProvisioningTypeId { get; set; }
        public int OrderDemandStateId { get; set; }
        public int OrderDemandTypeId { get; set; }
        public string JsonData { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
