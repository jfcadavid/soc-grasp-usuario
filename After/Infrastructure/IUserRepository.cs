using After.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace After.Infrastructure
{
    public class IUserRepository
    {
        // CAPA: INFRASTRUCTURA
        // RESPONSABILIDAD: Definir cómo se accede a los datos (contrato)
        // SoC: Separa la persistencia del resto del sistema
        public interface IuserRepository
        {
            bool ExistByEmail(string email);
            void Add(User user);
            User? FindByEmail(string email);
        }
    }
}
