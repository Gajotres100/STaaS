using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class BaseParam
    {
        [DataMember]
        public string TransactionId { get; set; }
    }
}