using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace After.Domain
{
    public class User
    {

        // CAPA: DOMAIN
        // RESPONSABILIDAD: Representar la entidad Usuario
        // SoC: Solo modela datos, no valida ni guarda
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Address { get; }

        public User(string name, string email, string phone, string address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;

        }
    }

    
}
