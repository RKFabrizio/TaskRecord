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
    public class ActividadsController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public ActividadsController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var actividads = _context.Actividads.Select(i => new {
                i.IdAct,
                i.IdCon,
                i.IdClm,
                i.IdDis,
                i.Titulo,
                i.IdUm,
                i.IdFrc,
                i.IdRan,
                i.Especificacion,
                i.Inicio,
                i.Fin,
                i.Referencia,
                i.ReferenciaUrl,
                i.Habilitado,
                i.Descripcion,
                i.Duracion,
                i.SubActividad
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdAct" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(actividads, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Actividad();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Actividads.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdAct });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Actividads.FirstOrDefaultAsync(item => item.IdAct == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Actividads.FirstOrDefaultAsync(item => item.IdAct == key);

            _context.Actividads.Remove(model);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IActionResult> ActividadNombre(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Actividads
                         orderby i.Titulo
                         where i.Habilitado == true
                         select new
                         {
                             Value = i.IdAct,
                             Text = i.Titulo
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> DisciplinasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Disciplinas
                         orderby i.Nombre
                         where i.Habilitado == true
                         select new {
                             Value = i.IdDis,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> DisciplinasFilterLookup(int IdAct, DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Actividads
                         from d in _context.Disciplinas
                         where i.IdDis == d.IdDis && i.IdAct == IdAct && i.Habilitado == true
                         
                         select new
                         {
                             IdDis = d.IdDis,
                             Nombre = d.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ClaseMantencionsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ClaseMantencions
                         orderby i.Nombre
                         where i.Habilitado == true
                         select new {
                             Value = i.IdClm,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ConsecuenciaLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Consecuencia
                         orderby i.Nombre
                         where i.Habilitado == true
                         select new {
                             Value = i.IdCon,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> FrecuenciaLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Frecuencia
                         orderby i.IdFrc
                         where i.Habilitado == true
                         select new {
                             Value = i.IdFrc,
                             Text = i.Valor
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup.OrderBy(x => x.Value), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> RangosLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Rangos
                         orderby i.Nombre
                         select new {
                             Value = i.IdRan,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UnidadMedidaLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.UnidadMedida
                         orderby i.Nombre
                         where i.Habilitado == true
                         select new {
                             Value = i.IdUm,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Actividad model, IDictionary values) {
            string ID_ACT = nameof(Actividad.IdAct);
            string ID_CON = nameof(Actividad.IdCon);
            string ID_CLM = nameof(Actividad.IdClm);
            string ID_DIS = nameof(Actividad.IdDis);
            string TITULO = nameof(Actividad.Titulo);
            string ID_UM = nameof(Actividad.IdUm);
            string ID_FRC = nameof(Actividad.IdFrc);
            string ID_RAN = nameof(Actividad.IdRan);
            string ESPECIFICACION = nameof(Actividad.Especificacion);
            string INICIO = nameof(Actividad.Inicio);
            string FIN = nameof(Actividad.Fin);
            string REFERENCIA = nameof(Actividad.Referencia);
            string REFERENCIA_URL = nameof(Actividad.ReferenciaUrl);
            string HABILITADO = nameof(Actividad.Habilitado);
            string DESCRIPCION = nameof(Actividad.Descripcion);
            string DURACION = nameof(Actividad.Duracion);
            string SUBACTIVIDAD = nameof(Actividad.SubActividad);

            if(values.Contains(ID_ACT)) {
                model.IdAct = Convert.ToInt32(values[ID_ACT]);
            }

            if(values.Contains(ID_CON)) {
                model.IdCon = Convert.ToString(values[ID_CON]);
            }

            if(values.Contains(ID_CLM)) {
                model.IdClm = Convert.ToString(values[ID_CLM]);
            }

            if(values.Contains(ID_DIS)) {
                model.IdDis = values[ID_DIS] != null ? Convert.ToInt32(values[ID_DIS]) : (int?)null;
            }

            if(values.Contains(TITULO)) {
                model.Titulo = Convert.ToString(values[TITULO]);
            }

            if(values.Contains(ID_UM)) {
                model.IdUm = Convert.ToString(values[ID_UM]);
            }

            if(values.Contains(ID_FRC)) {
                model.IdFrc = values[ID_FRC] != null ? Convert.ToInt32(values[ID_FRC]) : (int?)null;
            }

            if(values.Contains(ID_RAN)) {
                model.IdRan = values[ID_RAN] != null ? Convert.ToInt32(values[ID_RAN]) : (int?)null;
            }

            if(values.Contains(ESPECIFICACION)) {
                model.Especificacion = Convert.ToString(values[ESPECIFICACION]);
            }

            if(values.Contains(INICIO)) {
                model.Inicio = values[INICIO] != null ? Convert.ToDouble(values[INICIO], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(FIN)) {
                model.Fin = values[FIN] != null ? Convert.ToDouble(values[FIN], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(REFERENCIA)) {
                model.Referencia = Convert.ToString(values[REFERENCIA]);
            }

            if(values.Contains(REFERENCIA_URL)) {
                model.ReferenciaUrl = Convert.ToString(values[REFERENCIA_URL]);
            }

            if(values.Contains(HABILITADO)) {
                model.Habilitado = values[HABILITADO] != null ? Convert.ToBoolean(values[HABILITADO]) : (bool?)null;
            }

            if(values.Contains(DESCRIPCION)) {
                model.Descripcion = Convert.ToString(values[DESCRIPCION]);
            }

            if (values.Contains(DURACION))
            {
                model.Duracion = values[DURACION] != null ? Convert.ToDecimal(values[DURACION], CultureInfo.InvariantCulture) : (decimal?)null;
            }


            if (values.Contains(SUBACTIVIDAD)) {
                model.SubActividad = Convert.ToString(values[SUBACTIVIDAD]);
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