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
    public class SistemaController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public SistemaController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var sistemas = _context.Sistemas.Select(i => new {
                i.IdSis,
                i.IdSec,
                i.IdCod,
                i.Nombre,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdSis" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(sistemas, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Sistema();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Sistemas.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdSis });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Sistemas.FirstOrDefaultAsync(item => item.IdSis == key);
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
            var model = await _context.Sistemas.FirstOrDefaultAsync(item => item.IdSis == key);

            _context.Sistemas.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> CondicionsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Condicions
                         orderby i.Nombre
                         select new {
                             Value = i.IdCod,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SectorsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Sectors
                         orderby i.Nombre
                         select new {
                             Value = i.IdSec,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Sistema model, IDictionary values) {
            string ID_SIS = nameof(Sistema.IdSis);
            string ID_SEC = nameof(Sistema.IdSec);
            string ID_COD = nameof(Sistema.IdCod);
            string NOMBRE = nameof(Sistema.Nombre);
            string HABILITADO = nameof(Sistema.Habilitado);
            string EXTRACOLUMN1 = nameof(Sistema.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Sistema.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Sistema.Extracolumn3);

            if(values.Contains(ID_SIS)) {
                model.IdSis = Convert.ToInt32(values[ID_SIS]);
            }

            if(values.Contains(ID_SEC)) {
                model.IdSec = values[ID_SEC] != null ? Convert.ToInt32(values[ID_SEC]) : (int?)null;
            }

            if(values.Contains(ID_COD)) {
                model.IdCod = Convert.ToString(values[ID_COD]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]).ToUpper();
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