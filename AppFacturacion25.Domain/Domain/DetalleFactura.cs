using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion25.Domain.Domain
{
    public class DetalleFactura
    {
        public int IdDetalle { get; set; }
        public int IdArticulo { get; set; }
        public string Articulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
