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
    public class UnidadController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public UnidadController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var unidads = _context.Unidads.Select(i => new {
                i.IdUni,
                i.IdFlt,
                i.Unidad1,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdUni" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(unidads, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Unidad();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Unidads.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdUni });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Unidads.FirstOrDefaultAsync(item => item.IdUni == key);
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
            var model = await _context.Unidads.FirstOrDefaultAsync(item => item.IdUni == key);

            _context.Unidads.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> FlotaLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Flota
                         orderby i.Flota
                         select new {
                             Value = i.IdFlt,
                             Text = i.Flota
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Unidad model, IDictionary values) {
            string ID_UNI = nameof(Unidad.IdUni);
            string ID_FLT = nameof(Unidad.IdFlt);
            string UNIDAD1 = nameof(Unidad.Unidad1);
            string HABILITADO = nameof(Unidad.Habilitado);
            string EXTRACOLUMN1 = nameof(Unidad.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Unidad.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Unidad.Extracolumn3);

            if(values.Contains(ID_UNI)) {
                model.IdUni = Convert.ToInt32(values[ID_UNI]);
            }

            if(values.Contains(ID_FLT)) {
                model.IdFlt = Convert.ToInt32(values[ID_FLT]);
            }

            if(values.Contains(UNIDAD1)) {
                model.Unidad1 = Convert.ToString(values[UNIDAD1]).ToUpper();
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