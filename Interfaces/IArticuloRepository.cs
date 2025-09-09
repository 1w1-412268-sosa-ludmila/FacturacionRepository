using AppFacturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion1._5.Interfaces
{
    public interface IArticuloRepository
    {
        List<Articulo> RecuperarTodos();
        Articulo RecuperarPorId(int id);
        void Guardar(Articulo articulo); // usa SP_GUARDAR_ARTICULO (id=0 -> insert, >0 -> update)
    }
}
