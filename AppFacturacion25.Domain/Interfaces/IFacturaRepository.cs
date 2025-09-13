using AppFacturacion25.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion25.Domain.Interfaces
{
    public interface IFacturaRepository
    {
        Factura RecuperarPorId(int nroFactura);
        int InsertarFactura(Factura factura); // inserta maestro y detalles
    }
}
