using ComProvis.Actions.Actions.Notification.Helper;
using ComProvis.Actions.Helper;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Log;
using ComProvis.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComProvis.Actions.Actions.Notification
{
    [Provision(ProvisionType.SendMail)]
    public class SendMail : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IConfiguration _configuration;

        public SendMail(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IConfiguration configuration) : base(logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
            _configuration = configuration;
        }

        public override void DoJob(dynamic data)
        {
            var operationGuid = string.Empty;

            try
            {
                var json = JsonConvert.DeserializeObject(data);
                operationGuid = json.OrderDemandGuid;

                var mail = new MailHelper(TemplateManager.EmailResources(_configuration["FileSystemResourceManagerResourcesPath"]).GetString(json.TemplateName.Value), _configuration["SmtpServer"], _configuration["SubjectPrefix"]);
                mail.ParseBody(json);

                mail.Send(_configuration["Bcc"]);

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);
            }
            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(SendMail), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }
    }
}
