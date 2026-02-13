using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace After.Architecture
{
    // CAPA: ARCHITECTURE
    // RESPONSABILIDAD: Validaciones simples
    public class UserValidator
    {
        public bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
        }

        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }
    }
}

