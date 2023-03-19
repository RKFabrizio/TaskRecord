using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TSK.Models.Entity;

namespace TSK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReporteController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public ReporteController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var reportes = _context.Reportes.Select(i => new {
                i.IdRep,
                i.IdPm,
                i.IdUni,
                i.Fecha,
                i.Horometro,
                i.Comentario,
                i.Creado,
                i.Avance,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdRep" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(reportes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Reporte();
            Console.WriteLine("HOLA" +values);
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Reportes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdRep });
        }


        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await _context.Reportes.FirstOrDefaultAsync(item => item.IdRep == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key)
        {
            var model = await _context.Reportes.FirstOrDefaultAsync(item => item.IdRep == key);

            _context.Reportes.Remove(model);
            await _context.SaveChangesAsync();
        }



        [HttpGet]
        public async Task<IActionResult> PmsLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from pm in _context.Pms
                         join flo in _context.Flota on pm.IdFlt equals flo.IdFlt
                         orderby pm.IdPm
                         where pm.Habilitado == true
                         select new
                         {
                             Value = pm.IdPm,
                             Text = pm.Nombre + " - " + flo.Flota + " - " + pm.Descripcion
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PmsLookup1(DataSourceLoadOptions loadOptions)
        {
            var lookup = from pm in _context.Pms
                         join flo in _context.Flota on pm.IdFlt equals flo.IdFlt
                         orderby pm.IdPm
                         where pm.Habilitado == true
                         select new
                         {
                             Value = pm.IdPm,
                             Text = pm.Nombre + " - " + flo.Flota + " - " + pm.Descripcion
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }


        [HttpGet]
        public async Task<IActionResult> UnidadsLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from uni in _context.Unidads
                         orderby uni.Unidad1
                         where uni.Habilitado==true
                         select new
                         {
                             Value = uni.IdUni,
                             Text = uni.Unidad1
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UnidadsFlotaLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from uni in _context.Unidads
                         join flo in _context.Flota on uni.IdFlt equals flo.IdFlt
                         orderby flo.Flota + " - " + uni.Unidad1
                         where uni.Habilitado == true
                         select new
                         {
                             Value = uni.IdUni,
                             Text = flo.Flota + " - " + uni.Unidad1
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UsuariosLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Usuarios
                         orderby i.Nombre
                         select new
                         {
                             Value = i.IdUsr,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

    

        private void PopulateModel(Reporte model, IDictionary values) {
            string ID_REP = nameof(Reporte.IdRep);
            string ID_PM = nameof(Reporte.IdPm);
            string ID_UNI = nameof(Reporte.IdUni);
            string FECHA = nameof(Reporte.Fecha);
            string HOROMETRO = nameof(Reporte.Horometro);
            string COMENTARIO = nameof(Reporte.Comentario);
            string CREADO = nameof(Reporte.Creado);
            string AVANCE = nameof(Reporte.Avance);
            string HABILITADO = nameof(Reporte.Habilitado);
            string EXTRACOLUMN1 = nameof(Reporte.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Reporte.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Reporte.Extracolumn3);

            if(values.Contains(ID_REP)) {
                model.IdRep = Convert.ToInt32(values[ID_REP]);
            }

            if(values.Contains(ID_PM)) {
                model.IdPm = Convert.ToInt32(values[ID_PM]);
            }

            if(values.Contains(ID_UNI)) {
                model.IdUni = Convert.ToInt32(values[ID_UNI]);
            }

            if(values.Contains(FECHA)) {
                model.Fecha = values[FECHA] != null ? Convert.ToDateTime(values[FECHA]) : (DateTime?)null;
            }

            if(values.Contains(HOROMETRO)) {
                model.Horometro = values[HOROMETRO] != null ? Convert.ToInt32(values[HOROMETRO]) : (int?)null;
            }

            if(values.Contains(COMENTARIO)) {
                model.Comentario = Convert.ToString(values[COMENTARIO]);
            }

            if(values.Contains(CREADO)) {
                model.Creado = values[CREADO] != null ? Convert.ToBoolean(values[CREADO]) : (bool?)null;
            }

            if(values.Contains(AVANCE)) {
                model.Avance = Convert.ToDouble(values[AVANCE], CultureInfo.InvariantCulture);
            }

            if(values.Contains(HABILITADO)) {
                model.Habilitado = values[HABILITADO] != null ? Convert.ToBoolean(values[HABILITADO]) : (bool?)null;
            }

            if(values.Contains(EXTRACOLUMN1)) {
                model.Extracolumn1 = Convert.ToString(values[EXTRACOLUMN1]);
            }

            if(values.Contains(EXTRACOLUMN2)) {
                model.Extracolumn2 = Convert.ToString(values[EXTRACOLUMN2]);
            }

            if(values.Contains(EXTRACOLUMN3)) {
                model.Extracolumn3 = Convert.ToString(values[EXTRACOLUMN3]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}