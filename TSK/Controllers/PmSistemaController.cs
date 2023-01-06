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
    public class PmSistemaController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public PmSistemaController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var pmsistemas = _context.PmSistemas.Select(i => new {
                i.IdPms,
                i.IdSis,
                i.IdPm,
                i.IdDis,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdPms" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(pmsistemas, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new PmSistema();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.PmSistemas.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdPms });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.PmSistemas.FirstOrDefaultAsync(item => item.IdPms == key);
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
            var model = await _context.PmSistemas.FirstOrDefaultAsync(item => item.IdPms == key);

            _context.PmSistemas.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> DisciplinasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Disciplinas
                         orderby i.Nombre
                         select new {
                             Value = i.IdDis,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PmsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from pm in _context.Pms
                         join flo in _context.Flota on pm.IdFlt equals flo.IdFlt
                         orderby pm.Descripcion + " - " + pm.IdPm + " - " + flo.Flota
                         select new {
                             Value = pm.IdPm,
                             Text = pm.Descripcion + " - " + pm.IdPm + " - " + flo.Flota
                         };


            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SistemasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from sis in _context.Sistemas
                         join con in _context.Condicions on sis.IdCod equals con.IdCod
                         orderby con.Nombre + " - " + sis.Nombre
                         select new {
                             Value = sis.IdSis,
                             Text = con.Nombre + " - " +  sis.Nombre 
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(PmSistema model, IDictionary values) {
            string ID_PMS = nameof(PmSistema.IdPms);
            string ID_SIS = nameof(PmSistema.IdSis);
            string ID_PM = nameof(PmSistema.IdPm);
            string ID_DIS = nameof(PmSistema.IdDis);
            string HABILITADO = nameof(PmSistema.Habilitado);
            string EXTRACOLUMN1 = nameof(PmSistema.Extracolumn1);
            string EXTRACOLUMN2 = nameof(PmSistema.Extracolumn2);
            string EXTRACOLUMN3 = nameof(PmSistema.Extracolumn3);

            if(values.Contains(ID_PMS)) {
                model.IdPms = Convert.ToInt32(values[ID_PMS]);
            }

            if(values.Contains(ID_SIS)) {
                model.IdSis = Convert.ToInt32(values[ID_SIS]);
            }

            if(values.Contains(ID_PM)) {
                model.IdPm = Convert.ToInt32(values[ID_PM]);
            }

            if(values.Contains(ID_DIS)) {
                model.IdDis = Convert.ToInt32(values[ID_DIS]);
            }

            if(values.Contains(HABILITADO)) {
                model.Habilitado = values[HABILITADO] != null ? Convert.ToBoolean(values[HABILITADO]) : (bool?)null;
            }
            if (values.Contains(EXTRACOLUMN1))
            {
                model.Extracolumn1 = Convert.ToString(values[EXTRACOLUMN1]);
            }

            if (values.Contains(EXTRACOLUMN2)) {
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