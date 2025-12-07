using System.Text.Json.Serialization;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Services;
using ComplaintService.Infrastructure.Data;
using ComplaintService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComplaintService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // For when i use azure hosted server/db
        // builder.Services.AddDbContext<ComplaintDbContext>(options =>
        //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
        builder.Services.AddDbContext<ComplaintDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    
        //Services
        builder.Services.AddScoped<IComplaintService, Application.Services.ComplaintService>();
        builder.Services.AddScoped<IResolutionService, ResolutionService>();

        builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();


        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ComplaintDbContext>();
                dbContext.Database.Migrate();
                Console.WriteLine("✅ Complaint Service: Database migrated successfully");
            }
        }
        
        app.UseCors("AllowAll");

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}