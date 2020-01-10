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
    [Provision(ProvisionType.RemoveProduct)]
    public class RemoveProduct : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IUserRepository _userRepository;

        public RemoveProduct(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IUserRepository CompanyRepository) : base(logRepository)
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
                RemoveProductData json = JsonConvert.DeserializeObject<RemoveProductData>(data);
                operationGuid = json.OrderDemandGuid;

                _userRepository.RemoveUserRole(json.ExternalId);

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);
            }

            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(RemoveProduct), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }
    }
}
