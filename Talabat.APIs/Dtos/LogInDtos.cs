using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class LogInDtos
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
