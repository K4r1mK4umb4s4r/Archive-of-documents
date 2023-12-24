using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ArchiveOfDocuments.UnitTests.Repository
{
    public class RepositoryTestsBaseClass
    {
        public RepositoryTestsBaseClass()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Test.json", optional: true)
                .Build();

            Settings = ArchiveOfDocumentsSettingsReader.Read(configuration);
            ServiceProvider = ConfigureServiceProvider();

            DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<ArchiveOfDocumentsDbContext>>();
        }

        private IServiceProvider ConfigureServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContextFactory<ArchiveOfDocumentsDbContext>(
                options => { options.UseSqlServer(Settings.ArchiveOfDocumentsDbContextConnectionString); },
                ServiceLifetime.Scoped);
            return serviceCollection.BuildServiceProvider();
        }

        protected readonly ArchiveOfDocumentsSettings Settings;
        protected readonly IDbContextFactory<ArchiveOfDocumentsDbContext> DbContextFactory;
        protected readonly IServiceProvider ServiceProvider;
    }
}