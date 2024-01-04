using app_gastos_reclutadores.Context;
using app_gastos_reclutadores.dtos;
using app_gastos_reclutadores.Interfaces;
using app_gastos_reclutadores.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace for the repository layer of the application, handling data operations
namespace app_gastos_reclutadores.Repository
{
    // Class GastosRepository, implementing the IGastosRepository interface
    public class GastosRepository : IGastosRepository
    {
        // Private read-only field for the database context
        private readonly GastosContext _context;

        // Constructor injecting the GastosContext
        public GastosRepository(GastosContext context)
        {
            _context = context;
        }

        // Method to add a Gasto entity to the Gastos DbSet
        public void Add(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
        }

        // Method to save changes to the database
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // Method to retrieve Gastos by a specific date
        public IEnumerable<Gasto> GetGastosPorFecha(DateTime fecha)
        {
            return _context.Gastos.Where(g => g.Fecha.Date == fecha.Date).ToList();
        }

        // Method to retrieve Gastos by a specific month and year
        public IEnumerable<Gasto> GetGastosPorMes(int año, int mes)
        {
            return _context.Gastos.Where(g => g.Fecha.Year == año && g.Fecha.Month == mes).ToList();
        }

        // Method to retrieve Gastos by a specific year
        public IEnumerable<Gasto> GetGastosPorAño(int año)
        {
            return _context.Gastos.Where(g => g.Fecha.Year == año).ToList();
        }

        // Method to get the historical data of Gastos, joining with the Categorias
        public IEnumerable<GastoHistorialDto> GetHistorialGastos()
        {
            return _context.Gastos.Join(_context.Categorias,
                gasto => gasto.CategoriaID,
                categoria => categoria.ID,
                (gasto, categoria) => new GastoHistorialDto
                {
                    Fecha = gasto.Fecha,
                    Descripcion = gasto.Descripcion,
                    Categoria = categoria.Nombre,
                    Monto = gasto.Monto
                })
            .OrderByDescending(gasto => gasto.Fecha)
            .ToList();
        }

        // Method to check the existence of any Gasto entity matching a specific condition
        public bool Any(Func<Gasto, bool> predicate)
        {
            return _context.Gastos.Any(predicate);
        }
    }
}

