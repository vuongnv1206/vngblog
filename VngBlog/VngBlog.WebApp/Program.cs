﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using VngBlog.Domain.Entities.Systems;
using VngBlog.Infrastructure;
using VngBlog.Infrastructure.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAuthentication().AddGoogle(options =>
{
    IConfigurationSection authenticationSection = builder.Configuration.GetSection("Authentication");
    string googleClientId = authenticationSection["Google:ClientId"];
    string googleClientSecret = authenticationSection["Google:ClientSecret"];
    options.ClientId = googleClientId;
    options.ClientSecret = googleClientSecret;
    //options.CallbackPath = "/Auth/Signin-google";
});

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
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"Uploads")),  //1 đg đãn bởi đg dẫn hiện tại đến thư mục Uploads
    RequestPath = "/contents"   //Địa chỉ tùy chúng ta thiết lập 
    // /contents/1.jpg => Uploads/1.jpg
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapRazorPages();
app.Run();
