using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class RemoveAssetParams : BaseParam
    {
        [DataMember]
        public string ProductId { get; set; }
    }
}