using CI_SkillMaster.Utility.Extension;
using CISkillMaster.DataAccessLayer.Data;
using CISkillMaster.Services.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CIDbContext>();

builder.Services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

//Service Dependency Helper 
builder.Services.ServiceDependencyHelper();

//Repository Dependency Helper
builder.Services.RepositoryDependencyHelper();

//Repository Service Helper
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.Run();
