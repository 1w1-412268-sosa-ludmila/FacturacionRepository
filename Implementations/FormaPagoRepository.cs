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
    public class FormaPagoRepository : IFormaPagoRepository
    {
        public List<FormaPago> RecuperarTodos()
        {
            var list = new List<FormaPago>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FORMASPAGO");
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new FormaPago
                {
                    IdFormaPago = (int)row["id_formapago"],
                    Nombre = row["nombre"].ToString()
                });
            }
            return list;
        }

        public void Insertar(string nombre)
        {
            var pars = new List<SpParameter>
            {
                new SpParameter("@nombre", nombre)
            };
            DataHelper.GetInstance().ExecuteSPDML("SP_INSERTAR_FORMAPAGO", pars);
        }
    }
}