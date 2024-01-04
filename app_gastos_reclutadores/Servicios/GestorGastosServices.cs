using app_gastos_reclutadores.Context;
using app_gastos_reclutadores.dtos;
using app_gastos_reclutadores.Enums;
using app_gastos_reclutadores.Interfaces;
using app_gastos_reclutadores.Modelos;
using app_gastos_reclutadores.Repository;
using System;
using System.Linq;

namespace app_gastos_reclutadores.Servicios
{
    public class GestorDeGastosService : IGestorDeGastos
    {
        private readonly IGastosRepository _gastosRepository;
        private readonly ICategoriasRepository _categoriasRepository;

        public GestorDeGastosService(IGastosRepository? gastosRepository=null, ICategoriasRepository? categoriasRepository=null)
        {
            _gastosRepository = gastosRepository;
            _categoriasRepository = categoriasRepository;
        }

        public void RegistrarGasto(GastoDto gastoDto)
        {
            var nuevoGasto = new Gasto
            {
                Fecha = gastoDto.Fecha,
                Monto = gastoDto.Monto,
                Descripcion = gastoDto.Descripcion,
                CategoriaID = gastoDto.CategoriaID
            };

            _gastosRepository.Add(nuevoGasto);
            _gastosRepository.SaveChanges();
        }

        public void AnadirCategoria(string nombreCategoria)
        {
            var categoria = new Categoria { Nombre = nombreCategoria };
            _categoriasRepository.Add(categoria);
            _categoriasRepository.SaveChanges();
        }

        public void EditarCategoria(int id, string nuevoNombre)
        {
            var categoria = _categoriasRepository.FindById(id);
            if (categoria == null)
            {
                throw new Exception("Categoría no encontrada.");
            }

            categoria.Nombre = nuevoNombre;
            _categoriasRepository.SaveChanges();
        }

        public void EliminarCategoria(int id)
        {
            var categoria = _categoriasRepository.FindById(id);
            if (categoria == null)
            {
                throw new Exception("Categoría no encontrada.");
            }

            if (_gastosRepository.Any(g => g.CategoriaID == id))
            {
                throw new Exception("No se puede eliminar la categoría porque tiene gastos asociados.");
            }

            _categoriasRepository.Remove(categoria);
            _categoriasRepository.SaveChanges();
        }

        public void GenerarInforme(TipoInforme tipoInforme)
        {
            switch (tipoInforme)
            {
                case TipoInforme.Diario:
                    GenerarInformeDiario();
                    break;
                case TipoInforme.Mensual:
                    GenerarInformeMensual();
                    break;
                case TipoInforme.Anual:
                    GenerarInformeAnual();
                    break;
                default:
                    throw new Exception("Tipo de informe no válido.");
            }
        }

        private void GenerarInformeDiario()
        {
            Console.WriteLine("Ingrese la fecha del informe (formato YYYY-MM-DD):");
            DateTime fechaInforme = DateTime.Parse(Console.ReadLine());

            var gastosPorCategoria = _gastosRepository.GetGastosPorFecha(fechaInforme)
                .GroupBy(g => g.CategoriaID)
                .Select(group => new
                {
                    Categoria = group.Key,
                    Total = group.Sum(g => g.Monto)
                }).ToList();

            Console.WriteLine($"\nInforme Diario: {fechaInforme:yyyy-MM-dd}");
            Console.WriteLine(new string('-', 40)); // Línea divisoria
            Console.WriteLine($"{"Categoria",-20} {"Total gastado",20}");
            Console.WriteLine(new string('-', 40)); // Línea divisoria

            foreach (var item in gastosPorCategoria)
            {
                var categoria = _categoriasRepository.FindById(item.Categoria);
                Console.WriteLine($"{categoria.Nombre,-20} {item.Total,20:C2}");
            }
            Console.WriteLine(new string('-', 40)); // Línea divisoria
        }

        private void GenerarInformeMensual()
        {
            Console.WriteLine("Ingrese el año y mes del informe (formato YYYY-MM):");
            var periodo = Console.ReadLine().Split('-');
            int año = int.Parse(periodo[0]);
            int mes = int.Parse(periodo[1]);

            var gastosPorCategoria = _gastosRepository.GetGastosPorMes(año, mes)
                .GroupBy(g => g.CategoriaID)
                .Select(group => new
                {
                    Categoria = group.Key,
                    Total = group.Sum(g => g.Monto)
                }).ToList();

            Console.WriteLine($"\nInforme Mensual: {año}-{mes:D2}");
            Console.WriteLine(new string('-', 40)); // Línea divisoria
            Console.WriteLine($"{"Categoria",-20} {"Total gastado",20}");
            Console.WriteLine(new string('-', 40)); // Línea divisoria

            foreach (var item in gastosPorCategoria)
            {
                var categoria = _categoriasRepository.FindById(item.Categoria);
                Console.WriteLine($"{categoria.Nombre,-20} {item.Total,20:C2}");
            }
            Console.WriteLine(new string('-', 40)); // Línea divisoria
        }

        private void GenerarInformeAnual()
        {
            Console.WriteLine("Ingrese el año del informe (formato YYYY):");
            int añoInforme = int.Parse(Console.ReadLine());

            var gastosPorCategoria = _gastosRepository.GetGastosPorAño(añoInforme)
                .GroupBy(g => g.CategoriaID)
                .Select(group => new
                {
                    Categoria = group.Key,
                    Total = group.Sum(g => g.Monto)
                }).ToList();

            Console.WriteLine($"\nInforme Anual: {añoInforme}");
            Console.WriteLine(new string('-', 40)); // Línea divisoria
            Console.WriteLine($"{"Categoria",-20} {"Total gastado",20}");
            Console.WriteLine(new string('-', 40)); // Línea divisoria

            foreach (var item in gastosPorCategoria)
            {
                var categoria = _categoriasRepository.FindById(item.Categoria);
                Console.WriteLine($"{categoria.Nombre,-20} {item.Total,20:C2}");
            }
            Console.WriteLine(new string('-', 40)); // Línea divisoria
        }


        public void VerHistorialGastos()
        {
            var historial = _gastosRepository.GetHistorialGastos();
            Console.WriteLine("\nHistorial de Gastos");
            Console.WriteLine(new string('-', 80)); // Línea divisoria
            Console.WriteLine($"{"Fecha",-12} {"Categoría",-20} {"Descripción",-30} {"Monto",18}");
            Console.WriteLine(new string('-', 80)); // Línea divisoria

            foreach (var gasto in historial)
            {
                Console.WriteLine($"{gasto.Fecha.ToShortDateString(),-12} {gasto.Categoria,-20} {gasto.Descripcion,-30} {gasto.Monto,18:C2}");
            }
            Console.WriteLine(new string('-', 80)); // Línea divisoria
        }

    }
}
