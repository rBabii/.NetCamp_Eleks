using BlogPlatform.Application.Managers.AuthManager;
using BlogPlatform.Application.Managers.BlogManager;
using BlogPlatform.Application.Managers.PostManager;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using BlogPlatform.Infrastructure.ExternalOptions;
using BlogPlatform.Infrastructure.HttpServices.Auth;
using BlogPlatform.Infrastructure.Repositories.MsSQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace BlogPlatform.Api
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

            services.AddHttpClient<AuthService>(c =>
            {
                c.BaseAddress = new Uri("http://localhost:5000");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddSingleton<AuthManager>();

            services.AddDbContext<DataContext>(opts => opts.UseSqlServer(Configuration["Data:ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IBlogRepository, MsSqlBlogRepository>();
            services.AddScoped<IPostRepository, MsSqlPostRepository>();
            services.AddScoped<BlogManager>();
            services.AddScoped<PostManager>();

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

            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
