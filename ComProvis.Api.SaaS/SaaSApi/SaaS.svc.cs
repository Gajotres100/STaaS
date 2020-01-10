using SaaSApi.Code.STaaSApi;
using System.Collections.Generic;
using System.ServiceModel;

namespace SaaSApi
{
    [ServiceContract]
    public class SaaS
    {
        #region Customer

        [OperationContract]
        public GetCustomerResult GetCustomer(string CustomerId)
        {
            var op = new STaaSOperation();
            return op.GetCompany(CustomerId);
        }

        [OperationContract]
        public Result AddCustomer(AddCustomerParams data)
        {
            var op = new STaaSOperation();
            return op.AddCompany(data);
        }

        [OperationContract]
        public Result UpdateCustomer(string CustomerId, UpdateCustomerParams data)
        {
            var op = new STaaSOperation();
            return op.UpdateCompany(CustomerId, data);
        }

        [OperationContract]
        public Result RemoveCustomer(string CustomerId, RemoveCustomerParams data)
        {
            var op = new STaaSOperation();
            return op.RemoveCompany(CustomerId, data);
        }

        [OperationContract]
        public Result SuspendCustomer(string CustomerId, SuspendCustomerParams data)
        {
            var op = new STaaSOperation();
            return op.SuspendCompany(CustomerId, data);
        }

        [OperationContract]
        public Result ReactivateCustomer(string CustomerId, ReactivateCustomerParams data)
        {
            var op = new STaaSOperation();
            return op.ReactivateCompany(CustomerId, data);
        }

        #endregion

        #region Asset

        [OperationContract]
        public Result AddAsset(string CustomerId, AddAssetParams data)
        {
            var op = new STaaSOperation();
            return op.AddAsset(CustomerId, data);
        }

        [OperationContract]
        public Result UpdateAsset(string CustomerId, string AssetId, UpdateAssetParams data)
        {
            var op = new STaaSOperation();
            return op.UpdateAsset(CustomerId, AssetId, data);
        }

        [OperationContract]
        public Result RemoveAsset(string CustomerId, string AssetId, RemoveAssetParams data)
        {
            var op = new STaaSOperation();
            return op.RemoveAsset(CustomerId, AssetId, data);
        }

        #endregion

        #region User

        [OperationContract]
        public GetUserResult GetUser(string CustomerId, string UserId)
        {
            var op = new STaaSOperation();
            return op.GetUser(CustomerId,UserId);
        }

        [OperationContract]
        public Result AddUser(string CustomerId, AddUserParams data)
        {
            var op = new STaaSOperation();
            return op.AddUser(CustomerId, data);
        }

        [OperationContract]
        public Result UpdateUser(string CustomerId, string UserId, UpdateUserParams data)
        {
            var op = new STaaSOperation();
            return op.UpdateUser(CustomerId, UserId, data);
        }

        [OperationContract]
        public Result RemoveUser(string CustomerId, string UserId, RemoveUserParams data)
        {
            var op = new STaaSOperation();
            return op.RemoveUser(CustomerId, UserId, data);
        }

        #endregion

        #region Assignation

        [OperationContract]
        public Result AssignProduct(string CustomerId, string UserId, string ProductId, AssignProductParams data)
        {
            var op = new STaaSOperation();
            return op.AssignProduct(CustomerId, UserId, ProductId, data);
        }

        [OperationContract]
        public Result RemoveProduct(string CustomerId, string UserId, string ProductId, RemoveProductParams data)
        {
            var op = new STaaSOperation();
            return op.RemoveProduct(CustomerId, UserId, ProductId, data);
        }

        #endregion

        #region Transaction

        [OperationContract]
        public TransactionResult GetTransactionStatus(string TransactionId)
        {
            var op = new STaaSOperation();
            return op.GetTransactionStatus(TransactionId);
        }

        #endregion

        #region Validation

        [OperationContract]
        public ValidateResult ValidateAttribute(string Name, string Value, string ProductId, string CustomerId)
        {
            var ret = new ValidateResult
            {
                IsValid = true
            };

            var massages = new List<Messages>();
            var massage = new Messages
            {
                Language = "hr",
                Message = "Kloc"
            };
            massages.Add(massage);

            ret.Message = massages;

            return ret;
        }

        [OperationContract]
        public ValidateResult ValidateAsset(string AssetId, int ActionTypeId, string ProductId, int Quantity, AdditionalAttributes[] AdditionalAttribute)
        {
            var ret = new ValidateResult
            {
                IsValid = true
            };

            var massages = new List<Messages>();
            var massage = new Messages
            {
                Language = "hr",
                Message = "Kloc"
            };
            massages.Add(massage);

            ret.Message = massages;

            return ret;
        }

        #endregion
    }
}
