using System.Text;
using System.Text.Json.Serialization;
using LogisticsService.Application.Interfaces;
using LogisticsService.Application.Services;
using LogisticsService.Infrastructure;
using LogisticsService.Infrastructure.Interfaces;
using LogisticsService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<LogisticsDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // This ensures enums are serialized/deserialized as strings
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Logistics Service",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header, // header
        Description = "Enter 'Bearer' [space] and then your valid token.",
        Name = "Authorization", // header name
        Type = SecuritySchemeType.Http, // must be ApiKey for Bearer in header
        BearerFormat = "JWT", // optional, indicates JWT tokens
        Scheme = "bearer" // the "scheme" name
    });
    options.AddSecurityRequirement(document => new()
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true, // Ensure the token hasn't expired
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            ),
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token successfully validated!");
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

// Registering my services
builder.Services.AddScoped<ILoadsRepository, LoadsRepository>();
builder.Services.AddScoped<ILoadsService, LoadsService>();
builder.Services.AddScoped<IVehiclesRepository, VehiclesRepository>();
builder.Services.AddScoped<IVehiclesService, VehiclesService>();

// Registering Grpc service
builder.Services.AddGrpcClient<IdentityService.IdentityServiceClient>(options =>
{
    options.Address = new Uri("https://localhost:7078");
    //options.Address = new Uri("http://localhost:5138");
});



var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Logistics Service v1");
    });
}


app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();


/*"loadStatus": "Draft",
"cargoType": "Dry",
"location": "string",
"pickupDate": "2026-02-15T06:46:20.047Z",
"weight": 10,
"delieveryLocation": "Location",
"delieveryContact": "Contact",
"delieveryInstructions": "Nothing Yet",
"deliveryDate": "2026-02-15T06:46:20.047Z",
"vehicleType": "Flatbed",
"isDeleted": False,
"createdByUserId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
"createdDate": "2026-02-15T06:46:20.047Z"*/


// 0x7Ff8bbf9C8AB106db589e7863fb100525F61CCe5