using CarnetEstudiantil.Api.Services;
using CarnetEstudiantil.Persistencia.Context;
using CarnetEstudiantil.Persistencia.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarnetEstudiantil.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("https://0.0.0.0:7276");

            // ============================================================
            // BASE DE DATOS
            // ============================================================

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            // ============================================================
            // REPOSITORIES
            // ============================================================

            builder.Services.AddScoped<ICarnetRepository, CarnetRepository>();

            // Contraseña encriptada
            builder.Services.AddScoped<PasswordService>();

            // ============================================================
            // AUTENTICACIÓN LOCAL CON USUARIO + CONTRASEÑA
            // ============================================================

            var jwtKey = builder.Configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("No se configuró Jwt:Key.");

            var jwtIssuer = builder.Configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("No se configuró Jwt:Issuer.");

            var jwtAudience = builder.Configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("No se configuró Jwt:Audience.");

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey)
                        )
                    };
                });

            // ============================================================
            // MICROSOFT ENTRA ID — SUSPENDIDO TEMPORALMENTE
            // Se conserva como referencia para reactivarlo posteriormente.
            //
            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddMicrosoftIdentityWebApi(
            //         builder.Configuration.GetSection("AzureAd")
            //     );
            // ============================================================

            builder.Services.AddAuthorization();

            // ============================================================
            // CONTROLLERS
            // ============================================================

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            // ============================================================
            // PIPELINE
            // ============================================================

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}