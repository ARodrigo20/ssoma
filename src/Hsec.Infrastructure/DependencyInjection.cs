using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hsec.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Hsec.Application.Common.Interfaces;
using Hsec.Infrastructure.Services;

namespace Hsec.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());            
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ICorreosService, CorreosService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IFileLevTareaService, FileUploadLevTareaService>();
            services.AddTransient<IReportService, ReportService>();

            return services;
        }
    }
}
