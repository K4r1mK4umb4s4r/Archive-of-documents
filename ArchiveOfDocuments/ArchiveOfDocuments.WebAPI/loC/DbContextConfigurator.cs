using ArchiveOfDocuments.DataAccess;
using ArchiveOfDocuments.WebAPI.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArchiveOfDocuments.Service.IoC
{
    public class DbContextConfigurator
    {
        public static void ConfigureService(IServiceCollection services, ArchiveOfDocumentsSettings settings)
        {
            services.AddDbContextFactory<ArchiveOfDocumentsDbContext>(
                options => { options.UseSqlServer(settings.ArchiveOfDocumentsDbContextConnectionString); },
                ServiceLifetime.Scoped);
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ArchiveOfDocumentsDbContext>>();
            using var context = contextFactory.CreateDbContext();
            context.Database.Migrate();
        }
    }
}