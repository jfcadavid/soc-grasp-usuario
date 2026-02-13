using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using After.Domain;
using After.Infrastructure;

namespace After.Architecture
{
    // GRASP Controller:
    // RESPONSABILIDAD: Coordinar el caso de uso "Registrar usuario"
    // SoC: NO valida profundo, NO guarda, solo orquesta
    public class RegisterUserController
    {
        private readonly UserService _service;
        private readonly UserValidator _validator;

        
        public RegisterUserController(UserService service, UserValidator validator)
        {
            _service = service;
            _validator = validator;
        }

        public bool Register(string name, string email, string password)
        {
            // Validación delegada
            if (!_validator.IsValidName(name)) return false;
            if (!_validator.IsValidEmail(email)) return false;
            if (!_validator.IsValidPassword(password)) return false;

            // Crear entidad de dominio
            var user = new User(name, email, password);

            // Lógica de negocio delegada al servicio
            return _service.Register(user);
        }
    }
}
