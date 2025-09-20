using System;
using System.Collections.Generic;

namespace AppFacturacionAPI.Entities;

public partial class DetallesFactura
{
    public int IdDetalle { get; set; }

    public int IdArticulo { get; set; }

    public int Cantidad { get; set; }

    public int NroFactura { get; set; }

    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    public virtual Factura NroFacturaNavigation { get; set; } = null!;
}
