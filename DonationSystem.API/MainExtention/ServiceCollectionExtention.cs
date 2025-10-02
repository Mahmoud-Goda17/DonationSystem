using System.Text;
using System.Text.Json.Serialization;
using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Presistance.Repository;
using Presistance;
using Services.Abstractions;
using Services;
using Shared.FluentValidation.Auth;
using Shared.FluentValidation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.API.MainExtention
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
           services.AddControllers()
                .AddJsonOptions(options =>
                {
                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddValidatorsFromAssemblyContaining<AssemblyRef>();

            services
                .AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DonationDbContext>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddDbContext<DonationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            });


            services.AddScoped<IunitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManger, ServiceManger>();


            services.Configure<JwtOpt>(config.GetSection("JwtOpt"));

            var jwtop = config.GetSection("JwtOpt").Get<JwtOpt>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtop.Issuer,
                    ValidAudience = jwtop.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtop.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}
