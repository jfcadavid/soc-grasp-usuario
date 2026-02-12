using After.Domain;
using System.Collections.Generic;
using System.Linq;

namespace After.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        //  CAPA: INFRASTRUCTURE
        //  RESPONSABILIDAD: Simular almacenamiento en memoria
        //  SoC: Solo maneja datos, no contiene reglas de negocio

        // Simulando BD
        private readonly List<User> _users = new();

        public bool ExistByEmail(string email)
        {
            return _users.Any(u => u.Email == email);
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? FindByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }
    }
}
