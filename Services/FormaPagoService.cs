using AppFacturacion1._5.Domain;
using AppFacturacion1._5.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion1._5.Services
{
    public class FormaPagoService
    {
        private readonly IFormaPagoRepository _repo;
        public FormaPagoService(IFormaPagoRepository repo) => _repo = repo;

        public List<FormaPago> ObtenerTodos() => _repo.RecuperarTodos();
        public void Insertar(string nombre) => _repo.Insertar(nombre);
    }
}
