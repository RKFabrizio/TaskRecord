using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TSK.Models.Entity;
using TSK.Models;

namespace TSK.Controllers
{
    public class TaskRecordController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public TaskRecordController(USAEU2GIGDEVSQLContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult RegistroNuevo(string val)
        {
            @ViewBag.listataskrecord = "active";
            return View();
        }


        [Authorize]
        public IActionResult Registro()
        {
            @ViewBag.taskrecord = "active";
            return View();
        }

        public IActionResult RegistroDetalle(int id)
        {
            var data = ReporteData(id);
            @ViewBag.taskrecord = "active";

            return View(data);

        }

        public IActionResult VerDetalle(int id)
        {
            var data = ReporteData(id);
            @ViewBag.taskrecord = "active";

            return View(data);

        }

        public IActionResult Reporte()
        {
            @ViewBag.taskrecord = "active";
            @ViewBag.reporte = "active";
            return View();
        }

        public IActionResult ReporteDetalle(int id)
        {
            var data = ReporteData(id);
            @ViewBag.taskrecord = "active";
            @ViewBag.reporte = "active";

            return View(data);
        }

            public Query ReporteData(int id)
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


    }
}
