using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class UpdateAssetParams : BaseParam
    {
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        AdditionalAttributes[] AdditionalAttribute { get; set; }
    }
}