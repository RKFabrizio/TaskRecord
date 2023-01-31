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
    public class PosicionsController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public PosicionsController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var posicions = _context.Posicions.Select(i => new {
                i.IdPos,
                i.Cargo,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdPos" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(posicions, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Posicion();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Posicions.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdPos });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Posicions.FirstOrDefaultAsync(item => item.IdPos == key);
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
            var model = await _context.Posicions.FirstOrDefaultAsync(item => item.IdPos == key);

            _context.Posicions.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Posicion model, IDictionary values) {
            string ID_POS = nameof(Posicion.IdPos);
            string CARGO = nameof(Posicion.Cargo);
            string HABILITADO = nameof(Posicion.Habilitado);
            string EXTRACOLUMN1 = nameof(Posicion.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Posicion.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Posicion.Extracolumn3);

            if(values.Contains(ID_POS)) {
                model.IdPos = Convert.ToInt32(values[ID_POS]);
            }

            if(values.Contains(CARGO)) {
                model.Cargo = Convert.ToString(values[CARGO]);
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