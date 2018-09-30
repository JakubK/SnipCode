using AspNetCore.Identity.LiteDB;
using AspNetCore.Identity.LiteDB.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Services;
using SnipCodeAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace SnipCodeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDateTime, SystemDateTime>();

            services.AddSingleton<IDataGateway>(dataMapper => new LiteRepositoryDataMapper(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ISnippetRepository, SnippetRepository>();
            services.AddSingleton<ISnippetFileRepository, SnippetFileRepository>();
            services.AddSingleton<ISnippetService, SnippetService>();
            services.AddSingleton<IAuthService, AuthService>();

            services.AddSingleton<LiteDbContext>();
            services.AddIdentity<User,AspNetCore.Identity.LiteDB.IdentityRole>(options =>
            services.AddTransient<ISeedService, SeedService>();
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddUserStore<LiteDbUserStore<User>>()
              .AddRoleStore<LiteDbRoleStore<AspNetCore.Identity.LiteDB.IdentityRole>>()
              .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "SnipCode API",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SnipCode API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
