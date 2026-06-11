using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class UserDtos
    {
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
