using Microsoft.AspNetCore.Mvc;
using ReservaCanchas.Data;
using ReservaCanchas.Models;
using System.Linq;

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
        public IActionResult Login(string correo, string password)
        {
            var usuario = _context.Usuario.SingleOrDefault(u => u.Correo == correo);
            if (usuario != null && usuario.Password == password)
                return RedirectToAction("Index", "Home");

            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.TipoUsuario = new List<string> { "Corriente" }; // Asigna una lista con el valor "Corriente"
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(usuario);
        }


    }
}