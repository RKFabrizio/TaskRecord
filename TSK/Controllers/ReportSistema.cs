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
        private readonly IWebHostEnvironment _iwebHostEnvironment;
        public FileContentResult a;


        public ReportSistemaController(IWebHostEnvironment iwebHostEnvironment)
        {
            this._iwebHostEnvironment = iwebHostEnvironment;
        }

        public IActionResult Index()
        {

            return a;
        }

        public IActionResult Print(int model)
        {
            string mimtype = "";
            int extension = 1;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var path = $"{this._iwebHostEnvironment.WebRootPath}\\Reports\\ReporteSistema.rdl";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("NRO_REPORTE", model.ToString());
            parameters.Add("NRO_REPORTE_USUARIOS", model.ToString());
            parameters.Add("NRO_SISTEMA_DE_REPORTE", model.ToString());
            parameters.Add("NRO_REPORTE_FLOTA", model.ToString());
            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            a = File(result.MainStream, "app/pdf", "reporte_sistema.pdf");
            return a;
        }
    }
}