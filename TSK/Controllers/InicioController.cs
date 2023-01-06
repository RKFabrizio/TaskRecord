using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TSK.Controllers
{
    [Authorize]
    public class InicioController : Controller
    {

        public IActionResult Bienvenidaold()
        {
            @ViewBag.bienvenida = "active";
            return View();
        }
        public IActionResult Bienvenida()
        {
            @ViewBag.bienvenida = "active";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }
    }
}
