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
using static System.Net.Mime.MediaTypeNames;

namespace TSK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PmController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public PmController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var pms = _context.Pms.Select(i => new {
                i.IdPm,
                i.Nombre,
                i.Descripcion,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3,
                i.IdFlt,
                i.IdPmCopy
            }).OrderBy(x => x.IdPm).ThenByDescending(x => x.IdPm);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdPm" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(pms, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Pm();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Pms.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdPm });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Pms.FirstOrDefaultAsync(item => item.IdPm == key);
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
            var model = await _context.Pms.FirstOrDefaultAsync(item => item.IdPm == key);

            _context.Pms.Remove(model);
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

        [HttpGet]
        public async Task<IActionResult> PMLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from pm in _context.Pms
                         join flo in _context.Flota on pm.IdFlt equals flo.IdFlt
                         orderby pm.IdPm
                         select new
                         {
                             Value = pm.IdPm,
                             Text = pm.Nombre + " - " + pm.Descripcion + " - " + flo.Flota
                         };

            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }



        private void PopulateModel(Pm model, IDictionary values) {
            string ID_PM = nameof(Pm.IdPm);
            string NOMBRE = nameof(Pm.Nombre);
            string DESCRIPCION = nameof(Pm.Descripcion);
            string HABILITADO = nameof(Pm.Habilitado);
            string EXTRACOLUMN1 = nameof(Pm.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Pm.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Pm.Extracolumn3);
            string ID_FLT = nameof(Pm.IdFlt);
            string ID_PMCOPY = nameof(Pm.IdPmCopy);

            if (values.Contains(ID_PM)) {
                model.IdPm = Convert.ToInt32(values[ID_PM]);
            }

            if (values.Contains(NOMBRE))
            {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if (values.Contains(DESCRIPCION)) {
                model.Descripcion = Convert.ToString(values[DESCRIPCION]);
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

            if(values.Contains(ID_FLT)) {
                model.IdFlt = Convert.ToInt32(values[ID_FLT]);
            }

            if (values.Contains(ID_PMCOPY))
            {
                model.IdPmCopy = Convert.ToString(values[ID_PMCOPY]);
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