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
    [Provision(ProvisionType.UpdateUser)]
    public class Update : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IUserRepository _userRepository;

        public Update(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IUserRepository CompanyRepository) : base(logRepository)
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
                UpdateUserData json = JsonConvert.DeserializeObject<UpdateUserData>(data);
                operationGuid = json.OrderDemandGuid;

                _userRepository.UpdateUser(json);

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
