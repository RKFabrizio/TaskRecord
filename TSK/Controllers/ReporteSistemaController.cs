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
    public class ReporteSistemaController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public ReporteSistemaController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var repsistemas = _context.RepSistemas.Select(i => new {
                i.IdRepsis,
                i.IdRep,
                i.NomSistema,
                i.NomSector,
                i.NomDisciplina,
                i.IdCod,
                i.Avance,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdRepsis" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(repsistemas, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetCondicion(int idRep, string idCod, DataSourceLoadOptions loadOptions)
        {
            var repsistemas = _context.RepSistemas.Select(i => new {
                i.IdRepsis,
                i.IdRep,
                i.NomSistema,
                i.NomSector,
                i.NomDisciplina,
                i.IdCod,
                i.Avance,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            }).Where(e => e.IdCod == idCod && e.IdRep == idRep);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdRepsis" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(repsistemas, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new RepSistema();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.RepSistemas.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdRepsis });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.RepSistemas.FirstOrDefaultAsync(item => item.IdRepsis == key);
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
            var model = await _context.RepSistemas.FirstOrDefaultAsync(item => item.IdRepsis == key);

            _context.RepSistemas.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> ReportesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Reportes
                         orderby i.IdPm
                         select new {
                             Value = i.IdRep,
                             Text = i.Comentario
                         }
                         ;
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(RepSistema model, IDictionary values) {
            string ID_REPSIS = nameof(RepSistema.IdRepsis);
            string ID_REP = nameof(RepSistema.IdRep);
            string NOM_SISTEMA = nameof(RepSistema.NomSistema);
            string NOM_SECTOR = nameof(RepSistema.NomSector);
            string NOM_DISCIPLINA = nameof(RepSistema.NomDisciplina);
            string ID_COD = nameof(RepSistema.IdCod);
            string AVANCE = nameof(RepSistema.Avance);
            string HABILITADO = nameof(RepSistema.Habilitado);
            string EXTRACOLUMN1 = nameof(RepSistema.Extracolumn1);
            string EXTRACOLUMN2 = nameof(RepSistema.Extracolumn2);
            string EXTRACOLUMN3 = nameof(RepSistema.Extracolumn3);

            if(values.Contains(ID_REPSIS)) {
                model.IdRepsis = Convert.ToInt32(values[ID_REPSIS]);
            }

            if(values.Contains(ID_REP)) {
                model.IdRep = Convert.ToInt32(values[ID_REP]);
            }

            if(values.Contains(NOM_SISTEMA)) {
                model.NomSistema = Convert.ToString(values[NOM_SISTEMA]).ToUpper();
            }

            if(values.Contains(NOM_SECTOR)) {
                model.NomSector = Convert.ToString(values[NOM_SECTOR]).ToUpper();
            }

            if(values.Contains(NOM_DISCIPLINA)) {
                model.NomDisciplina = Convert.ToString(values[NOM_DISCIPLINA]).ToUpper();
            }

            if (values.Contains(ID_COD))
            {
                model.IdCod = Convert.ToString(values[ID_COD]);
            }

            if (values.Contains(AVANCE))
            {
                model.Avance = Convert.ToSingle(values[AVANCE], CultureInfo.InvariantCulture);
            }

            if (values.Contains(HABILITADO)) {
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