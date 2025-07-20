using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.contexts;
using Store.Application.Services.Queries.GetUsers;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string contectionString = @"Data Source=DESKTOP-8ILS0U2; Initial Catalog=StoreDb; Integrated Security=True; TrustServerCertificate=True";
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<DataBaseContext>(options =>
        options.UseSqlServer(contectionString));
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
