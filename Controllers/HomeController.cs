using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using ejercicio_xml_csharp.Models;
using ejercicio_xml_csharp.Data;

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
                var context     = new AppDbContext();
                var chars       = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var random      = new Random();
                var name        = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                var document    = new Document { 
                    nameFile    = name, 
                    document    = jsonContent.Trim()
                };

                context.Document.Add(document);
                context.SaveChanges();

                return View(document);
            }
        }
        
        return BadRequest("No se recibió ningún archivo XML.");

    }

    [HttpPost]
    public IActionResult ExportFile(string name)
    {
        
        if (String.IsNullOrEmpty(name))
        {
            return BadRequest("Nombre nullo :" + name);
        }

        var document                = from d in new AppDbContext().Document select d;
        document                    = document.Where(d => d.nameFile.Contains(name));
        XDocument xmlDocument       = JsonConvert.DeserializeXNode(document.ToList()[0].document);
        MemoryStream memoryStream   = new MemoryStream();
        xmlDocument.Save(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return File(memoryStream, "application/xml", name + ".xml");
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
