using app_gastos_reclutadores.Interfaces;
using app_gastos_reclutadores.dtos;
using app_gastos_reclutadores.Enums;
using System;

namespace app_gastos_reclutadores
{
    public class MenuPrincipal
    {
        // Field to store the expense manager service reference
        private readonly IGestorDeGastos _gestorDeGastos;

        // Constructor for initializing the MenuPrincipal class
        public MenuPrincipal(IGestorDeGastos gestorDeGastos)
        {
            // Assign the provided expense manager service to the local field
            _gestorDeGastos = gestorDeGastos;
        }

        // Method to start the main menu interface
        public void Iniciar()
        {
            // Boolean flag to control the menu loop
            bool continuar = true;

            // Loop for the main menu; continues until 'continuar' is set to false
            while (continuar)
            {
                try
                {
                    // Clearing the console for a fresh display of the menu
                    Console.Clear();

                    // Setting the text color for the menu header
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    // Displaying the menu header
                    Console.WriteLine(" Registro de Gastos ".PadLeft(30, '=').PadRight(50, '='));

                    // Setting the text color for the menu options
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    // Listing all available menu options
                    Console.WriteLine("1. Registrar un gasto");
                    Console.WriteLine("2. Añadir una categoría");
                    Console.WriteLine("3. Editar una categoría");
                    Console.WriteLine("4. Eliminar una categoría");
                    Console.WriteLine("5. Generar informe de gastos");
                    Console.WriteLine("6. Ver Historial de Gastos");
                    Console.WriteLine("7. Salir");

                    // Resetting the console color to its default
                    Console.ResetColor();
                    // Separator for aesthetic purposes
                    Console.WriteLine("==================================================");
                    // Prompting the user to select an option
                    Console.Write("Seleccione una opción: ");

                    // Reading user input for menu selection
                    string opcion = Console.ReadLine();

                    // Switch statement to handle different menu options based on user input
                    switch (opcion)
                    {
                        case "1":
                            RegistrarGastoInterfaz(); // Method to register an expense
                            break;
                        case "2":
                            AnadirCategoriaInterfaz(); // Method to add a category
                            break;
                        case "3":
                            EditarCategoriaInterfaz(); // Method to edit a category
                            break;
                        case "4":
                            EliminarCategoriaInterfaz(); // Method to delete a category
                            break;
                        case "5":
                            GenerarInformeInterfaz(); // Method to generate a report
                            break;
                        case "6":
                            _gestorDeGastos.VerHistorialGastos(); // Method to view expense history
                            break;
                        case "7":
                            continuar = false; // Set flag to false to exit the loop and end the program
                            break;
                        default:
                            // Handling invalid option selection
                            Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                }
                // Catch block for handling format exceptions
                catch (FormatException fe)
                {
                    // Setting the error message color to red
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Displaying the format error message
                    Console.WriteLine("\nError en el formato de entrada: : " + fe.Message);
                    // Prompt for user to continue after an error
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    // Resetting the console color to default
                    Console.ResetColor();
                    // Waiting for the user to press a key before continuing
                    Console.ReadKey();
                }
                // Catch block for handling all other exceptions
                catch (Exception ex)
                {
                    // Setting the error message color to red
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Displaying the general error message
                    Console.WriteLine("\nERROR: " + ex.Message);
                    // Prompt for user to continue after an error
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    // Resetting the console color to default
                    Console.ResetColor();
                    // Waiting for the user to press a key before continuing
                    Console.ReadKey();
                }
            }
        }

        // Method to handle the interface for registering an expense
        private void RegistrarGastoInterfaz()
        {
            // Using helper methods to validate and get the expense date
            DateTime fecha = Validaciones.SolicitarFecha("Ingrese la fecha del gasto (formato YYYY-MM-DD):");
            // Using helper methods to validate and get the expense amount
            decimal monto = Validaciones.SolicitarMonto("Ingrese el monto del gasto:");
            // Using helper methods to validate and get the expense description
            string descripcion = Validaciones.SolicitarDescripcion("Ingrese una descripción para el gasto:");
            // Using helper methods to validate and get the category ID
            int categoriaId = Validaciones.SolicitarCategoriaId("Ingrese el ID de la categoría:");

            // Creating a new DTO for the expense
            var gastoDto = new GastoDto
            {
                Fecha = fecha,
                Monto = monto,
                Descripcion = descripcion,
                CategoriaID = categoriaId
            };

            // Using the expense manager service to register the expense
            _gestorDeGastos.RegistrarGasto(gastoDto);
        }

        // Method to add a new category
        private void AnadirCategoriaInterfaz()
        {
            // Solicit and validate the name for the new category using the Validaciones helper class
            string nombreCategoria = Validaciones.SolicitarNombreCategoria("Ingrese el nombre de la nueva categoría:");

            // Call the method to add a new category with the given name
            _gestorDeGastos.AnadirCategoria(nombreCategoria);
        }

        // Method to edit an existing category
        private void EditarCategoriaInterfaz()
        {
            // Solicit and validate the ID of the category to be edited
            int id = Validaciones.SolicitarIdCategoria("Ingrese el ID de la categoría a editar:");

            // Solicit and validate the new name for the category
            string nuevoNombre = Validaciones.SolicitarNombreCategoria("Ingrese el nuevo nombre para la categoría:");

            // Call the method to update the category with the new name
            _gestorDeGastos.EditarCategoria(id, nuevoNombre);
        }

        // Method to delete an existing category
        private void EliminarCategoriaInterfaz()
        {
            // Solicit and validate the ID of the category to be deleted
            int id = Validaciones.SolicitarIdCategoria("Ingrese el ID de la categoría a eliminar:");

            // Ask for confirmation before deleting the category
            Console.WriteLine("¿Está seguro de que desea eliminar la categoría con ID {0}? (s/n)", id);
            string confirmacion = Console.ReadLine();

            // Check if the user confirmed the deletion
            if (confirmacion.Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                // Call the method to delete the category
                _gestorDeGastos.EliminarCategoria(id);
                // Inform the user that the category has been successfully deleted
                Console.WriteLine("Categoría eliminada correctamente.");
            }
            else
            {
                // Inform the user that the deletion has been cancelled
                Console.WriteLine("Operación de eliminación cancelada.");
            }
        }

        // Method to generate a report
        private void GenerarInformeInterfaz()
        {
            // Solicit and validate the user's choice for the type of report
            int opcion = Validaciones.SolicitarTipoInforme("Seleccione el tipo de informe: 1. Diario, 2. Mensual, 3. Anual");

            TipoInforme tipoInforme;

            // Determine the type of report based on the user's choice
            switch (opcion)
            {
                case 1:
                    tipoInforme = TipoInforme.Diario; // Set the report type to Daily
                    break;
                case 2:
                    tipoInforme = TipoInforme.Mensual; // Set the report type to Monthly
                    break;
                case 3:
                    tipoInforme = TipoInforme.Anual; // Set the report type to Annual
                    break;
                default:
                    // Inform the user of an invalid option and exit the method
                    Console.WriteLine("Opción no válida.");
                    return;
            }

            // Call the method to generate the report with the selected type
            _gestorDeGastos.GenerarInforme(tipoInforme);
        }

    }
}
