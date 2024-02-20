using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;


namespace ejercicio_xml_csharp.Models;

public class Document
{
    [Key]
    public int? id { get; set; }    

    public string? fileName { get; set; } 

    [Column(TypeName = "json")] 
    public JObject? document { get; set; } 
}

