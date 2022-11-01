//using ValensSurveyManagementAPI.Extension;

using ValensSurveyManagementAPI.Context;
using ValensSurveyManagementAPI.Contracts;
using ValensSurveyManagementAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ValensSurveyManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<AccesTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
builder.Configuration.Bind("Authentication", authenticationConfiguration);
builder.Services.AddSingleton(authenticationConfiguration);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

});
builder.Services.AddOptions();
builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication"));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Authentication:AccessTokenSecret").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

