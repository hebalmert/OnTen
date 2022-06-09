using Microsoft.AspNetCore.Identity;
using OnTen.Web.Data.Entities;
using OnTen.Web.Models;
using System.Threading.Tasks;

namespace OnTen.Web.Helper
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();


        //Se pasa el Usuario y el Pass, para ver si es Valido el Usuario, nos da un Result
        Task<SignInResult> ValidatePasswordAsync(User user, string password);



    }
}
