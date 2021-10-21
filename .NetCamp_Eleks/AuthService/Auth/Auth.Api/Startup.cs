using Auth.Application.Helpers;
using Auth.Application.Helpers.JWT.Auth;
using Auth.Application.Helpers.JWT.EmailVerify;
using Auth.Application.Helpers.JWT.RefreshToken;
using Auth.Application.Helpers.JWT.ResetPassword;
using Auth.Application.Options;
using Auth.Application.UserManager;
using Auth.Domain.UserAggregate;
using Auth.Infrastructure.Repositories;
using Auth.Infrastructure.Services.Email;
using Auth.Infrastructure.Services.Email.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            
            services.AddSingleton<PasswordHasher>();

            services.AddSingleton<AuthTokenHelper>();
            services.AddSingleton<EmailVerifyTokenHelper>();
            services.AddSingleton<ResetPasswordTokenHelper>();
            services.AddSingleton<RefreshTokenHelper>();

            services.AddSingleton<EmailService>();

            services.AddSingleton<UserManager>();

            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "http://localhost:4201", "http://localhost:4202")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Secret)),
                        ValidateIssuerSigningKey = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });

            var authOptionsConfiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsConfiguration);

            var hashingConfigurations = Configuration.GetSection("Hashing");
            services.Configure<HashingOptions>(hashingConfigurations);

            var refreshOptionsConfigurations = Configuration.GetSection("AuthRefresh");
            services.Configure<RefreshOptions>(refreshOptionsConfigurations);

            var emailVerifyTokenOptionsConfigurations = Configuration.GetSection("EmailVerifyToken");
            services.Configure<EmailVerifyTokenOptions>(emailVerifyTokenOptionsConfigurations);

            var resetPasswordTokenOptionsConfigurations = Configuration.GetSection("PasswordResetToken");
            services.Configure<ResetPasswordTokenOptions>(resetPasswordTokenOptionsConfigurations);

            var EmailServiceOptionsConfigurations = Configuration.GetSection("EmailServiceOptions");
            services.Configure<EmailServiceOptions>(EmailServiceOptionsConfigurations);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
