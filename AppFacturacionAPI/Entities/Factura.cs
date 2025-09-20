using System;
using System.Collections.Generic;

namespace AppFacturacionAPI.Entities;

public partial class Factura
{
    public int NroFactura { get; set; }

    public DateTime Fecha { get; set; }

    public string Cliente { get; set; } = null!;

    public int IdFormapago { get; set; }

    public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; } = new List<DetallesFactura>();

    public virtual FormasPago IdFormapagoNavigation { get; set; } = null!;
}
