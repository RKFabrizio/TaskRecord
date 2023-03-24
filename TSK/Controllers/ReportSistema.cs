using DevExpress.Blazor.Reporting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AspNetCore.Reporting;
using DevExtreme.AspNet.Mvc;

namespace TSK.Controllers
{
    public class ReportSistemaController : Controller
    {
        private readonly IWebHostEnvironment _iwebHostEnvironment1;

        public ReportSistemaController(IWebHostEnvironment iwebHostEnvironment)
        {
            this._iwebHostEnvironment1 = iwebHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print2(int model)
        {
            string mimtype = "";
            int extension = 1;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var path = $"{this._iwebHostEnvironment1.WebRootPath}\\ReportSistema\\SistemaReporte.rdl";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("NRO_REPORTE", model.ToString());
            parameters.Add("NRO_SISTEMA_DE_REPORTE", model.ToString());
            parameters.Add("NRO_REPORTE_FLOTA", model.ToString());
            parameters.Add("NRO_REPORTE_USUARIOS", model.ToString());
            parameters.Add("NRO_REPORTE_ENTREGA", model.ToString());
            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf", "reporte_"+model.ToString()+".pdf");
        }
    }
}

