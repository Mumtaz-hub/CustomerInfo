using Domain.CoreEntities;

namespace Domain.Entities
{
    public class User : BaseEntity   
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public string MobileNumber { get; set; }
    }

}
