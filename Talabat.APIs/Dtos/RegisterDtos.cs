using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class RegisterDtos
    {
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$" ,
            ErrorMessage = " Has minimum 8 characters in length , At least one uppercase English letter ,At least one lowercase English letter.,At least one digit,At least one special character")]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
