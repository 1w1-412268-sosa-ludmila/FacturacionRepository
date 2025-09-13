using AppFacturacion25.Domain.Domain;
using AppFacturacion25.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion25.Domain.Services
{
    public class ArticuloService
    {
        private readonly IArticuloRepository _repo;
        public ArticuloService(IArticuloRepository repo) => _repo = repo;

        public List<Articulo> ObtenerTodos() => _repo.RecuperarTodos();
        public Articulo ObtenerPorId(int id) => _repo.RecuperarPorId(id);
        public void Guardar(Articulo a) => _repo.Guardar(a);
    }
}
