using Forum.Context;
using Forum.Contracts;
using Forum.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
{
    options.Cookie.Name = "YourCookieNameHere";
    options.LoginPath = "/Account/Login";
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

app.MapRazorPages();

app.Run();
