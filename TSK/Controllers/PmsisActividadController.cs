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
    public class PmsisActividadController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public PmsisActividadController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var pmsisactividads = _context.PmsisActividads.Select(i => new {
                i.IdAct,
                i.IdPms,
                i.Orden,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            }).OrderBy(x => x.IdPms).ThenBy(x => x.Orden);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdAct", "IdPms" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(pmsisactividads, loadOptions));
        }


        [HttpGet]
        public async Task<IActionResult> PmsisActividadesLookup(int idPms, DataSourceLoadOptions loadOptions)
        {
            //var acts = _context.PmsisActividads.Select(i => new
            //{
            //    i.IdPms,
            //    i.IdAct,
            //    i.Orden

            //}).Where(e => e.IdPms == idPms);

            var joinResult = _context.PmsisActividads.Join(_context.Actividads,
                pmsisAct => pmsisAct.IdAct,
                act => act.IdAct,
                (pmsisAct, act) => new { PmsisAct = pmsisAct, Actividad = act });

            var result = joinResult.Select(jr => new
            {
                jr.PmsisAct.IdPms,
                jr.PmsisAct.IdAct,
                jr.Actividad.Titulo,
                jr.PmsisAct.Orden,

            });



            return Json(await DataSourceLoader.LoadAsync(result, loadOptions));
        }



        //[HttpPost]
        //public async Task<IActionResult> Post(string values) {
        //    var model = new PmsisActividad();
        //    var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
        //    PopulateModel(model, valuesDict);

        //    if(!TryValidateModel(model))
        //        return BadRequest(GetFullErrorMessage(ModelState));

        //    var result = _context.PmsisActividads.Add(model);
        //    await _context.SaveChangesAsync();

        //    return Json(new { result.Entity.IdAct, result.Entity.IdPms });
        //}

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values) {
            var keys = JsonConvert.DeserializeObject<IDictionary>(key);
            var keyIdAct = Convert.ToInt32(keys["IdAct"]);
            var keyIdPms = Convert.ToInt32(keys["IdPms"]);
            var model = await _context.PmsisActividads.FirstOrDefaultAsync(item =>
                            item.IdAct == keyIdAct && 
                            item.IdPms == keyIdPms);
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
        public async Task Delete(string key) {
            var keys = JsonConvert.DeserializeObject<IDictionary>(key);
            var keyIdAct = Convert.ToInt32(keys["IdAct"]);
            var keyIdPms = Convert.ToInt32(keys["IdPms"]);
            var model = await _context.PmsisActividads.FirstOrDefaultAsync(item =>
                            item.IdAct == keyIdAct && 
                            item.IdPms == keyIdPms);

            _context.PmsisActividads.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> ActividadsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Actividads
                         orderby i.IdCon
                         select new {
                             Value = i.IdAct,
                             Text = i.Titulo
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PmSistemasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from pmsis in _context.PmSistemas
                         join sis in _context.Sistemas on pmsis.IdSis equals sis.IdSis
                         join con in _context.Condicions on sis.IdCod equals con.IdCod
                         join pm in _context.Pms on pmsis.IdPm equals pm.IdPm
                         join flo in _context.Flota on pm.IdFlt equals flo.IdFlt
                         orderby pm.Descripcion + " " + pm.IdPm + " - " + flo.Flota + " - " + sis.Nombre + " - " + con.Nombre
                         select new {
                             Value = pmsis.IdPms,
                             Text = pm.Descripcion + " " + pm.IdPm + " - " + flo.Flota + " - " +  sis.Nombre + " - " + con.Nombre
                         };


            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));

        }

        private void PopulateModel(PmsisActividad model, IDictionary values) {
            string ID_ACT = nameof(PmsisActividad.IdAct);
            string ID_PMS = nameof(PmsisActividad.IdPms);
            string ORDEN = nameof(PmsisActividad.Orden);
            string HABILITADO = nameof(PmsisActividad.Habilitado);
            string EXTRACOLUMN1 = nameof(PmsisActividad.Extracolumn1);
            string EXTRACOLUMN2 = nameof(PmsisActividad.Extracolumn2);
            string EXTRACOLUMN3 = nameof(PmsisActividad.Extracolumn3);

            if(values.Contains(ID_ACT)) {
                model.IdAct = Convert.ToInt32(values[ID_ACT]);
            }

            if(values.Contains(ID_PMS)) {
                model.IdPms = Convert.ToInt32(values[ID_PMS]);
            }

            if(values.Contains(ORDEN)) {
                model.Orden = values[ORDEN] != null ? Convert.ToInt32(values[ORDEN]) : (int?)null;
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

        [HttpPost]
        public async Task<IActionResult> AddMultipleActivities(int idPms, List<PmsisActividad> activities)
        {
            // Validar que el idPms exista en la base de datos
            var pms = await _context.PmsisActividads.FindAsync(idPms);
            if (pms == null)
                return BadRequest("El IdPms especificado no existe en la base de datos.");

            // Asignar el idPms a cada actividad antes de agregarlas a la base de datos
            foreach (var act in activities)
                act.IdPms = idPms;

            // Agregar las actividades a la base de datos
            await _context.PmsisActividads.AddRangeAsync(activities);
            await _context.SaveChangesAsync();

            try
            {
                foreach (var activity in activities)
                {
                    activity.IdPms = idPms;
                    _context.PmsisActividads.Add(activity);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        public IActionResult EditActivity(PmsisActividad activity)
        {
            _context.PmsisActividads.Update(activity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}