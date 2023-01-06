using Microsoft.AspNetCore.Mvc;
using TSK.Data;
using TSK.Models;
using System.Text;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace TSK.Controllers
{
    public class AccesoController : Controller
    {

        UsuarioDatos _UsuarioDatos = new UsuarioDatos();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario _usuario)
        {
            var usuario = _UsuarioDatos.ValidarUsuario(_usuario.UserName, ConvertirSha256(_usuario.Clave));

            if (usuario != null)
            {
                var claims = new List<Claim>
                {   new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("Usuario", usuario.UserName),
                    new Claim("NombreRol", usuario.Roles[0])
                };

                foreach (string rol in usuario.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Bienvenida", "Inicio");
            }
            else
            {
                @ViewBag.msg = "Error de usuario o clave";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }



    }





}
