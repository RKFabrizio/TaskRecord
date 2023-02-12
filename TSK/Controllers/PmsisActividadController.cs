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
using TSK.Models;
using TSK.Models.Entity;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using DevExpress.PivotGrid.OLAP;

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
        public async Task<IActionResult> PmsisActividadesLookup(int IdPms, DataSourceLoadOptions loadOptions) // nos vota 0 como id
        {

            var result = from pmsisActividad in _context.PmsisActividads
                         from actividad in _context.Actividads
                         where pmsisActividad.IdAct == actividad.IdAct && pmsisActividad.IdPms == IdPms
                         select new
                         {
                             IdPms = pmsisActividad.IdPms,
                             IdAct = pmsisActividad.IdAct,
                             NombreActividad = actividad.Titulo,
                             Orden = pmsisActividad.Orden,
                         };


            return Json(await DataSourceLoader.LoadAsync(result, loadOptions));
        }



        [HttpPost]
       
        public async Task<IActionResult> PmsisActividadesBatchPost(int idPms, string values)
        {
     
            var model = new PmsisActividad { IdPms = idPms };
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.PmsisActividads.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdAct, result.Entity.IdPms });
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new PmsisActividad();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.PmsisActividads.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    return BadRequest("Ya existe una actividad con el mismo IdAct e IdPms. Por favor, proporcione valores únicos para estos campos.");
                }
            }

            return Json(new { result.Entity.IdAct, result.Entity.IdPms });
        }


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

    


        [HttpPut]
        public IActionResult EditActivity(PmsisActividad activity)
        {
            _context.PmsisActividads.Update(activity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}