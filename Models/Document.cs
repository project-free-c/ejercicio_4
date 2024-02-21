using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace ejercicio_xml_csharp.Models;
[Table("document")]
public class Document
{
    [Key]
    public int? id { get; set; }    

    [Column("name_file")]
    public string? nameFile { get; set; } 

    [Column(TypeName = "json")] 
    public string? document { get; set; } 
}

