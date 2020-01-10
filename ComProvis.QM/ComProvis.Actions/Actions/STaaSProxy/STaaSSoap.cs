using ComProvis.Actions.Proxy.STaaSPlatformApi;
using Microsoft.Extensions.Configuration;
using System;
using System.ServiceModel;

namespace ComProvis.Actions.Actions.STaaSProxy
{
    public class STaaSSoap : ISTaaSSoap
    {
        readonly BasicHttpBinding _binding = new BasicHttpBinding();

        private ISTaaSPlatformApi _sTaaSPlatformApi;

        public ISTaaSPlatformApi STaaSPlatformApi
        {
            get
            {
                if (_sTaaSPlatformApi == null)
                    _sTaaSPlatformApi = new STaaSPlatformApiClient();
                return _sTaaSPlatformApi;
            }
            set { _sTaaSPlatformApi = value; }
        }


        private readonly IConfiguration _configuration;
        public STaaSSoap(IConfiguration configuration)
        {
            _configuration = configuration;

            var address = new EndpointAddress(_configuration["STaaSPlatformApi"]);
            _binding.OpenTimeout = new TimeSpan(0, 60, 0);
            _binding.CloseTimeout = new TimeSpan(0, 60, 0);
            _binding.SendTimeout = new TimeSpan(0, 60, 0);
            _binding.ReceiveTimeout = new TimeSpan(0, 60, 0);
            _sTaaSPlatformApi = new STaaSPlatformApiClient(_binding, address);

        }

        public void DiskSpaceCreate(string folderName)
        {
            if (!_sTaaSPlatformApi.DiskSpaceCreate(folderName))
                throw new Exception(string.Format("Error creating folder \"{0}\" ", folderName));
        }

        public void RenamePhysicalFolder(string folderName)
        {
            if (!_sTaaSPlatformApi.DiskSpaceMarkDeleted(folderName))
                throw new Exception(string.Format("Error renameing folder \"{0}\" ", folderName));
        }

    }
}
