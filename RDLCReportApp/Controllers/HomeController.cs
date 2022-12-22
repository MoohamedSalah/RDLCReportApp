

using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Microsoft.ReportingServices.Interfaces;
using RDLCReportApp.Models;
using System.Data;
using System.Text;

namespace RDLCReportApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SalaryService salaryService = new();
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeSalaryInfo()
        {

            var dataTable = new DataTable();

            dataTable = salaryService.GetActorInfo();

            var mineType = "";
            var extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptActorInfo.rdlc";
            Stream reportDefinition;
            using var fs = new FileStream(path, FileMode.Open);
            reportDefinition = fs;

            var report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);

            report.DataSources.Add(new ReportDataSource("source", dataTable));
          
            byte[] pdf = report.Render("PDF");
            fs.Dispose();

            return File(pdf, "application/pdf", "test.pdf");





        }
    }
}