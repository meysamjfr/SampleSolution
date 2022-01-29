using System.Threading.Tasks;
using Project.DTOs.ExtendedUser;
using Project.Entities;

namespace Project.Repositories
{
    public interface IExtendedUserService
    {
        Task<ExtendedUser> Register(RegisterExtendedUserDTO user);
        Task<ExtendedUser> Login(LoginExtendedUserDTO user);
        Task Logout();
    }
}
