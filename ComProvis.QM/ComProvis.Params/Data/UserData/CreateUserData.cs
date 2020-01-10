namespace ComProvis.Params.Data.UserData
{
    public class CreateUserData : UserBase
    {
        public string ExternalCustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Username { get; set;}
        public string Email { get; set; }
        public string ContactInfo { get; set; }
    }
}
