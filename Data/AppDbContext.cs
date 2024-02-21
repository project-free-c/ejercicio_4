using Microsoft.EntityFrameworkCore; 
using ejercicio_xml_csharp.Models;

namespace ejercicio_xml_csharp.Data;

public class AppDbContext : DbContext 
{
    public DbSet<Document> Document { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string query = "server=mariadb;port=3306;user=root;password=12345;database=ejercicio_3"; 
        
        optionsBuilder.UseMySql(query, new MySqlServerVersion(new Version(10,4,27)));        
    }   

}