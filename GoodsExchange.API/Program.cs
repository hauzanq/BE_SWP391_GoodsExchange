using GoodsExchange.API.Extensions;

namespace GoodsExchange.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddAuthenticationServicesConfigurations(builder.Configuration)
                            .AddSwaggerConfigurations()
                            .AddDbContextsWithConfigurations(builder.Configuration)
                            .RegisterService()
                            .AddCorsConfigurations();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowCORS");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
