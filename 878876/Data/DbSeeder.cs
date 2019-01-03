using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _878876.Data
{
    public static class DbSeeder
    {
        public static void SeedDb(UserManager<IdentityUser> userManager)
        {
            CreateUsers(userManager);
        }

        private static void CreateUsers(UserManager<IdentityUser> userManager)
        {
            if(userManager.FindByNameAsync("Member1@email.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Member1@email.com",
                    Email = "Member1@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            
            if (userManager.FindByNameAsync("Customer1@email.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Customer1@email.com",
                    Email = "Customer1@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer2@email.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Customer2@email.com",
                    Email = "Customer2@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer3@email.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Customer3@email.com",
                    Email = "Customer3@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer4@email.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Customer4@email.com",
                    Email = "Customer4@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer5@email.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Customer5@email.com",
                    Email = "Customer5@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
        }
    }
}
