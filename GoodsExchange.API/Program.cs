using FluentValidation.AspNetCore;
using GoodsExchange.API.StartConfigurations;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GoodsExchange.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            #region Authenticate with JWTBearer

            string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = builder.Configuration.GetValue<string>("Tokens:Key");
            
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });

            #endregion

            // Swagger configuration
            builder.Services.ConfigSwaggerOptions();

            // DI for FluentValidation
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();


            // DI for DbContext
            builder.Services.AddDbContext<GoodsExchangeDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Register services with DI
            builder.Services.RegisterService();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
