using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class GetUserResult
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string ContactInfo { get; set; }
        [DataMember]
        public string UserId { get; set; }
    }
}