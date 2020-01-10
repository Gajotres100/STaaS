using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class UpdateCustomerParams : BaseParam
    {
        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}