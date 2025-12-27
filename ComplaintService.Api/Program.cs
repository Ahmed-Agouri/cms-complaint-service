using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using ComplaintService.Api.Middleware;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Services;
using ComplaintService.Infrastructure.Clients;
using ComplaintService.Infrastructure.Data;
using ComplaintService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ComplaintService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ComplaintDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = false;
            });

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
        
        builder.Services.AddHttpClient<IAuditClient, AuditClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5124");
            client.BaseAddress = new Uri("http://localhost:5182");

        });
        builder.Services.AddScoped<IComplaintService, Application.Services.ComplaintService>();
        builder.Services.AddScoped<IResolutionService, ResolutionService>();
        builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
        

        var jwtKey = builder.Configuration["Jwt:Key"];
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtAudience = builder.Configuration["Jwt:Audience"];
        

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                        Encoding.UTF8.GetBytes(jwtKey!)
                    ),
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].ToString();
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ComplaintDbContext>();
                dbContext.Database.Migrate();
            }
        }

        app.UseCors("AllowAll");
        app.UseMiddleware<ApiKeyMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}