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
            if(context.Post.Any() == false)
            {
                context.Post.Add(new Post()
                {
                    Title = "What is surfing?",
                    Author = "Member1@email.com",
                    PostDate = DateTime.Now,
                    Content = "Surfing is a surface water sport in which the wave rider, referred to as a surfer, " +
                    "rides on the forward or deep face of a moving wave, which usually carries the surfer towards the shore. Waves suitable for surfing are primarily found in the ocean, " +
                    "but can also be found in lakes or rivers in the form of a standing wave or tidal bore. However, surfers can also utilize artificial waves such as those from boat wakes " +
                    "and the waves created in artificial wave pools. The term surfing refers to the act of riding a wave, regardless of whether the wave is ridden with a board or without a board, " +
                    "and regardless of the stance used.The native peoples of the Pacific, for instance, surfed waves on alaia, paipo, and other such craft, and did so on their belly and knees." +
                    "The modern - day definition of surfing, however, most often refers to a surfer riding a wave standing up on a surfboard this is also referred to as stand - up surfing.",
                });
                context.Post.Add(new Post()
                {
                    Title = "What is swell?",
                    Author = "Member1@email.com",
                    PostDate = DateTime.Now,
                    Content = "Swell is generated when wind blows consistently over a large area of open water, called the wind's fetch. " +
                    "The size of a swell is determined by the strength of the wind and the length of its fetch and duration. Because of this, surf tends to be larger and more prevalent on coastlines " +
                    "exposed to large expanses of ocean traversed by intense low pressure systems. Local wind conditions affect wave quality, since the surface of a wave can become choppy in blustery conditions." +
                    "Ideal conditions include a light to moderate offshore wind,because it blows into the front of the wave, making it a barrel or tube wave.Waves are Left handed and Right Handed depending upon " +
                    "the breaking formation of the wave. Waves are generally recognized by the surfaces over which they break. For example, there are Beach breaks, Reef breaks and Point breaks. The most important influence " +
                    "on wave shape is the topography of the seabed directly behind and immediately beneath the breaking wave.The contours of the reef or bar front becomes stretched by diffraction. Each break is different, since each " +
                    "location's underwater topography is unique. At beach breaks, sandbanks change shape from week to week. Surf forecasting is aided by advances in information technology. Mathematical modeling graphically depicts the size and direction of swells around the globe.",
                });
            }
            context.SaveChanges();
        }

        private static void CreateUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("SiteOwner@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "SiteOwner@email.com",
                    Email = "SiteOwner@email.com",
                    Name = "Luke",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Member1@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Member1@email.com",
                    Email = "Member1@email.com",
                    Name = "Member1",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer1@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer1@email.com",
                    Email = "Customer1@email.com",
                    Name = "Customer1",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer2@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer2@email.com",
                    Email = "Customer2@email.com",
                    Name = "Customer2",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer3@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer3@email.com",
                    Email = "Customer3@email.com",
                    Name = "Customer3",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer4@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer4@email.com",
                    Email = "Customer4@email.com",
                    Name = "Customer4",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
            if (userManager.FindByNameAsync("Customer5@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Customer5@email.com",
                    Email = "Customer5@email.com",
                    Name = "Customer5",
                };
                userManager.CreateAsync(user, "Password123!").Wait();
            }
        }

        private static async Task CreateClaims(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser siteowner = await userManager.FindByEmailAsync("SiteOwner@email.com");
            var allClaimsList = (await userManager.GetClaimsAsync(siteowner)).Select(p => p.Type);

            if (!allClaimsList.Contains("canEdit"))
            {
                await userManager.AddClaimAsync(siteowner, new Claim("canEdit", "canEdit"));
            }
            if (!allClaimsList.Contains("canComment"))
            {
                await userManager.AddClaimAsync(siteowner, new Claim("canComment", "canComment"));
            }
            if (!allClaimsList.Contains("canAddUser"))
            {
                await userManager.AddClaimAsync(siteowner, new Claim("canAddUser", "canAddUser"));
            }
            if (!allClaimsList.Contains("canEditUser"))
            {
                await userManager.AddClaimAsync(siteowner, new Claim("canEditUser", "canEditUser"));
            }
            if (!allClaimsList.Contains("canDeleteUser"))
            {
                await userManager.AddClaimAsync(siteowner, new Claim("canDeleteUser", "canDeleteUser"));
            }

            ApplicationUser member1 = await userManager.FindByEmailAsync("Member1@email.com");
            var claimList = (await userManager.GetClaimsAsync(member1)).Select(p => p.Type);

            if (!claimList.Contains("canEdit"))
            {
                await userManager.AddClaimAsync(member1, new Claim("canEdit", "canEdit"));
            }
            if (!claimList.Contains("canComment"))
            {
                await userManager.AddClaimAsync(member1, new Claim("canComment", "canComment"));
            }

            ApplicationUser customer1 = await userManager.FindByEmailAsync("Customer1@email.com");
            var claimList2 = (await userManager.GetClaimsAsync(customer1)).Select(p => p.Type);

            if (!claimList2.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer1, new Claim("canComment", "canComment"));
            }

            ApplicationUser customer2 = await userManager.FindByEmailAsync("Customer2@email.com");
            var claimList3 = (await userManager.GetClaimsAsync(customer2)).Select(p => p.Type);

            if (!claimList3.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer2, new Claim("canComment", "canComment"));
            }

            ApplicationUser customer3 = await userManager.FindByEmailAsync("Customer3@email.com");
            var claimList4 = (await userManager.GetClaimsAsync(customer3)).Select(p => p.Type);

            if (!claimList4.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer3, new Claim("canComment", "canComment"));
            }

            ApplicationUser customer4 = await userManager.FindByEmailAsync("Customer4@email.com");
            var claimList5 = (await userManager.GetClaimsAsync(customer4)).Select(p => p.Type);

            if (!claimList5.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer4, new Claim("canComment", "canComment"));
            }

            ApplicationUser customer5 = await userManager.FindByEmailAsync("Customer5@email.com");
            var claimList6 = (await userManager.GetClaimsAsync(customer5)).Select(p => p.Type);

            if (!claimList6.Contains("canComment"))
            {
                await userManager.AddClaimAsync(customer5, new Claim("canComment", "canComment"));
            }
        }
    }
}
