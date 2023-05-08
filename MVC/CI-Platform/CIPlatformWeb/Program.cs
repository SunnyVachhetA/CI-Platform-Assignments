using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new CleanDataModelBinderProvider());
                });

builder.Services.AddDbContext<CIDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceUnit, ServiceUnit>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<CIPlatformWeb.Areas.Admin.Utilities.AjaxErrorFilter>();
builder.Services.AddSession(); //Session Registration

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
app.UseSession(); //Session Used

app.MapControllerRoute(
    name: "default",
    //pattern: "{area=Admin}/{controller=Home}/{Action=Index}"
    pattern: "{area=Volunteer}/{controller=Home}/{action=Index}/{id?}"
);

app.Run();
