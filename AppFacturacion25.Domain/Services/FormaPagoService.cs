using AppFacturacion25.Domain.Domain;
using AppFacturacion25.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion25.Domain.Services
{
    public class FormaPagoService
    {
        private readonly IFormaPagoRepository _repo;
        public FormaPagoService(IFormaPagoRepository repo) => _repo = repo;

        public List<FormaPago> ObtenerTodos() => _repo.RecuperarTodos();
        public void Insertar(string nombre) => _repo.Insertar(nombre);
    }
}
