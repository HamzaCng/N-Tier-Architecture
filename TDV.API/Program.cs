using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TDV.Application;
using TDV.Application.Shared.Authentications;
using TDV.Application.Shared.Users;
using TDV.Application.Users;
using TDV.DataAccess.Abstract;
using TDV.DataAccess.Context;
using TDV.DataAccess.Repositories;
using Microsoft.OpenApi.Models;
using NLog.Web;
using NLog;
using TDV.API.Extensions; 

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger(); // NLog yapılandırması
try
{
    logger.Debug("Uygulama başlıyor");

    var builder = WebApplication.CreateBuilder(args);

    var jwtSettings = builder.Configuration.GetSection("JwtSettings");

    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddDbContext<TestDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
    });
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
    builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

    #region Services ______________________________________________________________

    builder.Services.AddScoped<IUserService, UserService>();

    #endregion

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(opt =>
    {
        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please Enter Token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

    builder.Services.AddAuthorization();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<TestDbContext>();

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseLog();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Uygulama başlatılamadı");
    throw;
}
finally
{
    LogManager.Shutdown(); // NLog'un düzgün kapanması için.
}
