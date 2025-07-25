
using LuftBornTask.Infrastructure.Implementation.Services;
using LuftBornTask.Infrastructure.Implementation.Repository;
using LuftBornTask.Application.Interfaces.Services;
using LuftBornTask.Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using LuftBornTask.Infrastructure.Context;
using LuftBornTask.Application.Interfaces.UnitOfWork;
using LuftBornTask.Infrastructure.Implementation.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LuftBornTask.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IProductRepository, ProductEFRepo>();
            builder.Services.AddScoped<IProductService, OrdinaryProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //this is only the configuration needed whe working with apis and spa 
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-rwd7fqhnvxkbfppi.us.auth0.com/";
                    options.Audience = "https://localhost:7188"; 
                });


            //this commented configuration is dealt with when using apps as mvc, u must install mMicrosoft.OpenIdConnect library
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //})
            //.AddCookie(options =>
            //{
            //    options.Cookie.SameSite = SameSiteMode.None;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //})
            //.AddOpenIdConnect("Auth0", options =>
            //{
            //    options.Authority = "https://dev-rwd7fqhnvxkbfppi.us.auth0.com";
            //    options.ClientId = "gDQTGGpU3oalNtd72ahgGypCyDzkDT9g";
            //    options.ClientSecret = "qskeXdSneoqNDX2IHcNHSqeboeaMO6K8P3xtgOjKf6R1ytwLbcBEVPR--O1JXO9A";
            //    options.ResponseType = "code";

            //    options.CallbackPath = new PathString("/signin-oidc");
            //    options.SignedOutRedirectUri = "https://localhost:4200/login";

            //    options.SaveTokens = true;

            //    options.Scope.Clear();
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("email");

            //    options.ClaimsIssuer = "Auth0";
            //});

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("https://localhost:4200") 
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); 
                });
            });
            var app = builder.Build();
            using(var scope = app.Services.CreateScope())
{
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Migration failed: {ex.Message}");
                    throw; 
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAngularApp");

            app.UseAuthentication(); 
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
