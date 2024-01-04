using app_gastos_reclutadores.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace declaration for the application, specifically for database context
namespace app_gastos_reclutadores.Context
{
    // Definition of GastosContext, inheriting from DbContext, a part of Entity Framework for .NET
    public class GastosContext : DbContext
    {
        // Declaration of a DbSet property for Gastos, representing a collection of 'Gasto' entities
        public DbSet<Gasto> Gastos { get; set; }

        // Declaration of a DbSet property for Categorias, representing a collection of 'Categoria' entities
        public DbSet<Categoria> Categorias { get; set; }

        // Override of the OnConfiguring method to set up the database context
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuration of the DbContext to use SQL Server with specified connection string
            optionsBuilder.UseSqlServer("Server=[Put Here your Server];Database=[Put here your DB];Integrated Security=True;User Id=[Put Here your User];Password=[Put Here your Password];Encrypt=False;");
        }
    }
}

