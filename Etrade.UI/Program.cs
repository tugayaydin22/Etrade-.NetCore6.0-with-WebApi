using Etrade.DAL.Abstract;
using Etrade.DAL.Concrete;
using Etrade.Entities.Context;
using Etrade.Entities.Models.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection
builder.Services.AddDbContext<EtradeContext>();
builder.Services.AddScoped<ICategoryDAL,CategoryDAL>();
builder.Services.AddScoped<IProductDAL,ProductDAL>();
builder.Services.AddScoped<IOrderDAL,OrderDAL>();

//Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);//kitilendiðinde
    options.Lockout.MaxFailedAccessAttempts = 5;//kilitleme adedi
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
}).AddEntityFrameworkStores<EtradeContext>()
.AddDefaultTokenProviders();

//cookie ayarý
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";//Giriþ yapýlmadýysa
    options.AccessDeniedPath = "/";//Yetkisi yoksa
    options.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityExampleCookie",
        HttpOnly = false,
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.Always//Https üzerinden
    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
});
//Session
builder.Services.AddSession();

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

//Identity ve cookie için 
app.UseAuthentication();//Önce giriþ kontrolü
app.UseAuthorization();//Sonra Yetki kontrolü

//Use Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
