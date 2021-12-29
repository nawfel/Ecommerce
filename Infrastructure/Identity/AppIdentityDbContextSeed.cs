using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task seedUserAsync(UserManager<AppUser> userManager){

                if(!userManager.Users.Any()){
                    var user = new AppUser{
                        DisplayName="bob",

                        Email="bob@test.com",
                        UserName="bob@test.com",
                        Address=new Address{
                            FirstName="Bob",
                            LastName="Bobbity",
                            Street="10 th street",
                            City="New York",
                            State="NY",
                            ZipCode="9021"
                        }
                    };
                    await userManager.CreateAsync(user,"Pa$$w0rd");
                }
            
        }
    }
}