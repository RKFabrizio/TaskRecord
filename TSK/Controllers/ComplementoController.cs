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
    public class ComplementoController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public ComplementoController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var complementos = _context.Complementos.Select(i => new {
                i.IdCom,
                i.IdRep,
                i.IdEqu,
                i.IdHerr,
                i.IdPms,
                i.Habilitado,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdCom" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(complementos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Complemento();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Complementos.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdCom });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Complementos.FirstOrDefaultAsync(item => item.IdCom == key);
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
            var model = await _context.Complementos.FirstOrDefaultAsync(item => item.IdCom == key);

            _context.Complementos.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> EquiposLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Equipos
                         orderby i.Nombre
                         select new {
                             Value = i.IdEqu,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> HerramientaLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Herramienta
                         orderby i.Nombre
                         select new {
                             Value = i.IdHerr,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PmSistemasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from pmsis in _context.PmSistemas
                join sis in _context.Sistemas on pmsis.IdSis equals sis.IdSis
                join pm in _context.Pms on pmsis.IdPm equals pm.IdPm
                orderby pm.Descripcion + " - " + sis.Nombre
                select new
                {
                    Value = pmsis.IdPms,
                    Text = pm.Descripcion + " - " + sis.Nombre
                };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> RepuestosLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Repuestos
                         orderby i.Nombre
                         select new {
                             Value = i.IdRep,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Complemento model, IDictionary values) {
            string ID_COM = nameof(Complemento.IdCom);
            string ID_REP = nameof(Complemento.IdRep);
            string ID_EQU = nameof(Complemento.IdEqu);
            string ID_HERR = nameof(Complemento.IdHerr);
            string ID_PMS = nameof(Complemento.IdPms);
            string HABILITADO = nameof(Complemento.Habilitado);
            string EXTRACOLUMN3 = nameof(Complemento.Extracolumn3);

            if(values.Contains(ID_COM)) {
                model.IdCom = Convert.ToInt32(values[ID_COM]);
            }

            if(values.Contains(ID_REP)) {
                model.IdRep = values[ID_REP] != null ? Convert.ToInt32(values[ID_REP]) : (int?)null;
            }

            if(values.Contains(ID_EQU)) {
                model.IdEqu = values[ID_EQU] != null ? Convert.ToInt32(values[ID_EQU]) : (int?)null;
            }

            if(values.Contains(ID_HERR)) {
                model.IdHerr = values[ID_HERR] != null ? Convert.ToInt32(values[ID_HERR]) : (int?)null;
            }

            if(values.Contains(ID_PMS)) {
                model.IdPms = Convert.ToInt32(values[ID_PMS]);
            }

            if(values.Contains(HABILITADO)) {
                model.Habilitado = values[HABILITADO] != null ? Convert.ToBoolean(values[HABILITADO]) : (bool?)null;
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