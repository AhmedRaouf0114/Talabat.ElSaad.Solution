using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Error;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManger;

        public AccountController(UserManager<AppUser> userManger , SignInManager<AppUser> signInManger)
        {
            _userManger = userManger;
            _signInManger = signInManger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDtos>> LogIn(LogInDtos model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var result = await _signInManger.CheckPasswordSignInAsync(user, model.Password ,false);
            if (!result.Succeeded) 
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDtos()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "JWT"
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDtos>> Register(RegisterDtos model)
        {
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split("@")[0]
            };

            var result = await _userManger.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            return new UserDtos()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "JWT"
            };

        }
    }
}
