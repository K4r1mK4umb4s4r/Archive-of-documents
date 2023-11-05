using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiveOfDocuments.WebAPI.loC
{
 /// <summary>
 /// Static class for serilog configuration
 /// </summary>
        public static class SerilogConfigurator
        {
            public static void ConfigureService(WebApplicationBuilder builder)
            {
                builder.Host.UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration
                    .Enrich.WithCorrelationId()
                    .ReadFrom.Configuration(context.Configuration);
                });

                builder.Services.AddHttpContextAccessor();
            }

            public static void ConfigureApplication(IApplicationBuilder app)
            {
                app.UseSerilogRequestLogging();
            }
        }   
}
