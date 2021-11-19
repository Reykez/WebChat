using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Database;
using WebChat.Domain;
using WebChat.Domain.Interfaces;
using WebChat.Middleware;
using WebChat.Services;

namespace WebChat
{
    // Implement the observer (to be able to notify all objects of state)
    // Or directly download from database (with refresh || notify when online?) - to research. 

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
            // VALIDATORS 
            services.AddControllers();

            // SERVICES
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMessageService, MessageService>();

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
                        .AllowAnyOrigin());
            });

            // AUTHENTICATION
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
            services.AddScoped<IAuthorizationHandler,AuthorizationHandler> ();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("FrontEndIntegration");
            /* if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } */

            app.UseAuthentication(); 

            app.UseMiddleware<ErrorHandlingMiddleware>();  

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebChat Api");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
