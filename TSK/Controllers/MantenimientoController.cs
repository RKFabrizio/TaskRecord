using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TSK.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        public IActionResult Equipo()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.Equipo = "active";
            return View();
        }

        public IActionResult Herramienta()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.herramienta = "active";
            return View();
        }

        public IActionResult Repuesto()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.repuesto = "active";
            return View();
        }

        public IActionResult Complemento()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.complemento = "active";
            return View();
        }

        public IActionResult Condicion()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.condicion = "active";
            return View();
        }

        public IActionResult Sector()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.sector = "active";
            return View();
        }

        public IActionResult Componente()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.componente = "active";
            return View();
        }



        public IActionResult Nivel()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.autenticacion = "active";
            @ViewBag.nivel = "active";
            return View();
        }

        public IActionResult Disciplina()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.autenticacion = "active";
            @ViewBag.disciplina = "active";
            return View();
        }
        public IActionResult Rol()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.autenticacion = "active";
            @ViewBag.rol = "active";
            return View();
        }

        public IActionResult Usuario()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.autenticacion = "active";
            @ViewBag.usuario = "active";
            return View();
        }

        

       

        public IActionResult Consecuencia()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.consecuencia = "active";
            return View();
        }

        
        public IActionResult ClaseMantencion()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.clasemantencion = "active";
            return View();
        }
        public IActionResult Frecuencia()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.frecuencia = "active";
            return View();
        }

        public IActionResult Rango()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.rango = "active";
            return View();
        }

        public IActionResult Tarea()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.tarea = "active";
            return View();
        }

        public IActionResult UnidadMedida()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.unidadmedida = "active";
            return View();
        }

        public IActionResult Empresa()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.empresa = "active";
            return View();
        }
        public IActionResult TipoEquipo()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.tipoequipo = "active";
            return View();
        }
        public IActionResult Flota()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.flota = "active";
            return View();
        }

        public IActionResult Unidad()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.unidad = "active";
            return View();
        }

        

        public IActionResult ReporteSistema()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.reportesistema = "active";
            return View();
        }

        public IActionResult ReporteDetalle()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.reportedetalle = "active";
            return View();
        }

        public IActionResult Entrega()
        {
            @ViewBag.mantenimiento = "active";
            @ViewBag.entrega = "active";
            return View();
        }
    }
}
