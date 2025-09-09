using AppFacturacion1._5.Data;
using AppFacturacion1._5.Domain;
using AppFacturacion1._5.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion1._5.Implementations
{
    public class FacturaRepository : IFacturaRepository
    {
        public Factura RecuperarPorId(int nroFactura)
        {
            var pars = new List<SpParameter> { new SpParameter("@nro_factura", nroFactura) };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURA_POR_ID", pars);

            Factura f = null;
            foreach (DataRow row in dt.Rows)
            {
                if (f == null)
                {
                    f = new Factura
                    {
                        NroFactura = (int)row["nro_factura"],
                        Fecha = (DateTime)row["fecha"],
                        Cliente = row["cliente"].ToString(),
                        FormaPago = row["FormaPago"].ToString()
                    };
                }

                f.Detalles.Add(new DetalleFactura
                {
                    IdDetalle = (int)row["id_detalle"],
                    Cantidad = (int)row["cantidad"],
                    Articulo = row["Articulo"].ToString(),
                    PrecioUnitario = (decimal)row["precioUnitario"]
                });
            }
            return f;
        }

        public int InsertarFactura(Factura factura)
        {
            // 1) Inserto maestro y obtengo @id_factura
            var parsMaster = new List<SpParameter>
            {
                new SpParameter("@cliente", factura.Cliente),
                new SpParameter("@id_formapago", factura.IdFormaPago),
                new SpParameter("@id_factura", 0, SqlDbType.Int, ParameterDirection.Output)
            };

            int idFactura = DataHelper.GetInstance()
                .ExecuteSPReturnOutputInt("SP_INSERTAR_FACTURA", parsMaster, "@id_factura");

            // 2) Inserto detalles
            foreach (var d in factura.Detalles)
            {
                var parsDet = new List<SpParameter>
                {
                    new SpParameter("@nro_factura", idFactura),
                    new SpParameter("@id_articulo", d.IdArticulo),
                    new SpParameter("@cantidad", d.Cantidad)
                };
                DataHelper.GetInstance().ExecuteSPDML("SP_INSERTAR_DETALLE", parsDet);
            }

            return idFactura;
        }
    }
}
