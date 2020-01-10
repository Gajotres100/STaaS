using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class AssignProductParams : BaseParam
    {
        [DataMember]
        AdditionalAttributes[] AdditionalAttribute { get; set; }
    }
}