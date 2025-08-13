using Application;
using Application.Dto.JWT;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtData = builder.Configuration.GetSection("JWTConfig").Adapt<JWTConfigMapperDto>();
jwtData.SECRET_KEY = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new Exception();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtData.SECRET_KEY)),
            ValidIssuer = jwtData.Issuer,
            ValidateIssuer = true,
            ValidAudience = jwtData.Audience,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

builder.Services.Scan(t =>
{
    t.FromAssemblyOf<IBaseApplicationRepository>()
    .AddClasses()
    .AsSelf()
    .AsImplementedInterfaces()
    .WithScopedLifetime();
});
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
