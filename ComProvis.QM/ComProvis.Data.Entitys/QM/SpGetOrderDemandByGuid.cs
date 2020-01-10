
namespace ComProvis.Data.Entitys.QM
{
    public class SpGetOrderDemandByGuid
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
