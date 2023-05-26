using CI_SkillMaster.Utility;
using CI_SkillMaster.Utility.Extension;
using CI_SkillMaster.Utility.Filter;
using CISkillMaster.DataAccessLayer.Data;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
   {
       options.SaveToken = true;
       options.RequireHttpsMetadata = false;
       options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ClockSkew = TimeSpan.Zero,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidAudience = builder.Configuration["JwtConfig:Issuer"],
           ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"]))
       };

       options.Events = new JwtBearerEvents()
       {
           OnAuthenticationFailed = context =>
           {
               if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                   context.Response.Headers.Add("token-expired", "true");
               return Task.CompletedTask;
           }
       };
   });

builder.Services.AddDbContext<CIDbContext>();

builder.Services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Service Dependency Helper 
builder.Services.ServiceDependencyHelper();

//Repository Dependency Helper
builder.Services.RepositoryDependencyHelper();
builder.Services.AddSession();
builder.Services.AddTransient<GlobalExceptionAttribute>();
builder.Services.AddTransient<AjaxExceptionAttribute>();
//builder.Services.AddScoped<AppExceptionMiddleware>();

//Repository Service Helper
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Volunteer/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseSession();
app.UseMiddleware<JwtMiddleware>();
//app.UseAuthorization();
//app.UseMiddleware<AppExceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Volunteer}/{controller=User}/{action=Login}/{id?}");

app.Run();
