using System;
using System.Collections.Generic;
using app_gastos_reclutadores;
using app_gastos_reclutadores.Context;
using app_gastos_reclutadores.Interfaces;
using app_gastos_reclutadores.Modelos;
using app_gastos_reclutadores.Repository;
using app_gastos_reclutadores.Servicios;
using Microsoft.Data.SqlClient;


namespace GestorGastos
{
    class Program
    {
        // List to hold 'Gasto' objects in memory
        static List<Gasto> gastos = new List<Gasto>();

        static void Main(string[] args)
        {
            // Initializing the database context for the application
            GastosContext context = new GastosContext();

            // Creating repositories for managing expenses (Gastos) and categories (Categorias) data
            IGastosRepository gastosRepository = new GastosRepository(context);
            ICategoriasRepository categoriasRepository = new CategoriasRepository(context);

            // Creating a service for managing expenses, integrating both the Gastos and Categorias repositories
            IGestorDeGastos gestorDeGastos = new GestorDeGastosService(gastosRepository, categoriasRepository);

            // Instantiating the main menu with the expense manager service and starting the application
            MenuPrincipal menu = new MenuPrincipal(gestorDeGastos);
            menu.Iniciar(); // Initiates the main menu loop
        }
    }
}

