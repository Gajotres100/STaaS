using ComProvis.Actions.Proxy.STaaSPlatformApi;

namespace ComProvis.Actions.Actions.STaaSProxy
{
    public interface ISTaaSSoap
    {
        ISTaaSPlatformApi STaaSPlatformApi { get; set; }

        void DiskSpaceCreate(string folderName);

        void RenamePhysicalFolder(string folderName);
    }
}