using System.Threading.Tasks;
using Project.DTOs.ApplicationUsers;
using Project.Entities;

namespace Project.Repositories
{
    public interface IApplicationUserService
    {
        ApplicationUserDTO Current();
        Task<ApplicationUserDTO> EditProfile(EditProfileApplicationUserDTO editProfile);
        Task<ApplicationUserDTO> GetById(int id);
        Task<bool> Login(LoginApplicationUserDTO login);
        Task<ApplicationUserDTO> Verify(VerifyApplicationUserDTO verify);
    }
}
