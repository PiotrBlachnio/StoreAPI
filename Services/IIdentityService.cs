using System.Threading.Tasks;
using Store.Database.Models;

namespace Store.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}