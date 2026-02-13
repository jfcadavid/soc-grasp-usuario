using After.Domain;
using After.Infrastructure;


namespace After.Architecture
{
    // CAPA: ARCHITECTURE (o Application)
    // RESPONSABILIDAD: Reglas de negocio (casos de uso)
    // SoC: la lógica que NO debe estar en el controller vive aquí
    public class UserService
    {
        private readonly IUserRepository _repository;

        // Inyección de dependencia:
        // el servicio no sabe CÓMO se guarda, solo usa el contrato
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool Register(User user)
        {
            // REGLA DE NEGOCIO: no permitir emails repetidos
            if (_repository.ExistsByEmail(user.Email))
                return false;

            // Persistencia delegada al repositorio
            _repository.Add(user);
            return true;
        }

        public User? FindByEmail(string email)
        {
            // Consulta delegada al repositorio
            return _repository.FindByEmail(email);
        }

        

    }
}
