using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using WebChat.Database;
using WebChat.Domain;
using WebChat.Domain.Entities;
using WebChat.Domain.Interfaces;
using WebChat.Domain.Models;
using WebChat.Hubs;
using WebChat.Middleware;
using WebChat.Services;
using WebChat.Validators;

namespace WebChat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation();

            // SignalR
            services.AddSignalR();
            services.AddSingleton<IDictionary<string, UserConnection>>(options => new Dictionary<string, UserConnection>());

            // VALIDATORS 
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

            // SERVICES
            services.AddScoped<IAccountService, AccountService>();

            // DATABASE
            services.AddDbContext<WebChatDbContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("WebChatDbConnectionString")));
            services.AddScoped<IWebChatDbContext, WebChatDbContext>();

            // MIDDLEWARE
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddHttpContextAccessor();

            // SWAGGER
            services.AddSwaggerGen();

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClientIntegration", builder =>
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(Configuration["CORS:FrontEndClientIntegrationUrl"]));
            });

            // AUTHENTICATION
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            var authenticationSettings = new AuthenticationSettings();
            services.AddSingleton(authenticationSettings);
            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });

            // AUTHORIZATION
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("FrontEndClientIntegration");
            /* if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } */

            app.UseAuthentication(); 

            app.UseMiddleware<ErrorHandlingMiddleware>();  

            //app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebChat Api");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });
        }
    }
}
