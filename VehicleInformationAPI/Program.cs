using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.DataLayer;

using VehicleInformationAPI.DataLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Repositories;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => 
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleInformation API", Version = "v1" });
    config.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Description = "Enter the Bearer Token : Copy directly from the authorize endpoint",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
            },
            new[] { $"api://{configuration["AzureAd:ClientId"]}/default"} //Scope details
        }
    });

    //config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    //{
    //    Description = "OAuth2.0 Auth Code with PKCE",
    //    Name = "oauth2",
    //    Type = SecuritySchemeType.OAuth2,
    //    Flows = new OpenApiOAuthFlows
    //    {
    //        AuthorizationCode = new OpenApiOAuthFlow
    //        {
    //            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
    //            TokenUrl = new Uri($"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),//token end point
    //            Scopes = new Dictionary<string, string>
    //                            {
    //                                { $"api://{configuration["AzureAd:ClientId"]}/App.Read", "Read access to VehicleInformation API" },
    //                                { $"api://{configuration["AzureAd:ClientId"]}/App.Write", "Write access to VehicleInformation API" }
    //                            }
    //        }
    //    }
    //});
    //config.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
    //        },
    //        new[] { $"api://{configuration["AzureAd:ClientId"]}/App.Read",
    //            $"api://{configuration["AzureAd:ClientId"]}/App.Write" } //Scope details
    //    }
    //});
});

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration);
// Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
///***********
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>

        {
            configuration.Bind("AzureAd", options);
            options.Events = new JwtBearerEvents();

            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                    // Access the scope claim (scp) directly
                    var scopeClaim = context.Principal?.Claims.FirstOrDefault(c => c.Type == "scp")?.Value;

                    if (scopeClaim != null)
                    {
                        logger.LogInformation("Scope found in token: {Scope}", scopeClaim);
                    }
                    else
                    {
                        logger.LogWarning("Scope claim not found in token.");
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError("Authentication failed: {Message}", context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError("Challenge error: {ErrorDescription}", context.ErrorDescription);
                    return Task.CompletedTask;
                }
            };
        }, options => { configuration.Bind("AzureAd", options); });
//*******/
// The following flag can be used to get more descriptive errors in development environments
IdentityModelEventSource.ShowPII = false;

builder.Services.AddScoped<IVehicleInformationService, VehicleInformationService>();
builder.Services.AddScoped<IVehicleInformationRepository, VehicleInformationRepository>();
builder.Services.AddScoped<IReadFromCsv, ReadFromCsv>();
builder.Services.AddScoped<IMyMapper, MyMapper>();
builder.Services.AddScoped<ISaveToFileStorage, SaveToFileStorage>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleInformationDb")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    //{
    //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //    options.RoutePrefix = string.Empty;
    //});
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleInformation API v1");
        options.OAuthClientId(configuration["AzureAd:SwaggerClientId"]);
        options.OAuthUsePkce(); // Enables PKCE flow for security
        options.OAuthScopeSeparator(" ");
    }
                    );
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
