using System;
using System.Collections.Generic;

namespace AppFacturacionAPI.Entities;

public partial class FormasPago
{
    public int IdFormapago { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
