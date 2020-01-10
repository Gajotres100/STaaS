using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class ValidateResult
    {
        [DataMember]
        public bool IsValid { get; set; }
        [DataMember]
        public List<Messages> Message { get; set; }
    }
}