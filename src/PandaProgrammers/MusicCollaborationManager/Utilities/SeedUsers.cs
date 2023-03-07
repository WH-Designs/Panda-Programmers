using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.Data;
using MusicCollaborationManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCollaborationManager.Utilities
{
    public static class SeedUsers
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserInfoData[] seedData, string testUserPw)
        {
            try
            {
                using (var context = new MCMDbContext(serviceProvider.GetRequiredService<DbContextOptions<MCMDbContext>>()))
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    foreach (var u in seedData)
                    {
                        var identityID = await EnsureUser(userManager, testUserPw, u.Email, u.Email, u.EmailConfirmed);
                        Listener li = new Listener { AspnetIdentityId = identityID, FirstName = u.FirstName, LastName = u.LastName, FriendId = u.FriendID};
                        if (!context.Listeners.Any(x => x.AspnetIdentityId == li.AspnetIdentityId && x.FirstName == li.FirstName && x.LastName == li.LastName))
                        {
                            context.Add(li);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Failed to initialize user seed data, service provider did not have the correct service");
            }
        }

        public static async Task InitializeAdmin(IServiceProvider serviceProvider, string email, string userName, string adminPw, 
            string firstName, string lastName, int friendId)
        {
            try
            {
                using (var context = new MCMDbContext(serviceProvider.GetRequiredService<DbContextOptions<MCMDbContext>>())) 
                { 
                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    var identityID = await EnsureUser(userManager, adminPw, email, email, true);
                    Listener li = new Listener { AspnetIdentityId = identityID, FirstName = firstName, LastName = lastName, FriendId = friendId};
                    if (!context.Listeners.Any(x => x.AspnetIdentityId == li.AspnetIdentityId && x.FirstName == li.FirstName &&
                        x.LastName== li.LastName)) 
                    { 
                        context.Add(li);
                        await context.SaveChangesAsync();
                    }
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    await EnsureRoleForUser(roleManager, userManager, identityID, "admin");
                }
            }
            catch(InvalidOperationException ex)
            {
                throw new Exception("Failed to initialize admin user or role, service provider did not have the correct service:" +
                    ex.Message);
            }
        }

        private static async Task<string> EnsureUser(UserManager<IdentityUser> userManager, string password, string username, string email, bool emailConfirmed)
        {
            var user = await userManager.FindByNameAsync(username);
            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = emailConfirmed                   
                };
                await userManager.CreateAsync(user, password);
            }
            if(user == null)
            {
                throw new Exception("The password is probably not strong enough");
            }
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRoleForUser(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser>
            userManager, string uid, string role)
        {
            IdentityResult iR = null;

            if (!await roleManager.RoleExistsAsync(role))
            {
                iR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = await userManager.FindByIdAsync(uid);
            if(user == null)
            {
                throw new Exception("An AspNetUser does not exist with the given id so we cannot give them the requested role");
            }

            if(!await userManager.IsInRoleAsync(user, role))
            {
                iR = await userManager.AddToRoleAsync(user, role);
            }
            return iR;
        }
    
    }

}
