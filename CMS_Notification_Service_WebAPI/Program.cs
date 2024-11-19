using CM05_Notication_DAL;
using CMS_Notication_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS_Notification_Service_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register DbContext
            builder.Services.AddDbContext<CMS_Notification_DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CMS_Notification_DB_ConnectionString")));

            // Register repository
            builder.Services.AddScoped<CM05_Notification_Repository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Replace with your Angular app's origin
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply CORS policy
            app.UseCors("AllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
