using AppFacturacion1._5.Domain;
using AppFacturacion1._5.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion1._5.Services
{
    public class FacturaService
    {
        private readonly IFacturaRepository _repo;
        public FacturaService(IFacturaRepository repo) => _repo = repo;

        public Factura Obtener(int nroFactura) => _repo.RecuperarPorId(nroFactura);

        public int Crear(Factura factura)
        {
            // Regla: si el mismo artículo se agrega más de una vez, acumulo cantidades
            var normalizados = factura.Detalles
                .GroupBy(d => d.IdArticulo)
                .Select(g => new DetalleFactura
                {
                    IdArticulo = g.Key,
                    Cantidad = g.Sum(x => x.Cantidad)
                })
                .ToList();

            factura.Detalles.Clear();
            factura.Detalles.AddRange(normalizados);

            return _repo.InsertarFactura(factura);
        }
    }
}
