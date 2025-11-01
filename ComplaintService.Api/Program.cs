using ComplaintService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComplaintService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //Database Configuration
        builder.Services.AddDbContext<ComplaintDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // controllers Configuration
        builder.Services.AddControllers();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}