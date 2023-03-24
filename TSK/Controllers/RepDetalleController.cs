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
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Expressions;


namespace TSK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RepDetalleController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public RepDetalleController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

      

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var repdetalles = _context.RepDetalles.Select(i => new {
                i.IdRepsis,
                i.IdAct,
                i.Titulo,
                i.Especificacion,
                i.Valor,
                i.IdEst,
                i.Referencia,
                i.ReferenciaURL,
                i.Observacion,
                i.Orden,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdAct", "IdRepsis" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(repdetalles, loadOptions));
        }

        public async Task<IActionResult> GetDetails(int id, DataSourceLoadOptions loadOptions)
        {
            var repdetalles = _context.RepDetalles.Select(i => new {
                i.IdRepsis,
                i.IdAct,
                i.Titulo,
                i.Especificacion,
                i.Valor,
                i.IdEst,
                i.Referencia,
                i.ReferenciaURL,
                i.Observacion,
                i.Orden,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            }).Where(e => e.IdRepsis == id);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdAct", "IdRepsis" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(repdetalles, loadOptions));
        }

        [HttpPost]
        public ActionResult ExportarPDF()
        {
            // Generar el reporte usando XtraReports
            var reporte = new XtraReport();
            // Aquí añadimos la lógica para llenar el reporte con los datos que necesitamos

            // Exportar el reporte en formato PDF
            var stream = new MemoryStream();
            var exportarPDF = new PdfExportOptions();
            reporte.ExportToPdf(stream, exportarPDF);

            // Devolver el archivo PDF al usuario
            return File(stream.ToArray(), "application/Reports", "Reporte.pdf");
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new RepDetalle();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.RepDetalles.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdAct, result.Entity.IdRepsis });
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values) {
            var keys = JsonConvert.DeserializeObject<IDictionary>(key);
            var keyIdAct = Convert.ToInt32(keys["IdAct"]);
            var keyIdRepsis = Convert.ToInt32(keys["IdRepsis"]);
            var model = await _context.RepDetalles.FirstOrDefaultAsync(item =>
                            item.IdAct == keyIdAct && 
                            item.IdRepsis == keyIdRepsis);
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
            var keyIdRepsis = Convert.ToInt32(keys["IdRepsis"]);
            var model = await _context.RepDetalles.FirstOrDefaultAsync(item =>
                            item.IdAct == keyIdAct && 
                            item.IdRepsis == keyIdRepsis);

            _context.RepDetalles.Remove(model);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IActionResult> EstadoLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from est in _context.Estados
                         orderby est.Valor
                         select new
                         {
                             Value = est.IdEst,
                             Text = est.Valor
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }


        private void PopulateModel(RepDetalle model, IDictionary values) {
            string ID_REPSIS = nameof(RepDetalle.IdRepsis);
            string ID_ACT = nameof(RepDetalle.IdAct);
            string TITULO = nameof(RepDetalle.Titulo);
            string ESPECIFICACION = nameof(RepDetalle.Especificacion);
            string VALOR = nameof(RepDetalle.Valor);
            string ID_EST = nameof(RepDetalle.IdEst);
            string REFERENCIA = nameof(RepDetalle.Referencia);
            string REFERENCIA_URL = nameof(RepDetalle.ReferenciaURL);
            string OBSERVACION = nameof(RepDetalle.Observacion);
            string ORDEN = nameof(RepDetalle.Orden);
            string HABILITADO = nameof(RepDetalle.Habilitado);
            string EXTRACOLUMN1 = nameof(RepDetalle.Extracolumn1);
            string EXTRACOLUMN2 = nameof(RepDetalle.Extracolumn2);
            string EXTRACOLUMN3 = nameof(RepDetalle.Extracolumn3);

            if(values.Contains(ID_REPSIS)) {
                model.IdRepsis = Convert.ToInt32(values[ID_REPSIS]);
            }

            if(values.Contains(ID_ACT)) {
                model.IdAct = Convert.ToInt32(values[ID_ACT]);
            }

            if(values.Contains(TITULO)) {
                model.Titulo = Convert.ToString(values[TITULO]).ToUpper();
            }

            if(values.Contains(ESPECIFICACION)) {
                model.Especificacion = Convert.ToString(values[ESPECIFICACION]).ToUpper();
            }

            if(values.Contains(VALOR)) {
                model.Valor = Convert.ToSingle(values[VALOR], CultureInfo.InvariantCulture);
            }

            if (values.Contains(ID_EST))
            {
                model.IdEst = Convert.ToInt32(values[ID_EST]);
            }

            
            //if (values.Contains(ID_EST)) {
            //    model.IdEst = Convert.ToInt32(values[ID_EST]); //values[COMPLETADA] != null ? Convert.ToBoolean(values[COMPLETADA]) : (bool?)null;
            //}

            if(values.Contains(REFERENCIA)) {
                model.Referencia = Convert.ToString(values[REFERENCIA]).ToUpper();
            }
            if (values.Contains(REFERENCIA_URL))
            {
                model.ReferenciaURL = Convert.ToString(values[REFERENCIA_URL]);
            }

            if (values.Contains(OBSERVACION)) {
                model.Observacion = Convert.ToString(values[OBSERVACION]);
            }

            if(values.Contains(ORDEN)) {
                model.Orden = values[ORDEN] != null ? Convert.ToInt32(values[ORDEN]) : (int?)null;
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