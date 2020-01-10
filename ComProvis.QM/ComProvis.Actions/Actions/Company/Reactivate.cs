using ComProvis.Actions.Helper;
using ComProvis.Data.Companys;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Log;
using ComProvis.Enums;
using ComProvis.Params;
using Newtonsoft.Json;
using System;

namespace ComProvis.Actions.SaaS.Company
{
    [Provision(ProvisionType.ReactivateCustomer)]
    public class Reactivate : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        ICompanyRepository _CompanyRepository;

        public Reactivate(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, ICompanyRepository CompanyRepository) : base(logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
            _CompanyRepository = CompanyRepository;
        }

        public override void DoJob(dynamic data)
        {
            var operationGuid = string.Empty;

            try
            {
                UpdateCompanyData json = JsonConvert.DeserializeObject<UpdateCompanyData>(data);
                operationGuid = json.OrderDemandGuid;

                _CompanyRepository.ReactivateCompany(json.ExternalId);

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
