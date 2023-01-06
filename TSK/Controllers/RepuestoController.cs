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
    public class RepuestoController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public RepuestoController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var repuestos = _context.Repuestos.Select(i => new {
                i.IdRep,
                i.Nombre,
                i.Nroparte,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdRep" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(repuestos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Repuesto();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Repuestos.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdRep });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Repuestos.FirstOrDefaultAsync(item => item.IdRep == key);
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
            var model = await _context.Repuestos.FirstOrDefaultAsync(item => item.IdRep == key);

            _context.Repuestos.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Repuesto model, IDictionary values) {
            string ID_REP = nameof(Repuesto.IdRep);
            string NOMBRE = nameof(Repuesto.Nombre);
            string NROPARTE = nameof(Repuesto.Nroparte);
            string HABILITADO = nameof(Repuesto.Habilitado);
            string EXTRACOLUMN1 = nameof(Repuesto.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Repuesto.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Repuesto.Extracolumn3);

            if(values.Contains(ID_REP)) {
                model.IdRep = Convert.ToInt32(values[ID_REP]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]).ToUpper();
            }

            if(values.Contains(NROPARTE)) {
                model.Nroparte = Convert.ToString(values[NROPARTE]).ToUpper();
            }

            if(values.Contains(HABILITADO)) {
                model.Habilitado = values[HABILITADO] != null ? Convert.ToBoolean(values[HABILITADO]) : (bool?)null;
            }

            if(values.Contains(EXTRACOLUMN1)) {
                model.Extracolumn1 = Convert.ToString(values[EXTRACOLUMN1]).ToUpper();
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