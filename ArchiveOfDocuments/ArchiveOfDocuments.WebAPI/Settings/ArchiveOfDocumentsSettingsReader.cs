using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiveOfDocuments.WebAPI.Settings
{
    public static class ArchiveOfDocumentsSettingsReader
    {
        public static ArchiveOfDocumentsSettings Read(IConfiguration configuration)
        {
            return new ArchiveOfDocumentsSettings()
            {
                ServiceUri = configuration.GetValue<Uri>("Uri"),
                ArchiveOfDocumentsDbContextConnectionString = configuration.GetValue<string>("ArchiveOfDocumentsDbContext"),
                IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
                ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
                ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            };
        }
    }
}
