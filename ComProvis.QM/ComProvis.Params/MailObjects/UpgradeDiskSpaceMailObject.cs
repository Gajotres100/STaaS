namespace ComProvis.Params.MailObjects
{
    public class UpgradeDiskSpaceMailObject : MailObjectBase
    {
        public string DiskSpaceName { get; set; }        
        public string DiskSpacePlan { get; set; }
    }
}
