
using Domain;

namespace Application.Interfaces
{
    // Interface injection; implementation in Infrastructure.Security 
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
