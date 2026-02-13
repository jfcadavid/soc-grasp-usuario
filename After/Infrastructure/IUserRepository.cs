using After.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace After.Infrastructure
{
    // CAPA: INFRASTRUCTURE
    // RESPONSABILIDAD:cómo se guarda/busca
    // SoC: el servicio NO guarda directamente, delega a este contrato
    public interface IUserRepository
    {
        bool ExistsByEmail(string email);
        void Add(User user);
        User? FindByEmail(string email);
    }
}
