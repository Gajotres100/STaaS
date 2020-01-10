namespace ComProvis.Enums
{
    public enum ProvisionType
    {
        #region Company

        CreateCustomer = 1001,
        DeleteCustomer = 1002,
        UpdateCustomer = 1003,
        SuspendCustomer = 1004,
        ReactivateCustomer = 1005,

        #endregion

        #region User

        CreateUser = 1006,
        DeleteUser = 1007,
        UpdateUser = 1008,
        AssigneProduct = 1018,
        RemoveProduct = 1019,

        #endregion

        #region StaaS

        CreateDiskSpace = 1009,
        DeleteDiskSpace = 1010,
        DeleteAllDisksDelayed = 1011,
        DeleteDiskSpaceOnPlatform = 1012,
        DeleteService = 1013,
        ReactivateDiskSpace = 1014,
        SuspendDiskSpace = 1015,
        UpdateDiskSpace = 1016,
        UpgradeDiskSpace = 1017,

        #endregion

        #region Notofication

        SendMail = 1020

        #endregion


    }
}
