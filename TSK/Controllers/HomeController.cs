using DevExpress.Blazor.Reporting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AspNetCore.Reporting;


namespace TSK.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _iwebHostEnvironment;
        public FileContentResult a;


        public HomeController(IWebHostEnvironment iwebHostEnvironment)
        {
            string mimtype = "";
            int extension = 1;
            this._iwebHostEnvironment = iwebHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var path = $"{this._iwebHostEnvironment.WebRootPath}\\Reports\\Report1.rdl";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("NRO_REPORTE", "2030");
            parameters.Add("NRO_REPORTE_USUARIOS", "2030");
            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            a = File(result.MainStream, "app/pdf");
        }

        public IActionResult Index()
        {
            return a;
        }

        public IActionResult Print()
        {
            return a;
        }
    }
}