using app_gastos_reclutadores.Context;
using app_gastos_reclutadores.Interfaces;
using app_gastos_reclutadores.Modelos;

// Definition of the CategoriasRepository class which implements the ICategoriasRepository interface
public class CategoriasRepository : ICategoriasRepository
{
    // Private read-only field for the database context
    private readonly GastosContext _context;

    // Constructor for CategoriasRepository with dependency injection of GastosContext
    public CategoriasRepository(GastosContext context)
    {
        _context = context;
    }

    // Method to add a new Categoria entity to the database
    public void Add(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
    }

    // Method to find a Categoria entity by its ID
    public Categoria FindById(int id)
    {
        return _context.Categorias.FirstOrDefault(c => c.ID == id);
    }

    // Method to remove an existing Categoria entity from the database
    public void Remove(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
    }

    // Method to save any changes made in the context to the database
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
