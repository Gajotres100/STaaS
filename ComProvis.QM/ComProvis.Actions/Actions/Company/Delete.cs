using ComProvis.Actions.Actions.STaaSProxy;
using ComProvis.Actions.Helper;
using ComProvis.Data.Companys;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Disks;
using ComProvis.Data.Log;
using ComProvis.Enums;
using ComProvis.Params;
using Newtonsoft.Json;
using System;

namespace ComProvis.Actions.SaaS.Company
{
    [Provision(ProvisionType.DeleteCustomer)]
    public class Delete : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        ICompanyRepository _CompanyRepository;
        IDiskSpaceRepository _diskSpaceRepository;
        ISTaaSSoap _sTaaSSoap;

        public Delete(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, ICompanyRepository companyRepository, IDiskSpaceRepository diskSpaceRepository, ISTaaSSoap sTaaSSoap) : base(logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
            _CompanyRepository = companyRepository;
            _diskSpaceRepository = diskSpaceRepository;
            _sTaaSSoap = sTaaSSoap;
        }

        public override void DoJob(dynamic data)
        {
            var operationGuid = string.Empty;

            try
            {
                DeleteCompanyData json = JsonConvert.DeserializeObject<DeleteCompanyData>(data);
                operationGuid = json.OrderDemandGuid;

                var diskSpaces = _diskSpaceRepository.GetCustomersDiskSpace(json.ExternalId);
                diskSpaces.ForEach(i => 
                {
                    _sTaaSSoap.RenamePhysicalFolder(i.Identifier.ToString("N"));
                });

                _CompanyRepository.DeleteCompany(json.ExternalId, 0);  //unjeti usera

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);
            }

            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(Create), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }
    }
}
