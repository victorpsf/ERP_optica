﻿using Application.Core;
using Application.Interfaces.Utils;
using Personal.Service.Repositories;
using Personal.Service.Repositories.Services;
using static Application.Base.Models.ConfigurationModels;

namespace Personal.Service
{
    public class Startup: StartupCore
    {
        public Startup(IConfiguration configuration) : base(
            configuration: configuration,
            databaseNames: new List<DatabaseName> {
                DatabaseName.AUTHENTICATION,
                DatabaseName.AUTHORIZATION,
                DatabaseName.PERSONAL
            },
            prefix: "person",
            disableCors: true
        )
        { }

        public override void ConfigureAnotherServices(IServiceCollection services, IAppConfigurationManager configuration, StartupCore context)
        {
            services.AddScoped<PersonRepository>();
            services.AddScoped<PersonRepoService>();
            services.AddScoped<DocumentRepoService>();
        }
    }
}
