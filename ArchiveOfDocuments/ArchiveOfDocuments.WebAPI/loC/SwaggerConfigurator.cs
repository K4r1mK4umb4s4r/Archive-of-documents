using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiveOfDocuments.WebAPI.loC
{
        /// <summary>
        /// Static class for Swagger configuration
        /// </summary>
        public static class SwaggerConfigurator
        {
            public static void ConfigureServices(IServiceCollection services)
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
            }


            public static void ConfigureApplication(IApplicationBuilder app)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
}
