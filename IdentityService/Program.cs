using System.Text;
using System.Text.Json.Serialization;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure;
using IdentityService.Infrastructure.Interfaces;
using IdentityService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Identity User Logic
builder.Services.AddIdentity<Users, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    // This ensures enums are serialized/deserialized as strings
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});;
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Registeting my services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Registering Grpc service
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    // Force listen on the HTTPS port
    options.ListenLocalhost(7078, listenOptions =>
    {
        listenOptions.UseHttps();
    });
    // Optional: Keep the HTTP port open too
    options.ListenLocalhost(5138); 
});

var app = builder.Build();

app.MapGrpcService<IdentityGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");
// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service v1");
    });
}

// Role Management
using (var scope = app.Services.CreateScope())
{
    await RoleSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


//app.UseHttpsRedirection();

app.Run();
