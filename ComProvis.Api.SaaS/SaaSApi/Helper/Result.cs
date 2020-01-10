using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class Result
    {
        [DataMember(Order = 0)]
        public bool IsCompleted { get; set; }
        [DataMember(Order = 1)]
        public bool Success { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }
    }

    [DataContract]
    public class TransactionResult
    {
        public TransactionResult()
        {
            Message = new List<Messages>();
        }

        [DataMember(Order = 0)]
        public bool IsCompleted { get; set; }
        [DataMember(Order = 1)]
        public bool Success { get; set; }
        [DataMember(Order = 2)]
        public List<Messages> Message { get; set; }
    }
}