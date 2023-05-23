using CI_SkillMaster.Utility.Extension;
using CI_SkillMaster.Utility.Filter;
using CISkillMaster.DataAccessLayer.Data;
using CISkillMaster.Services.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Volunteer/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
//app.UseMiddleware<AppExceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Volunteer}/{controller=User}/{action=Login}/{id?}");

app.Run();
