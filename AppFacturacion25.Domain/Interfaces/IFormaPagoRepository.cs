using AppFacturacion25.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion25.Domain.Interfaces
{
    public interface IFormaPagoRepository
    {
        List<FormaPago> RecuperarTodos();
        void Insertar(string nombre); // usa SP_INSERTAR_FORMAPAGO (@nombre)
    }
}
