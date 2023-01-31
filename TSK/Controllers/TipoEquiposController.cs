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
    public class TipoEquiposController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public TipoEquiposController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var tipoequipos = _context.TipoEquipos.Select(i => new {
                i.IdTeq,
                i.IdArea,
                i.TEquipo,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdTeq" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(tipoequipos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new TipoEquipo();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.TipoEquipos.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdTeq });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.TipoEquipos.FirstOrDefaultAsync(item => item.IdTeq == key);
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
            var model = await _context.TipoEquipos.FirstOrDefaultAsync(item => item.IdTeq == key);

            _context.TipoEquipos.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> AreasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Areas
                         orderby i.Nombre
                         select new {
                             Value = i.IdArea,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(TipoEquipo model, IDictionary values) {
            string ID_TEQ = nameof(TipoEquipo.IdTeq);
            string ID_AREA = nameof(TipoEquipo.IdArea);
            string TEQUIPO = nameof(TipoEquipo.TEquipo);
            string HABILITADO = nameof(TipoEquipo.Habilitado);
            string EXTRACOLUMN1 = nameof(TipoEquipo.Extracolumn1);
            string EXTRACOLUMN2 = nameof(TipoEquipo.Extracolumn2);
            string EXTRACOLUMN3 = nameof(TipoEquipo.Extracolumn3);

            if(values.Contains(ID_TEQ)) {
                model.IdTeq = Convert.ToInt32(values[ID_TEQ]);
            }

            if(values.Contains(ID_AREA)) {
                model.IdArea = Convert.ToInt32(values[ID_AREA]);
            }

            if(values.Contains(TEQUIPO)) {
                model.TEquipo = Convert.ToString(values[TEQUIPO]);
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