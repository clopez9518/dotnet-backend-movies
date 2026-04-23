using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Interfaces;
using Movies.Application.Mappings;
using Movies.Application.Services;
using Movies.Application.Settings;
using Movies.Infrastructure.Persistence;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Services;
using Microsoft.Extensions.Options;
using Movies.Infrastructure.Cookies;
using Movies.Infrastructure.External.TMDb.Services;
using Movies.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Infrastructure.Extensions
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //Services
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieAdminService, MovieAdminService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<DbInitializer>();

            //HttpClient for TMDb API
            services.AddHttpClient<ITmdbService, TmdbService>(client =>
            {
                client.BaseAddress = new Uri(configuration["TMDb:BaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            //Configurations
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            //Authentication
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization();

            //Repository
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            //Mappers
            services.AddAutoMapper(cfg => { }, typeof(MovieProfile).Assembly);
            services.AddAutoMapper(cfg => { }, typeof(GenreProfile).Assembly);
            services.AddAutoMapper(cfg => { }, typeof(UserProfile).Assembly);

            return services;
        }
    }
}
