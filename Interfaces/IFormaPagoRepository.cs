using AppFacturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion1._5.Interfaces
{
    public interface IFormaPagoRepository
    {
        List<FormaPago> RecuperarTodos();
        void Insertar(string nombre); // usa SP_INSERTAR_FORMAPAGO (@nombre)
    }
}
