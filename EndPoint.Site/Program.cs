using Bugeto_Store.Application.Services.Common.Queries.GetSlider;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.contexts;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Carts;
using Store.Application.Services.Common.Queries.GetCategory;
using Store.Application.Services.Common.Queries.GetHomePageImages;
using Store.Application.Services.Common.Queries.GetMenuItem;
using Store.Application.Services.Fainances.Commands.AddRequestPay;
using Store.Application.Services.Fainances.Queries.GetRequestPayForAdmin;
using Store.Application.Services.Fainances.Queries.GetRequestPayService;
using Store.Application.Services.HomePages.commands.EditHomePageImage;
using Store.Application.Services.HomePages.commands.EditSlider;
using Store.Application.Services.HomePages.commands.RemoveImage;
using Store.Application.Services.HomePages.commands.RemoveSlider;
using Store.Application.Services.HomePages.Commands.AddHomePageImages;
using Store.Application.Services.HomePages.Commands.AddNewSlider;
using Store.Application.Services.HomePages.Queries.GetImagesAdmin;
using Store.Application.Services.HomePages.Queries.GetSliders;
using Store.Application.Services.Orders.Commands.AddNewOrder;
using Store.Application.Services.Orders.Queries.GetOrdersForAdmin;
using Store.Application.Services.Orders.Queries.GetUserOrders;
using Store.Application.Services.Products.FacadPattern;
using Store.Application.Services.Users.commands.EditUser;
using Store.Application.Services.Users.commands.RegisterUser;
using Store.Application.Services.Users.commands.RemoveUser;
using Store.Application.Services.Users.commands.UserSatusChange;
using Store.Application.Services.Users.Commands.UserLogin;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Common.Roles;
using Store.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Authorization policies (roles)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
    options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
});

// Authentication with Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // اگر کسی لاگین نکرده بود و وارد بخش ادمین شد
        options.LoginPath = "/Admin/AdminAccount/Login";

        // اگر دسترسی نداشت (مثلا کاربر عادی وارد ادمین شد)
        options.AccessDeniedPath = "/Admin/AdminAccount/Login";

        // مدت زمان اعتبار کوکی
        options.ExpireTimeSpan = TimeSpan.FromDays(5);
    });

// for services
string contectionString = @"Data Source=DESKTOP-8ILS0U2; Initial Catalog=StoreDb; Integrated Security=True; TrustServerCertificate=True";
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<DataBaseContext>(options =>
        options.UseSqlServer(contectionString));
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IUserSatusChangeService, UserSatusChangeService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();

builder.Services.AddScoped<IGetMenuItemService, GetMenuItemService>();
builder.Services.AddScoped<IGetCategoryService, GetCategoryService>();
builder.Services.AddScoped<IAddNewSliderService, AddNewSliderService>();
builder.Services.AddScoped<IGetSliderService, GetSliderService>();
builder.Services.AddScoped<IAddHomePageImagesService, AddHomePageImagesService>();
builder.Services.AddScoped<IGetHomePageImagesService, GetHomePageImagesService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAddRequestPayService, AddRequestPayService>();
builder.Services.AddScoped<IGetRequestPayService, GetRequestPayService>();
builder.Services.AddScoped<IAddNewOrderService, AddNewOrderService>();
builder.Services.AddScoped<IGetUserOrdersService, GetUserOrdersService>();
builder.Services.AddScoped<IGetOrdersForAdminService, GetOrdersForAdminService>();
builder.Services.AddScoped<IGetRequestPayForAdminService, GetRequestPayForAdminService>();
builder.Services.AddScoped<IGetSlidersService, GetSlidersService>();
builder.Services.AddScoped<IRemoveSliderService, RemoveSliderService>();
builder.Services.AddScoped<IGetImagesAdminService, GetImagesAdminService>();
builder.Services.AddScoped<IRemoveImageService, RemoveImageService>();
builder.Services.AddScoped<IEditSliderService, EditSliderService>();
builder.Services.AddScoped<IEditHomePageImageService, EditHomePageImageService>();

// Facad inject 
builder.Services.AddScoped<IProductFacad, ProductFacad>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Area routes (Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
