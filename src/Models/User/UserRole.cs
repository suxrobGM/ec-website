using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;

namespace EC_Website.Models.UserModel
{
    public enum Role
    {
        Editor,
        Developer,
        Moderator,
        Admin,
        SuperAdmin                              
    }

    public class UserRole : IdentityRole
    {
        public UserRole(Role role): base(role.ToString())
        {
            Id = GeneratorId.GenerateLong();
            Role = role;            
        }

        public Role Role { get; set; }        
    }
}
