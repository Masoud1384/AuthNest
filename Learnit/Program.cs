using Application;
using Application.Dto.JWT;
using Application.Utility;
using Infrastructure;
using Mapster;
using AuthNest.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(JWTHelper.GetSecretKey().Bade64UrlDecode()),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
            ValidIssuer = builder.Configuration["JWTConfig:Issuer"],
            ValidateIssuer = true,
            ValidAudience = builder.Configuration["JWTConfig:Audience"],
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx => {
                Console.WriteLine(ctx.Exception);
                return Task.CompletedTask;
            },
            OnChallenge = ctx => {
                Console.WriteLine(ctx.Error +"------"+ ctx.ErrorDescription);
                return Task.CompletedTask;
            }
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
app.UseRouting();
app.Use(async (ctx, next) =>
{
    var token = ctx.Request.Headers.Authorization.ToString();
    Console.WriteLine(token);
    await next();
});



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthNestExceptionHandler();
app.MapControllers();

app.Run();
