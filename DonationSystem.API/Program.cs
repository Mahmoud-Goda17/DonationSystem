
using Domain.Contracts;
using Domain.Models;
using DonationSystem.API.MainExtention;
using DonationSystem.API.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistance;
using Presistance.Repository;
using Services;
using Services.Abstractions;
using Shared.FluentValidation;
using Shared.FluentValidation.Auth;
using Shared.FluentValidation.Donor;
using System.Text;
using System.Text.Json.Serialization;

namespace DonationSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //services
            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();
            // create roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = { "User", "Admin", "Donor", "Patient" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                    {
                        roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                    }
                }
            }


            // Middleware
            app.UseApplicationPipeline();

            app.Run();
        }
    }
}
