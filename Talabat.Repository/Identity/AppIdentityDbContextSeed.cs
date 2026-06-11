using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public  static class AppIdentityDbContextSeed
    {
        public  static async Task SeedUsersAsync(UserManager<AppUser> _UserManger)
        {
            if (_UserManger.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    UserName = "Saad.Magdy",
                    DisplayName = "Saad Magdy",
                    Email = "saadeldin.magdy1@gmail.com",
                    PhoneNumber = "01011223582",
                };

                await _UserManger.CreateAsync(user , "Pa$$w0rd");

            }
        }
    }
}
