using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion1._5.Domain
{
    public class Factura
    {
        public int NroFactura { get; set; }     
        public DateTime Fecha { get; set; }     
        public string Cliente { get; set; }    

        public int IdFormaPago { get; set; }    
        public string FormaPago { get; set; }   

        public List<DetalleFactura> Detalles { get; } = new List<DetalleFactura>();
    }
}
