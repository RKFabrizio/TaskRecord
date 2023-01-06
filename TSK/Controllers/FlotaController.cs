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
    public class FlotaController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public FlotaController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var flota = _context.Flota.Select(i => new {
                i.IdFlt,
                i.IdTeq,
                i.Flota,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdFlt" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(flota, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Flotum();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Flota.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdFlt });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Flota.FirstOrDefaultAsync(item => item.IdFlt == key);
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
            var model = await _context.Flota.FirstOrDefaultAsync(item => item.IdFlt == key);

            _context.Flota.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> TipoEquiposLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.TipoEquipos
                         orderby i.TEquipo
                         select new {
                             Value = i.IdTeq,
                             Text = i.TEquipo
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Flotum model, IDictionary values) {
            string ID_FLT = nameof(Flotum.IdFlt);
            string ID_TEQ = nameof(Flotum.IdTeq);
            string FLOTA = nameof(Flotum.Flota);
            string HABILITADO = nameof(Flotum.Habilitado);
            string EXTRACOLUMN1 = nameof(Flotum.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Flotum.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Flotum.Extracolumn3);

            if(values.Contains(ID_FLT)) {
                model.IdFlt = Convert.ToInt32(values[ID_FLT]);
            }

            if(values.Contains(ID_TEQ)) {
                model.IdTeq = Convert.ToInt32(values[ID_TEQ]);
            }

            if(values.Contains(FLOTA)) {
                model.Flota = Convert.ToString(values[FLOTA]).ToUpper();
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