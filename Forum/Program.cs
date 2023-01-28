
using Forum.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<RequestLocalizationOptions>(options => { options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pl-PL"); });
builder.Services.AddDbContext<Forum.Context.ForumDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ForumConnectionString")));

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
