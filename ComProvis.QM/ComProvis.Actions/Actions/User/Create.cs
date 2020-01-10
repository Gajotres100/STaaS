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
    [Provision(ProvisionType.CreateUser)]
    public class Create : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IUserRepository _userRepository;

        public Create(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IUserRepository CompanyRepository) : base(logRepository)
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
                CreateUserData json = JsonConvert.DeserializeObject<CreateUserData>(data);
                operationGuid = json.OrderDemandGuid;

                _userRepository.SaveUser(json);

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
