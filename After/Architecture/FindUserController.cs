using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using After.Domain;

namespace After.Architecture
{
    // GRASP CONTROLLER
    // RESPONSABILIDAD: Coordinar el caso de uso "Consultar usuario"
    public class FindUserController
    {
        private readonly UserService _service;

        public FindUserController(UserService service)
        {
            _service = service;
        }

        public User? Handle(string email)
        {
            // Consulta delegada: controller -> service -> repository
            return _service.FindByEmail(email);
        }
    }
}

