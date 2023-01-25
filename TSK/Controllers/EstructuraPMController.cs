using Microsoft.AspNetCore.Mvc;
using TSK.Models.Entity;
using TSK.Models;
using Microsoft.AspNetCore.Authorization;

namespace TSK.Controllers
{
    [Authorize]
    public class EstructuraPMController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public EstructuraPMController(USAEU2GIGDEVSQLContext context)
        {
            _context = context;
        }

        public IActionResult Actividad()
        {
            @ViewBag.epm = "active";
            @ViewBag.actividad = "active";
            return View();
        }

        public IActionResult Sistema()
        {
            @ViewBag.epm = "active";
            @ViewBag.sistema = "active";
            return View();
        }
        public IActionResult PM()
        {
            @ViewBag.epm = "active";
            @ViewBag.pm = "active";
            return View();
        }

        public IActionResult PMSistema()
        {
            @ViewBag.epm = "active";
            @ViewBag.pmsistema = "active";
            return View();
        }

        public IActionResult PMSistemaActividad()
        {
            @ViewBag.epm = "active";
            @ViewBag.pmsistemaactividad = "active";
            return View();
        }

        public IActionResult Reporte()
        {
            @ViewBag.epm = "active";
            @ViewBag.reporte = "active";
            return View();
        }


        public IActionResult ReporteDetalle(int id)
        {
            var data = ReporteData(id);
            @ViewBag.epm = "active";
            @ViewBag.reporte = "active";

            return View(data);

        }



        public Query ReporteData(int id)
        {
            try
            {
                var _queryreporte = new Query();

                var _query = (from r in _context.Reportes
                          join pm in _context.Pms on r.IdPm equals pm.IdPm
                          join uni in _context.Unidads on r.IdUni equals uni.IdUni
                          join flo in _context.Flota on uni.IdFlt equals flo.IdFlt

                          where r.IdRep == id
                          orderby r.IdRep
                          select new Query()
                          {
                              id = r.IdRep,
                              texto1 = pm.Descripcion,
                              texto2 = uni.Unidad1 + " - " + flo.Flota,
                              texto5 = r.Comentario,
                              fecha1 = r.Fecha,
                              valor1 = r.Horometro,
                              texto6 = r.Comentario,
                              habilitado = r.Habilitado,
                              creado = r.Creado
                          }).ToList();

                _queryreporte = _query[0];

            

            return _queryreporte;
            }
            catch (Exception)
            {

                throw;
            }


            

        }

    }
}
