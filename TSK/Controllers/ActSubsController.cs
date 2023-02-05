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
    public class ActSubsController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public ActSubsController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var actsubs = _context.ActSubs.Select(i => new {
                i.IdAct,
                i.IdSubAct,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdAct", "IdSubAct" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(actsubs, loadOptions));
        }

        public async Task<IActionResult> SubActividadesLookup(DataSourceLoadOptions loadOptions) 
        {
            
            var result = from actSubsA in _context.ActSubs
                         from subActividad in _context.SubActividads
                         where actSubsA.IdSubAct == subActividad.IdSubAct
                         select new
                         {
                             IdAct = actSubsA.IdAct,
                             IdSubAct = actSubsA.IdSubAct,
                             NombreSubActividad = subActividad.Nombre
                         };

            return Json(await DataSourceLoader.LoadAsync(result, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ActSub();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ActSubs.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdAct, result.Entity.IdSubAct });
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values) {
            var keys = JsonConvert.DeserializeObject<IDictionary>(key);
            var keyIdAct = Convert.ToInt32(keys["IdAct"]);
            var keyIdSubAct = Convert.ToInt32(keys["IdSubAct"]);
            var model = await _context.ActSubs.FirstOrDefaultAsync(item =>
                            item.IdAct == keyIdAct && 
                            item.IdSubAct == keyIdSubAct);
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
            var keyIdSubAct = Convert.ToInt32(keys["IdSubAct"]);
            var model = await _context.ActSubs.FirstOrDefaultAsync(item =>
                            item.IdAct == keyIdAct && 
                            item.IdSubAct == keyIdSubAct);

            _context.ActSubs.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> ActividadsLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Actividads
                         orderby i.IdCon
                         select new
                         {
                             Value = i.IdAct,
                             Text = i.Titulo
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SubActividadsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.SubActividads
                         orderby i.Nombre
                         select new {
                             Value = i.IdSubAct,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(ActSub model, IDictionary values) {
            string ID_ACT = nameof(ActSub.IdAct);
            string ID_SUB_ACT = nameof(ActSub.IdSubAct);
            string HABILITADO = nameof(ActSub.Habilitado);
            string EXTRACOLUMN1 = nameof(ActSub.Extracolumn1);
            string EXTRACOLUMN2 = nameof(ActSub.Extracolumn2);
            string EXTRACOLUMN3 = nameof(ActSub.Extracolumn3);

            if(values.Contains(ID_ACT)) {
                model.IdAct = Convert.ToInt32(values[ID_ACT]);
            }

            if(values.Contains(ID_SUB_ACT)) {
                model.IdSubAct = Convert.ToInt32(values[ID_SUB_ACT]);
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