using ComProvis.Api.Data.Customers;
using ComProvis.Api.Data.Loggers;
using ComProvis.Api.Data.OrderDemands;
using ComProvis.Enums;
using ComProvis.Params;
using ComProvis.Params.Data.STaaSData;
using ComProvis.Params.Data.UserData;
using Newtonsoft.Json;
using SaaSApi.Code.Validators;
using System;
using System.Linq;

namespace SaaSApi.Code.STaaSApi
{
    internal class STaaSOperation
    {
        #region Company

        internal GetCustomerResult GetCompany(string companyId)
        {
            var loggerManager = new LoggerManager();
            try
            {
                var customerManager = new CustomerManager();
                var company = customerManager.GetCompanyByExternalId(companyId);
                return company == null ? null : new GetCustomerResult
                {
                    Address = company.Address,
                    CompanyId = company.ExternalId,
                    CompanyName = company.Name,
                    Email = company.ContactEmail,
                    FirstName = company.ContactFirstName,
                    LastName = company.ContactLastName
                };
            }
            catch(Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(GetCustomerResult), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, null, companyId);
                return null;
            }
        }

        internal Result RemoveCompany(string externalId, RemoveCustomerParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(RemoveCompany), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var removeCompany = new DeleteCompanyData
                {
                    ExternalId = externalId,
                    OrderDemandGuid = operationGuid
                };

                var validator = new CompanyValidator();
                var valResults = validator.Validate(removeCompany);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.DeleteCustomer, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(removeCompany), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(RemoveCompany), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        internal Result SuspendCompany(string externalId, SuspendCustomerParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(SuspendCompany), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var suspendCompany = new SuspendCompanyData
                {
                    ExternalId = externalId,
                    OrderDemandGuid = operationGuid
                };

                var validator = new CompanyValidator();
                var valResults = validator.Validate(suspendCompany);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.SuspendCustomer, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(suspendCompany), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(SuspendCompany), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        internal Result AddCompany(AddCustomerParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(AddCompany), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var newCustomer = new CreateCompanyData
                {
                    Address = data.Address,
                    ContactEmail = data.Email,
                    ContactFirstName = data.FirstName,
                    ContactLastName = data.LastName,
                    ExternalId = data.CompanyId,
                    Name = data.CompanyName,
                    OrderDemandGuid = operationGuid
                };

                var validator = new AddCompanyValidator();
                var valResults = validator.Validate(newCustomer);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.CreateCustomer, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(newCustomer), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(AddCompany), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message};
            }
        }

