using ComplaintService.Application.Interfaces;
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
    
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
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
        builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();


        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors("AllowAll");

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}