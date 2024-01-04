using System;
using System.Globalization;

public static class Validaciones
{
    // Method to request and validate a date from the user
    public static DateTime SolicitarFecha(string mensaje)
    {
        DateTime fecha;
        Console.WriteLine(mensaje);
        // Loop until a valid date is entered in the specified format
        while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
        {
            // Display error message in red if the format is invalid
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Formato de fecha inválido. Por favor, ingrese la fecha en formato YYYY-MM-DD:");
        }
        return fecha;
    }

    // Method to request and validate a monetary amount
    public static decimal SolicitarMonto(string mensaje)
    {
        decimal monto;
        Console.WriteLine(mensaje);
        // Loop until a valid decimal number is entered and it is positive
        while (!decimal.TryParse(Console.ReadLine(), out monto) || monto < 0)
        {
            // Display error message in red if the input is invalid
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Monto inválido. Por favor, ingrese un número decimal positivo:");
        }
        return monto;
    }

    // Method to request and validate a description
    public static string SolicitarDescripcion(string mensaje)
    {
        string descripcion;
        do
        {
            Console.WriteLine(mensaje);
            descripcion = Console.ReadLine();

            // Validation for empty or too long descriptions
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La descripción no puede estar vacía. Por favor, ingrese una descripción válida.");
            }
            else if (descripcion.Length > 255) // Assuming 255 is the maximum allowed length
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La descripción es demasiado larga. Por favor, limítese a 255 caracteres.");
            }
        }
        while (string.IsNullOrWhiteSpace(descripcion) || descripcion.Length > 255);

        return descripcion;
    }

    // Method to request and validate a category ID
    public static int SolicitarCategoriaId(string mensaje)
    {
        int categoriaId;
        Console.WriteLine(mensaje);
        // Loop until a valid integer number is entered and it is positive
        while (!int.TryParse(Console.ReadLine(), out categoriaId) || categoriaId < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ID de categoría inválido. Por favor, ingrese un número entero positivo:");
        }
        return categoriaId;
    }

    // Method to request and validate a category name
    public static string SolicitarNombreCategoria(string mensaje)
    {
        string nombreCategoria;
        do
        {
            Console.WriteLine(mensaje);
            nombreCategoria = Console.ReadLine();

            // Validation for empty or incorrect length category names
            if (string.IsNullOrWhiteSpace(nombreCategoria))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El nombre de la categoría no puede estar vacío. Por favor, ingrese un nombre válido.");
            }
            else if (nombreCategoria.Length < 3 || nombreCategoria.Length > 50)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El nombre de la categoría debe tener entre 3 y 50 caracteres.");
            }
        }
        while (string.IsNullOrWhiteSpace(nombreCategoria) || nombreCategoria.Length < 3 || nombreCategoria.Length > 50);

        return nombreCategoria;
    }

    // Method to request and validate a category ID
    public static int SolicitarIdCategoria(string mensaje)
    {
        int id;
        do
        {
            Console.WriteLine(mensaje);
            if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID inválido, Por favor ingrese un número entero positivo.");
            }
        }
        while (id <= 0);

        return id;
    }

    // Method to request and validate the type of report
    public static int SolicitarTipoInforme(string mensaje)
    {
        int tipoInforme;
        do
        {
            Console.WriteLine(mensaje);
            string entrada = Console.ReadLine();
            // Loop until a valid option (1, 2, or 3) is chosen
            if (!int.TryParse(entrada, out tipoInforme) || (tipoInforme < 1 || tipoInforme > 3))
            {
                // Display error message in red for invalid selection and yellow to highlight valid options
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Selección inválida. ");
                Console.ResetColor();
                Console.Write("Por favor, seleccione ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("1");
                Console.ResetColor();
                Console.Write(" para Diario, ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("2");
                Console.ResetColor();
                Console.Write(" para Mensual, o ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("3");
                Console.ResetColor();
                Console.WriteLine(" para Anual.");
            }
        }
        while (tipoInforme < 1 || tipoInforme > 3);

        return tipoInforme;
    }
}
