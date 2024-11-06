using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ReservaCanchas.Data;
using ReservaCanchas.Models;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReservaCanchas.Controllers
{
    public class AuthController : Controller
    {
        private readonly ReservaCanchasContext _context;

        public AuthController(ReservaCanchasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string password)
        {
            var usuario = _context.Usuario.SingleOrDefault(u => u.Correo == correo);
            if (usuario != null && usuario.Password == password)
            {
                var tipoUsuario = usuario.TipoUsuario?.FirstOrDefault() ?? "Corriente";
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("Correo", usuario.Correo),
                    new Claim("TipoUsuario", tipoUsuario)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (_context.Usuario.Any(u => u.Correo == usuario.Correo))
            {
                ViewBag.Error = "El correo electrónico ya está registrado.";
                return View(usuario);
            }

            var passwordValid = Regex.IsMatch(usuario.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");
            if (!passwordValid)
            {
                ViewBag.Error = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número.";
                return View(usuario);
            }

            if (ModelState.IsValid)
            {
                usuario.TipoUsuario = new List<string> { "Corriente" }; // Asigna una lista con el valor "Corriente"
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
