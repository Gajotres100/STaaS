using ComProvis.Actions.Helper;
using ComProvis.Data.Companys;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Log;
using ComProvis.Enums;
using ComProvis.Params.Data.UserData;
using Newtonsoft.Json;
using System;

namespace ComProvis.Actions.Actions.User
{
    [Provision(ProvisionType.AssigneProduct)]
    public class AssigneProduct : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IUserRepository _userRepository;

        public AssigneProduct(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IUserRepository CompanyRepository) : base(logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
            _userRepository = CompanyRepository;
        }

        public override void DoJob(dynamic data)
        {
            var operationGuid = string.Empty;

            try
            {
                AssigneProductData json = JsonConvert.DeserializeObject<AssigneProductData>(data);
                operationGuid = json.OrderDemandGuid;

                _userRepository.AddUserRole(json.ExternalId, json.ExternalProcudtId);

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);
            }

            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(AssigneProduct), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }
    }
}
