using Forum.Context;
using Forum.Contracts;
using Forum.Contracts.Manager;
using Forum.Repositories;
using Forum.Repositories.Manager;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.Configure<RequestLocalizationOptions>(options => { options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pl-PL"); });
builder.Services.AddSingleton<DefaultDbContext>();
builder.Services.AddScoped<IBaseMethodsRepository, BaseMethodsRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
builder.Services.AddScoped<ICRUD_Repository, CRUD_Repository>();
builder.Services.AddScoped<IAdministrationDashboardRepository, AdministrationDashboardRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
{
    options.Cookie.Name = "YourCookieNameHere";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
