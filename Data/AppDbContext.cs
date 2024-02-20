using Microsoft.EntityFrameworkCore; 

namespace ejercicio_xml_csharp.Data;

public class AppDbContext : DbContext 
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string query = "server=localhost:9001;user=root;password=12345;database=ejercicio_3"; 
        
        optionsBuilder.UseMySql(query, new MySqlServerVersion(new Version(10,4,27)));        
    }   

}