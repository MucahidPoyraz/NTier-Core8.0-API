var builder = WebApplication.CreateBuilder(args);

// Razor View'lar için runtime derleme
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpClient();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Hata sayfası ayarı
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();


// 🟢 Ana URL geldiğinde Web area'ya yönlendir
app.MapGet("/", context =>
{
    context.Response.Redirect("/Web/Home");
    return Task.CompletedTask;
});

// Area route
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Varsayılan route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
