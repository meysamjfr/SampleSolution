using System.Threading.Tasks;
using Project.Models;

namespace Project.Repositories.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SimpleSend(Email email);
    }
}
