using Core.Entities;

namespace Core.Iterfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
         
    }
}