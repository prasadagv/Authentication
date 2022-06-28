using Authentication.Controllers;
using Authentication.Domain.Contracts;
using Authentication.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILogger, Logger<AuthenticationController>>();
builder.Services.AddSingleton<IAuthenticateService>(options =>
{
    var issuer = builder.Configuration.GetSection("JwtToken").GetValue<string>("Issuer");
    var audience = builder.Configuration.GetSection("JwtToken").GetValue<string>("Audience");
    var key = builder.Configuration.GetSection("JwtToken").GetValue<string>("Key");

    return new AuthenticateService(issuer, audience, key);
});

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
