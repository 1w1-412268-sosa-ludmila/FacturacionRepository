using AppFacturacion25.Domain.Data;
using AppFacturacion25.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFacturacion25.Domain.Interfaces;

namespace AppFacturacion25.Domain.Implementations
{
    public class ArticuloRepository : IArticuloRepository
    {
        public List<Articulo> RecuperarTodos()
        {
            var list = new List<Articulo>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS");
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Articulo
                {
                    IdArticulo = (int)row["id_articulo"],
                    Nombre = row["nombre"].ToString(),
                    PrecioUnitario = (decimal)row["precioUnitario"]
                });
            }
            return list;
        }

        public Articulo RecuperarPorId(int id)
        {
            var pars = new List<SpParameter> { new SpParameter("@id_articulo", id) };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULO_POR_ID", pars);
            if (dt.Rows.Count == 0) return null;

            var r = dt.Rows[0];
            return new Articulo
            {
                IdArticulo = (int)r["id_articulo"],
                Nombre = r["nombre"].ToString(),
                PrecioUnitario = (decimal)r["precioUnitario"]
            };
        }

        public void Guardar(Articulo a)
        {
            var pars = new List<SpParameter>
            {
                new SpParameter("@id_articulo", a.IdArticulo), // 0 -> INSERT
                new SpParameter("@nombre", a.Nombre),
                new SpParameter("@precio", a.PrecioUnitario)
            };
            DataHelper.GetInstance().ExecuteSPDML("SP_GUARDAR_ARTICULO", pars);
        }
    }
}
