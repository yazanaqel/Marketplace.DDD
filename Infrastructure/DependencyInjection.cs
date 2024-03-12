using Application.Application.Abstractions;
using Domain.Domain.Products;
using Domain.Domain.Users;
using Infrastructure.SqlServer;
using Infrastructure.SqlServer.Repositories;
using Infrastructure.SqlServerDb.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

        services.AddDbContext<MarketplaceDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddStackExchangeRedisCache(options => {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "RedisCache_";
        });


        services.AddIdentity<ApplicationUser, IdentityRole>(op => {
            op.Password.RequireDigit = false;
            op.Password.RequiredLength = 6;
            op.Password.RequireUppercase = false;
            op.Password.RequireLowercase = false;
            op.Password.RequireNonAlphanumeric = false;
            op.SignIn.RequireConfirmedAccount = false;
            op.ClaimsIdentity.UserIdClaimType = "UserId";
        })
    .AddEntityFrameworkStores<MarketplaceDbContext>();

        services.AddScoped<IDbContext>(factory => factory.GetRequiredService<MarketplaceDbContext>());
        services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<MarketplaceDbContext>());
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }

    public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration configuration) {

        services.Configure<HelperJWT>(configuration.GetSection(nameof(HelperJWT)));

        //services.AddAuthentication(options => {
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(o => {
        //    o.RequireHttpsMetadata = false;
        //    o.SaveToken = false;
        //    o.TokenValidationParameters = new TokenValidationParameters {
        //        ValidateIssuerSigningKey = true,
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidIssuer = configuration["HelperJWT:Issuer"],
        //        ValidAudience = configuration["HelperJWT:Audience"],
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["HelperJWT:Key"]))
        //    };
        //});

        return services;
    }
}


