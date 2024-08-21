using Listify.Domain.Services;
using Listify.Domain.Interfaces.Security;
using Listify.Security.Services;
using Listify.Security.Settings;
using Listify.Services.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddRouting(map => map.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection();
builder.Services.AddCorsConfig();

builder.Services.AddTransient<ITokenSecurity, TokenSecurity>();

builder.Services.AddAuthentication(
    auth =>
    {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
        bearer =>
        {
            bearer.RequireHttpsMetadata = false;
            bearer.SaveToken = true;
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.ASCII.GetBytes(TokenSettings.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
);

builder.Services.AddSingleton<SendEmailDomainService>(sp => new SendEmailDomainService(
    builder.Configuration["Smtp:Server"],
    int.Parse(builder.Configuration["Smtp:Port"]),
    builder.Configuration["Smtp:Username"],
    builder.Configuration["Smtp:Password"]
));

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseCorsConfig();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
