using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ejercicio_xml_csharp.Models;
using System.IO;
using System.Xml.Linq;

namespace ejercicio_xml_csharp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var xmlContent  = reader.ReadToEnd();
                var xmlDocument = XDocument.Parse(xmlContent);
                var jsonContent = Newtonsoft.Json.JsonConvert.SerializeXNode(xmlDocument);
                return Content(jsonContent, "application/json");
            }
        }
        
        return BadRequest("No se recibió ningún archivo XML.");

    }

    [HttpPost]
    public IActionResult ExportFile(string name)
    {
        return BadRequest("Mantenimiento.");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
