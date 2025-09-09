using System;
using AppFacturacion1._5.Domain;
using AppFacturacion1._5.Services;
using AppFacturacion1._5.Implementations;
using System.Linq;

class Program
{
    static ArticuloService articuloService = new ArticuloService(new ArticuloRepository());
    static FacturaService facturaService = new FacturaService(new FacturaRepository());
    static FormaPagoService formaPagoService = new FormaPagoService(new FormaPagoRepository());

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Sistema de Facturación ===");
            Console.WriteLine("1) Consultar factura por número");
            Console.WriteLine("2) Agregar nueva factura");
            Console.WriteLine("3) Listar todas las facturas");
            Console.WriteLine("4) Salir");
            Console.Write("Opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1": ConsultarFactura(); break;
                case "2": AgregarFactura(); break;
                case "3": ListarFacturas(); break;
                case "4": return;
                default: Console.WriteLine("Opción inválida"); break;
            }

            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    static void ConsultarFactura()
    {
        Console.Write("Ingrese número de factura: ");
        int nro = int.Parse(Console.ReadLine());
        var f = facturaService.Obtener(nro);

        if (f == null)
        {
            Console.WriteLine("Factura no encontrada.");
            return;
        }

        Console.WriteLine($"Factura #{f.NroFactura} - Cliente: {f.Cliente} - Fecha: {f.Fecha} - Forma de Pago: {f.FormaPago}");
        Console.WriteLine("Detalles:");
        foreach (var d in f.Detalles)
        {
            Console.WriteLine($"  Artículo: {d.Articulo}, Cantidad: {d.Cantidad}, Precio Unitario: {d.PrecioUnitario}");
        }
    }

    static void AgregarFactura()
    {
        var f = new Factura();
        Console.Write("Cliente: ");
        f.Cliente = Console.ReadLine();

        Console.WriteLine("Seleccione forma de pago:");
        var formas = formaPagoService.ObtenerTodos();
        foreach (var fp in formas)
            Console.WriteLine($"{fp.IdFormaPago}) {fp.Nombre}");
        f.IdFormaPago = int.Parse(Console.ReadLine());

        // Agregar artículos
        while (true)
        {
            Console.WriteLine("Artículos disponibles:");
            var articulos = articuloService.ObtenerTodos();
            foreach (var a in articulos)
                Console.WriteLine($"{a.IdArticulo}) {a.Nombre} - {a.PrecioUnitario}");

            Console.Write("ID Artículo a agregar (0 para terminar): ");
            int id = int.Parse(Console.ReadLine());
            if (id == 0) break;

            Console.Write("Cantidad: ");
            int cant = int.Parse(Console.ReadLine());

            var art = articulos.FirstOrDefault(x => x.IdArticulo == id);
            if (art != null)
            {
                f.Detalles.Add(new DetalleFactura
                {
                    IdArticulo = art.IdArticulo,
                    Articulo = art.Nombre,
                    Cantidad = cant,
                    PrecioUnitario = art.PrecioUnitario
                });
            }
        }

        int nroFactura = facturaService.Crear(f);
        Console.WriteLine($"Factura creada con nro: {nroFactura}");
    }

    static void ListarFacturas()
    {
        var dt = AppFacturacion1._5.Data.DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURAS");
        foreach (System.Data.DataRow row in dt.Rows)
        {
            Console.WriteLine($"Factura #{row["nro_factura"]} - Cliente: {row["cliente"]} - Fecha: {row["fecha"]} - Forma de Pago: {row["FormaPago"]}");
        }
    }
}







