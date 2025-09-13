using AppFacturacion25.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion25.Domain.Interfaces
{
    public interface IArticuloRepository
    {
        List<Articulo> RecuperarTodos();
        Articulo RecuperarPorId(int id);
        void Guardar(Articulo articulo); // usa SP_GUARDAR_ARTICULO (id=0 -> insert, >0 -> update)
    }
}
