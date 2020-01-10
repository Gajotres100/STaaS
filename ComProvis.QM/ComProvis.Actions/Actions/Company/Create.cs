﻿using ComProvis.Actions.Helper;
using ComProvis.Data.Companys;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Log;
using ComProvis.Enums;
using ComProvis.Params;
using Newtonsoft.Json;
using System;

namespace ComProvis.Actions.SaaS.Company
{
    [Provision(ProvisionType.CreateCustomer)]
    public class Create : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        ICompanyRepository _CompanyRepository;

        public Create(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, ICompanyRepository CompanyRepository) : base(logRepository)
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
                CreateCompanyData json = JsonConvert.DeserializeObject<CreateCompanyData>(data);
                operationGuid = json.OrderDemandGuid;

                _CompanyRepository.SaveCompany(json);

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
