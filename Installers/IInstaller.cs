using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}