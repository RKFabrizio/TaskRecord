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
    public class RepEntregaController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public RepEntregaController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var repentregas = _context.RepEntregas.Select(i => new {
                i.IdRent,
                i.IdRep,
                i.IdEnt,
                i.Resultado,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdRent" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(repentregas, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetId(int id, DataSourceLoadOptions loadOptions)
        {
            var repentregas = _context.RepEntregas.Select(i => new {
                i.IdRent,
                i.IdRep,
                i.IdEnt,
                i.Resultado,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            }).Where(e => e.IdRep == id);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdRent" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(repentregas, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new RepEntrega();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.RepEntregas.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdRent });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.RepEntregas.FirstOrDefaultAsync(item => item.IdRent == key);
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
            var model = await _context.RepEntregas.FirstOrDefaultAsync(item => item.IdRent == key);

            _context.RepEntregas.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> ReportesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Reportes
                         orderby i.IdPm
                         select new {
                             Value = i.IdRep,
                             Text = i.IdPm
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> EntregasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Entregas
                         orderby i.Nombre
                         select new {
                             Value = i.IdEnt,
                             Text = i.IdEnt.ToString() + ".- " + i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(RepEntrega model, IDictionary values) {
            string ID_RENT = nameof(RepEntrega.IdRent);
            string ID_REP = nameof(RepEntrega.IdRep);
            string ID_ENT = nameof(RepEntrega.IdEnt);
            string RESULTADO = nameof(RepEntrega.Resultado);
            string HABILITADO = nameof(RepEntrega.Habilitado);
            string EXTRACOLUMN1 = nameof(RepEntrega.Extracolumn1);
            string EXTRACOLUMN2 = nameof(RepEntrega.Extracolumn2);
            string EXTRACOLUMN3 = nameof(RepEntrega.Extracolumn3);

            if(values.Contains(ID_RENT)) {
                model.IdRent = Convert.ToInt32(values[ID_RENT]);
            }

            if(values.Contains(ID_REP)) {
                model.IdRep = Convert.ToInt32(values[ID_REP]);
            }

            if(values.Contains(ID_ENT)) {
                model.IdEnt = Convert.ToInt32(values[ID_ENT]);
            }

            if(values.Contains(RESULTADO)) {
                model.Resultado = Convert.ToString(values[RESULTADO]).ToUpper();
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