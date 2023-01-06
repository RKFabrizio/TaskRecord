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
    public class ActividadController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public ActividadController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var actividads = _context.Actividads.Select(i => new {
                i.IdAct,
                i.IdCon,
                i.IdClm,
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
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
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
        public async Task<IActionResult> ClaseMantencionsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ClaseMantencions
                         orderby i.Nombre
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
                         select new {
                             Value = i.IdFrc,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
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
            string TITULO = nameof(Actividad.Titulo);
            string ID_UM = nameof(Actividad.IdUm);
            string ID_FRC = nameof(Actividad.IdFrc);
            string ID_RAN = nameof(Actividad.IdRan);
            string ESPECIFICACION = nameof(Actividad.Especificacion);
            string INICIO = nameof(Actividad.Inicio);
            string FIN = nameof(Actividad.Fin);
            string REFERENCIA = nameof(Actividad.Referencia);
            string REFERENCIAURL = nameof(Actividad.ReferenciaUrl);
            string HABILITADO = nameof(Actividad.Habilitado);
            string EXTRACOLUMN1 = nameof(Actividad.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Actividad.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Actividad.Extracolumn3);

            if(values.Contains(ID_ACT)) {
                model.IdAct = Convert.ToInt32(values[ID_ACT]);
            }

            if(values.Contains(ID_CON)) {
                model.IdCon = Convert.ToString(values[ID_CON]);
            }

            if(values.Contains(ID_CLM)) {
                model.IdClm = Convert.ToString(values[ID_CLM]);
            }

            if(values.Contains(TITULO)) {
                model.Titulo = Convert.ToString(values[TITULO]).ToUpper();
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
                model.Especificacion = Convert.ToString(values[ESPECIFICACION]).ToUpper();
            }

            if(values.Contains(INICIO)) {
                model.Inicio = Convert.ToSingle(values[INICIO], CultureInfo.InvariantCulture);
            }

            if(values.Contains(FIN)) {
                model.Fin = Convert.ToSingle(values[FIN], CultureInfo.InvariantCulture);
            }

            if(values.Contains(REFERENCIA)) {
                model.Referencia = Convert.ToString(values[REFERENCIA]).ToUpper();
            }

            if (values.Contains(REFERENCIAURL))
            {
                model.ReferenciaUrl = Convert.ToString(values[REFERENCIAURL]);
            }

            if (values.Contains(HABILITADO)) {
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