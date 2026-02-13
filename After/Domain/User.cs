using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace After.Domain
{
    // CAPA: DOMAIN
    // RESPONSABILIDAD: Representar la entidad Usuario
    // SoC: Solo modela datos, no valida ni guarda
    public class User
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
