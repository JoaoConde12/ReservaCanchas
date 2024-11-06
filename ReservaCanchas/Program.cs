using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReservaCanchas.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configurar el contexto de base de datos.
builder.Services.AddDbContext<ReservaCanchasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReservaCanchasContext") ?? throw new InvalidOperationException("Connection string 'ReservaCanchasContext' not found.")));

// Configurar la autenticación con cookies.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Ruta de la página de inicio de sesión.
        options.AccessDeniedPath = "/Auth/AccessDenied"; // Ruta cuando el acceso es denegado.
    });

// Configurar servicios para el uso de sesión.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Duración de la sesión.
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Añadir servicios al contenedor.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración del middleware.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configurar el middleware para usar autenticación y autorización.
app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); // Usar la sesión.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
