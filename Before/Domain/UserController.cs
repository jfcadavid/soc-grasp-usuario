using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Before.Domain
{
    public class UserController
    {
        //“Base de datos” simulada
        private readonly List<User> _database = new();

        //Caso de uso: Register (pero con demasiadas responsabilidades adentro)
        public string Register(string name, string email, string phone, string address)
        {
            if (!email.Contains("@"))
            {
                return "email invalido";
            }
            if (phone.Any(c => !char.IsDigit(c)))
            {
                return "telefono invalido";
            } //la validación debería estar en un módulo aparte

            if (_database.Any(u => u.Email== email))
            {
                return "El usuario ya existe";
            }//El controller no debería decidir reglas de negocio

            var User = new User
            {
                Name = name,
                Email = email,
                Phone = phone,
                Address = address
            };//suma a que el controller hace demasiado.


            _database.Add(User);//no es responsabilidad del controlador.

            return "Usuario agregado correctamente";

            

        }

        //CASO DE USO: encontrar por email

        public string FindbyEmail(string email)
        {
            var user = _database.FirstOrDefault(u => u.Email== email);

            if (user==null)
            {
                return "usuario no encontrado";
            }

            return $"Usuario: {user.Name} - {user.Email} - {user.Phone} - {user.Address}";
        }
    }

    //mezcla validación, reglas de negocio, persistencia y formato de salida.
    //Esto lo convierte en un Fat Controller. Cada nueva funcionalidad obliga a
    //modificar esta clase, por lo que puede crecer indefinidamente y se vuelve
    //difícil de mantener y probar.
}
