using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Database.Models;

namespace Store.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}