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
    using System.Collections.Generic;
    
    public partial class DiskSpace
    {
        public int DiskSpaceID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public byte DiskSpaceStateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.Guid Identifier { get; set; }
        public int OwnerID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string AsetGroupID { get; set; }
    }
}
