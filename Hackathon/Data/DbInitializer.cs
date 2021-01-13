using Microsoft.AspNetCore.Identity;

namespace Hackathon.Data
{
    public static partial class DbInitializer
    {
        public static void Initialize(UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser
            {
                UserName = "user@conestogac.on.ca",
                Email = "user@conestogac.on.ca",
            };

            string password = "Password1@";
            var result = userManager.CreateAsync(user).Result;
            if (result.Succeeded)
            {
                userManager.AddPasswordAsync(user, password).Wait();
            }
        }
    }
}
