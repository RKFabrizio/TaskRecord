﻿using DevExpress.Blazor.Reporting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AspNetCore.Reporting;
using DevExtreme.AspNet.Mvc;

namespace TSK.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _iwebHostEnvironment;

        public HomeController(IWebHostEnvironment iwebHostEnvironment)
        {
            this._iwebHostEnvironment = iwebHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print(int model)
        {
            string mimtype = "";
            int extension = 1;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var path = $"{this._iwebHostEnvironment.WebRootPath}\\ReportSistema\\SistemaReporte.rdl";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("NRO_REPORTE", model.ToString());
            parameters.Add("NRO_REPORTE_USUARIOS", model.ToString());
            parameters.Add("NRO_REPORTE_FLOTA", model.ToString());
            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf", "reporte_entrega.pdf");
        }
    }
}