        internal Result ReactivateCompany(string externalId, ReactivateCustomerParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(ReactivateCompany), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var reactivateCompany = new ReactivateCompanyData
                {
                    ExternalId = externalId,
                    OrderDemandGuid = operationGuid
                };

                var validator = new CompanyValidator();
                var valResults = validator.Validate(reactivateCompany);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.ReactivateCustomer, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(reactivateCompany), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(ReactivateCompany), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        internal Result UpdateCompany(string externalId, UpdateCustomerParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(UpdateCompany), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var updateCustomert = new UpdateCompanyData
                {
                    Address = data.Address,
                    ContactEmail = data.Email,
                    ContactFirstName = data.FirstName,
                    ContactLastName = data.LastName,
                    ExternalId = externalId,
                    Name = data.CompanyName,
                    OrderDemandGuid = operationGuid                    
                };

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.UpdateCustomer, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(updateCustomert), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(UpdateCompany), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        #endregion

        #region Assets

        internal Result AddAsset(string customerId, AddAssetParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(AddAsset), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var customerManager = new CustomerManager();
                var customer = customerManager.GetCompanyByExternalId(customerId);

                var user = customerManager.GetCompanyFirstAdminByExternalId(customerId);

                var product = customerManager.GetProductByProductExternalId(data.ProductId);

                var diskName = data.AdditionalAttribute.FirstOrDefault(a => a.Name.Equals(nameof(CreateDiskSpaceData.DiskName)));

                var orderDemandManager = new OrderDemandManager();
                var addAsset = new CreateDiskSpaceData
                {
                    OrderDemandGuid = operationGuid,
                    AssetGroupId = data.AssetId,
                    CompanyId = customer.CompanyId,
                    ProductId = product.ProductID,
                    UserId = user.UserId,
                    DiskName = diskName?.Value
                };

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.CreateDiskSpace, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(addAsset), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(AddAsset), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        internal Result UpdateAsset(string customerId, string assetId, UpdateAssetParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(UpdateAsset), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var customerManager = new CustomerManager();
                var customer = customerManager.GetCompanyByExternalId(customerId);

                var user = customerManager.GetCompanyFirstAdminByExternalId(customerId);

                var product = customerManager.GetProductByProductExternalId(data.ProductId);

                var diskSpace = customerManager.GetDiskSpaceByAssetGroupId(assetId);

                var orderDemandManager = new OrderDemandManager();
                var addAsset = new UpgradeDiskSpaceData
                {
                    OrderDemandGuid = operationGuid,
                    CompanyId = customer.CompanyId,
                    UserId = user.UserId,
                    ProductId = product.ProductID,
                    DiskSpaceId = diskSpace.DiskSpaceID
                };

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.UpgradeDiskSpace, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(addAsset), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(UpdateAsset), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        internal Result RemoveAsset(string customerId, string assetId, RemoveAssetParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(RemoveAsset), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var customerManager = new CustomerManager();
                var customer = customerManager.GetCompanyByExternalId(customerId);

                var user = customerManager.GetCompanyFirstAdminByExternalId(customerId);

                var diskSpace = customerManager.GetDiskSpaceByAssetGroupId(assetId);

                var orderDemandManager = new OrderDemandManager();
                var addAsset = new DeleteDiskSpaceData
                {
                    OrderDemandGuid = operationGuid,
                    CompanyId = customer.CompanyId,
                    UserId = user.UserId,
                    DiskSpaceId = diskSpace.DiskSpaceID
                };

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.DeleteDiskSpace, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(addAsset), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(RemoveAsset), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        #endregion

        #region User

        internal GetUserResult GetUser(string customerId, string userId)
        {
            var loggerManager = new LoggerManager();
            try
            {
                var customerManager = new CustomerManager();
                var user = customerManager.GetUserByExternalId(userId);
                return user == null ? null : new GetUserResult
                {
                    Address = user.Address,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ContactInfo = user.ContactInfo,
                    UserId = userId
                };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(GetUser), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, null, userId);
                return null;
            }
        }

        internal Result AddUser(string companyId, AddUserParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(AddUser), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var addUser = new CreateUserData
                {
                    Address = data.Address,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    OrderDemandGuid = operationGuid,
                    ContactInfo = data.ContactInfo,
                    ExternalCustomerId = companyId,
                    ExternalId = data.UserId,
                    Username = data.Username
                };

                var validator = new AddUserValidator();
                var valResults = validator.Validate(addUser);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.CreateUser, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(addUser), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(AddUser), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        internal Result UpdateUser(string customerId, string userId, UpdateUserParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(UpdateUser), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var updateUser = new UpdateUserData
                {
                    Address = data.Address,
                    OrderDemandGuid = operationGuid,
                    ContactInfo = data.ContactInfo,
                    ExternalId = userId,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName
                };

                var validator = new UpdateUserValidator();
                var valResults = validator.Validate(updateUser);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.UpdateUser, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(updateUser), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(UpdateUser), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            };
        }

        internal Result RemoveUser(string customerId, string externalId, RemoveUserParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(RemoveUser), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var removeUser = new DeleteUserData
                {
                    ExternalId = externalId,
                    OrderDemandGuid = operationGuid
                };

                var validator = new RemoveUserValidator();
                var valResults = validator.Validate(removeUser);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.DeleteUser, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(removeUser), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(RemoveUser), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        #endregion

        #region Transaction

        internal TransactionResult GetTransactionStatus(string transactionId)
        {
            var loggerManager = new LoggerManager();
            try
            {
                loggerManager.InsertLogoRecord(nameof(GetTransactionStatus), nameof(LogLevel.Info), null, transactionId, transactionId);
                var orderDemandManager = new OrderDemandManager();
                var orderDemand = orderDemandManager.GetOrderDemandByExternalTransactionId(transactionId);
                if(orderDemand == null)
                    return new TransactionResult { IsCompleted = true, Success = false, Message = { new Messages { Message = "Nepostojeći transactionId", Language = "hr" } } };
                else if(orderDemand.OrderDemandStateId == (int)OrderDemandStates.Created || orderDemand.OrderDemandStateId == (int)OrderDemandStates.Executing || orderDemand.OrderDemandStateId == (int)OrderDemandStates.WaitingApruval)
                    return new TransactionResult { IsCompleted = false, Success = false };
                else if (orderDemand.OrderDemandStateId == (int)OrderDemandStates.Finished)
                    return new TransactionResult { IsCompleted = true, Success = true };

                return new TransactionResult { IsCompleted = true, Success = false, Message = { new Messages { Message = "Greška prilikom provizioniranja", Language = "hr" } } };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(GetTransactionStatus), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, transactionId, transactionId);
                return new TransactionResult { IsCompleted = true, Success = false, Message = { new Messages { Message = ex.Message, Language = "hr" } } };
            }
        }

        #endregion

        #region Assignation

        internal Result AssignProduct(string customerId, string userId, string productId, AssignProductParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(AssignProduct), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var assignProduct = new AssigneProductData
                {
                    ExternalId = userId,                 
                    OrderDemandGuid = operationGuid,
                    ExternalProcudtId = productId
                };

                var validator = new AssignProductValidator();
                var valResults = validator.Validate(assignProduct);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.AssigneProduct, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(assignProduct), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(AssignProduct), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        internal Result RemoveProduct(string customerId, string userId, string productId, RemoveProductParams data)
        {
            var loggerManager = new LoggerManager();
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                loggerManager.InsertLogoRecord(nameof(RemoveProduct), nameof(LogLevel.Info), null, data.TransactionId, JsonConvert.SerializeObject(data));

                var orderDemandManager = new OrderDemandManager();
                var removeProduct = new RemoveProductData
                {
                    ExternalId = userId,
                    OrderDemandGuid = operationGuid,
                    ExternalProcudtId = productId
                };

                var validator = new RemoveProductValidator();
                var valResults = validator.Validate(removeProduct);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                orderDemandManager.SaveOrderDemand(null, operationGuid, 0, (int)ProvisionType.RemoveProduct, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(removeProduct), data.TransactionId);
                return new Result { IsCompleted = false, Success = true };
            }
            catch (Exception ex)
            {
                loggerManager.InsertLogoRecord(nameof(RemoveProduct), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, JsonConvert.SerializeObject(data));
                return new Result { IsCompleted = true, Success = false, Message = ex.Message };
            }
        }

        #endregion
    }
}