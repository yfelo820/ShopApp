using DAL.DB_Context;
using Microsoft.EntityFrameworkCore;
using SL.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Shared.Utils;

IConfiguration configuration = new ConfigurationBuilder()
                                    .AddJsonFile(Constans.APPSETTINGS_JSON)
                                    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(Constans.DEFAULT_CONNECTION),
                         b => b.MigrationsAssembly(Constans.MIGRATION_ASSEMBLY_CL));
});

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IShopService, ShopService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(Constans.SWAGGER_VERSION, new OpenApiInfo
    {
        Title = Constans.SWAGGER_TITLE,
        Version = Constans.SWAGGER_VERSION
    });
});

builder.Services.AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection(Constans.JWT_KEY).Value))
                    };
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(Constans.SWAGGER_ENDPOINT, Constans.SWAGGER_ENDPOINT_NAME);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
