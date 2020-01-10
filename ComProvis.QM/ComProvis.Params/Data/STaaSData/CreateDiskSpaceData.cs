namespace ComProvis.Params.Data.STaaSData
{
    public class CreateDiskSpaceData : DiskBase
    {
        public string DiskName { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public string AssetGroupId { get; set; }
    }
}
