using SmartManager.Domain.Entities;
using SmartManager.Services.Services;
using SmartManager.Services.Interfaces;
using SmartManager.Infra.Interfaces;
using SmartManager.Infra.Repositories;
using AutoMapper;
using SmartManager.Services.DTOS;
using SmartManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using SmartManager.Services.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<SmartManagerContext>(options => options
                .UseMySql("Server=localhost;Database=SmartManager;Uid=root;Pwd=;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql")));




var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserDTO>().ReverseMap();
    cfg.CreateMap<UserDTO, createUserRequest>().ReverseMap();
    cfg.CreateMap<UserDTO, AuthenticateRequest>().ReverseMap();
    cfg.CreateMap<User, AuthenticateRequest>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
