using After.Architecture;
using After.Infrastructure;

namespace After
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Infraestructura (simula BD)
            IUserRepository repo = new InMemoryUserRepository();

            // Capa de aplicación
            var service = new UserService(repo);
            var validator = new UserValidator();

            // Controller GRASP
            var controller = new RegisterUserController(service, validator);
            var findController = new FindUserController(service);

            // Demo rápida
            var ok = controller.Register("Juan", "juan@mail.com", "123456");
            Console.WriteLine(ok ? "Registrado" : "Falló");

            var ok2 = controller.Register("Otro", "juan@mail.com", "123456");
            Console.WriteLine(ok2 ? "Registrado" : "Falló (email repetido)");

            // Caso de uso: Consultar
            var user = findController.Handle("juan@mail.com");
            Console.WriteLine(user == null ? "No encontrado" : $"Encontrado: {user.Name} - {user.Email}");
        }
    }
}
