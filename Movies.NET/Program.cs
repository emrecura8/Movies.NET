using Business.Services;
using DataAccess.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Movies.NET.Settings;

var builder = WebApplication.CreateBuilder(args);

#region AppSettings
builder.Configuration.GetSection(nameof(AppSettings)).Bind(new AppSettings());
#endregion

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Account/Home/Login";
    options.AccessDeniedPath = "/Account/Home/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});
#endregion

#region Session
builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30);
    config.IOTimeout = Timeout.InfiniteTimeSpan;
});
#endregion

// Add services to the container.
#region IoC Container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Db>(options=>options.UseSqlServer(connectionString));

builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<IDirectorService,DirectorService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IMovieService,MovieService>();
#endregion

builder.Services.AddControllersWithViews();

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

#region Authentication
app.UseAuthentication();
#endregion

app.UseAuthorization();

#region Session
app.UseSession();
#endregion

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
