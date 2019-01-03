using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _878876.Models;

namespace _878876.Data
{
    public static class DbSeeder
    {
        public static void SeedDb(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            CreateData(context);
            CreateUsers(userManager);
            CreateClaims(userManager).Wait();
        }

        private static void CreateData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            context.Post.Add(new Post()
            {
                Title = "Test Post 1.",
                PostDate = DateTime.Now,
                Content = "This is some content for a test post.",
            });
            context.SaveChanges();
        }

        private static void CreateUsers(UserManager<ApplicationUser> userManager)
        {
            if(userManager.FindByNameAsync("Member1@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Member1@email.com",
                    Email = "Member1@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer1@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer1@email.com",
                    Email = "Customer1@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer2@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer2@email.com",
                    Email = "Customer2@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer3@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer3@email.com",
                    Email = "Customer3@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer4@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer4@email.com",
                    Email = "Customer4@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer5@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer5@email.com",
                    Email = "Customer5@email.com",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
        }

        private static async Task CreateClaims(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser member1 = await userManager.FindByEmailAsync("Member1@email.com");
            var claimList = (await userManager.GetClaimsAsync(member1)).Select(p => p.Type);

            if (!claimList.Contains("canEdit"))
            {
                await userManager.AddClaimAsync(member1, new Claim("canEdit", "true"));
            }
            if (!claimList.Contains("canComment"))
            {
                await userManager.AddClaimAsync(member1, new Claim("canComment", "true"));
            }

            ApplicationUser customer1 = await userManager.FindByEmailAsync("Customer1@email.com");
            var claimList2 = (await userManager.GetClaimsAsync(customer1)).Select(p => p.Type);

            if (!claimList2.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer1, new Claim("canComment", "true"));
            }

            ApplicationUser customer2 = await userManager.FindByEmailAsync("Customer2@email.com");
            var claimList3 = (await userManager.GetClaimsAsync(customer2)).Select(p => p.Type);

            if (!claimList3.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer2, new Claim("canComment", "true"));
            }

            ApplicationUser customer3 = await userManager.FindByEmailAsync("Customer3@email.com");
            var claimList4 = (await userManager.GetClaimsAsync(customer3)).Select(p => p.Type);

            if (!claimList4.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer3, new Claim("canComment", "true"));
            }

            ApplicationUser customer4 = await userManager.FindByEmailAsync("Customer4@email.com");
            var claimList5 = (await userManager.GetClaimsAsync(customer4)).Select(p => p.Type);

            if (!claimList5.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer4, new Claim("canComment", "true"));
            }

            ApplicationUser customer5 = await userManager.FindByEmailAsync("Customer5@email.com");
            var claimList6 = (await userManager.GetClaimsAsync(customer5)).Select(p => p.Type);

            if (!claimList6.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer5, new Claim("canComment", "true"));
            }
        }
    }
}
