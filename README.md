Separation of Concerns (SoC)
+ Controller (GRASP)
Sistema de Gestión de Usuarios
Principios de Diseño de Software


1. Definición de los Principios
Separation of Concerns (SoC)
El principio de Separation of Concerns establece que cada módulo o clase del sistema debe tener una responsabilidad específica y bien definida. No se trata solo de dividir el código, sino de organizar el sistema de manera que cada parte se ocupe de un concern (preocupación) diferente.
En términos prácticos:
La validación de datos debe estar separada de la lógica de negocio
La persistencia debe estar separada del modelo de dominio
La coordinación de casos de uso debe estar separada de la implementación
¿Cómo identificar un concern?
Un concern es cualquier aspecto del sistema que tiene su propia razón de cambio. Por ejemplo:
Validación: Cambia cuando cambian las reglas de formato de datos
Persistencia: Cambia cuando cambia la base de datos o el mecanismo de almacenamiento
Lógica de negocio: Cambia cuando cambian las reglas del dominio
Controller (GRASP)
El patrón Controller de GRASP asigna la responsabilidad de recibir y coordinar las operaciones del sistema a un objeto controlador.
Este controlador actúa como intermediario entre la interfaz de usuario y la lógica del sistema.
Características clave:
No implementa lógica de negocio - solo coordina
No accede directamente a datos - delega al repositorio
Un controller por caso de uso - evita controladores sobrecargados
Recibe la solicitud, delega el trabajo y devuelve la respuesta
¿Cuándo un controller deja de ser liviano?
Un controller se considera sobrecargado cuando:
Tiene más de 10-15 líneas de código de lógica
Implementa validaciones complejas directamente
Contiene reglas de negocio
Accede directamente a estructuras de datos persistentes


2. Sistema de Gestión de Usuarios
Descripción del Dominio
El sistema permite administrar usuarios en una plataforma. Es un dominio simple pero representativo, ideal para demostrar SoC y Controller porque:
Tiene operaciones claramente separables (validar, crear, guardar)
Es común en sistemas reales
Permite mostrar diferentes capas de responsabilidad
Casos de Uso
1. Registrar Usuario
Recibe: nombre, email, contraseña
Valida formato de datos
Verifica que el email no exista
Crea y guarda el usuario
Retorna confirmación o error
2. Consultar Usuario
Recibe: email de búsqueda
Consulta en el repositorio
Retorna usuario o mensaje de 'no encontrado'
3. El Problema: Fat Controller (BEFORE)
Code Smells Identificados
Fat Controller - El controlador concentra demasiadas responsabilidades:
Validación: Verifica formato de email y teléfono directamente
Reglas de negocio: Decide si permitir usuarios duplicados
Creación de entidades: Construye el objeto User
Persistencia: Guarda directamente en la lista
Formato de respuesta: Construye mensajes de éxito/error
Consecuencias del Incumplimiento
1. Múltiples razones para cambiar
Si cambia la validación → modificar UserController
Si cambian las reglas de negocio → modificar UserController
Si cambia el almacenamiento → modificar UserController
2. Dificultad para probar
No puedes probar validación sin ejecutar todo el controller
No puedes probar lógica de negocio aisladamente
Necesitas simular persistencia incluso para tests unitarios
3. Crecimiento indefinido
Cada nuevo caso de uso se agrega al mismo controller
El archivo puede llegar a tener cientos de líneas
Se vuelve imposible de mantener
4. Alto acoplamiento
El controller conoce detalles de implementación de todo
Cambiar una cosa afecta todo el flujo
Evidencia en el Código BEFORE
En el archivo UserController.cs (líneas 12-42) se observa:
Línea 14-16: Validación mezclada con lógica
Línea 17-19: Validación de teléfono con lógica LINQ
Línea 21-24: Regla de negocio (email duplicado)
Línea 26-32: Creación de entidad
Línea 33: Persistencia directa


4. La Solución: Aplicando SoC + Controller (AFTER)
Arquitectura en Capas
El sistema se reorganizó en tres capas claramente definidas:
1. Domain (Dominio)
Responsabilidad: Modelar las entidades del negocio
Contiene: User.cs - solo propiedades y constructor
NO hace: Validación, persistencia, lógica de negocio
2. Infrastructure (Infraestructura)
Responsabilidad: Manejo de persistencia y acceso a datos
Contiene: IUserRepository.cs (contrato), InMemoryUserRepository.cs (implementación)
NO hace: Validación, reglas de negocio
3. Architecture (Arquitectura/Aplicación)
Responsabilidad: Coordinar casos de uso y aplicar reglas de negocio
Contiene: UserValidator.cs, UserService.cs, RegisterUserController.cs, FindUserController.cs
NO hace: Acceso directo a datos, construcción de entidades complejas
¿Dónde Termina la Lógica de Negocio?
Esta es una pregunta clave en la aplicación de SoC:
UserValidator: Validaciones simples de formato (no es lógica de negocio)
UserService: Reglas de negocio (ej: no permitir emails duplicados)
User (Domain): Reglas invariantes del dominio (si las tuviera)
Controller: SIN lógica de negocio - solo coordina
Flujo de Ejecución AFTER
Caso de uso: Registrar Usuario
RegisterUserController recibe la solicitud (nombre, email, password)
Delega validación a UserValidator
Crea entidad User del dominio
Delega lógica de negocio a UserService
UserService verifica duplicados usando IUserRepository
UserService delega persistencia a IUserRepository
Controller retorna resultado

5. Comparación BEFORE vs AFTER
Métricas Comparativas

<img width="784" height="182" alt="image" src="https://github.com/user-attachments/assets/80743879-204c-4447-b4ed-53b46df70471" />
<img width="784" height="182" alt="image" src="https://github.com/user-attachments/assets/1b99df4f-92e4-44bf-9261-1753d17eae20" />

Ejemplo de Cambio: Migrar de InMemory a PostgreSQL
BEFORE:
Modificar UserController completo
Cambiar la lista por conexión a BD
Riesgo alto: tocar validaciones, reglas, todo
Probar todo el flujo nuevamente
AFTER:
Crear PostgreSQLUserRepository implementando IUserRepository
Cambiar inyección de dependencia en Program.cs
Riesgo bajo: solo cambia infraestructura
Controllers, Services y Validators sin cambios
6. Impacto en Testing
BEFORE: Testing Monolítico
Para probar validación de email necesitas:
Instanciar UserController completo
Simular toda la lista de usuarios
Ejecutar el método Register completo
No puedes probar validación aislada
AFTER: Testing Modular
Test 1: Validación aislada
Instanciar solo UserValidator
Probar IsValidEmail sin dependencias
Test unitario puro
Test 2: Lógica de negocio con mock
Mockear IUserRepository
Probar UserService sin base de datos real
Verificar que no permite emails duplicados
Test 3: Controller de integración
Probar RegisterUserController con mocks
Verificar que coordina correctamente
7. Trade-offs y Costos del Diseño
Costos del Nuevo Diseño
1. Más archivos y clases
BEFORE: 2 archivos
AFTER: 7 archivos
Más navegación entre archivos
Mayor estructura de carpetas
2. Más líneas de código total
De ~50 líneas a ~180 líneas
Más boilerplate (interfaces, constructores)
3. Curva de aprendizaje
Desarrolladores nuevos necesitan entender la arquitectura
No es obvio dónde agregar nueva funcionalidad
Requiere disciplina de equipo
4. Overhead inicial
Toma más tiempo configurar la estructura inicial
Para un script de 10 líneas, es excesivo
Beneficios Obtenidos
1. Mantenibilidad
Cambios aislados y predecibles
Bajo riesgo de efectos secundarios
2. Testabilidad
Cada componente se prueba independientemente
Uso de mocks para aislar dependencias
3. Extensibilidad
Fácil agregar nuevas implementaciones (ej: PostgreSQL)
Nuevos casos de uso no afectan los existentes
4. Claridad
Cada clase tiene propósito claro
El flujo de datos es explícito
¿Cuándo NO Vale la Pena Esta Separación?
1. Scripts de una sola vez
Script de migración de datos que se ejecuta una vez
Herramienta CLI personal que usa solo una persona
2. Prototipos de 2-3 días
POC para validar una idea técnica
Demo rápida para stakeholders
3. Sistemas con 0 cambios esperados
Herramienta interna con requisitos 100% fijos
(Nota: estos casos son raros en la práctica)
4. Proyectos de menos de 500 líneas
El overhead arquitectónico puede ser mayor que el beneficio
Cuándo SÍ Vale la Pena
Aplicaciones que evolucionarán en el tiempo
Sistemas con múltiples desarrolladores
Proyectos donde los requisitos cambiarán
Código que necesita ser mantenido a largo plazo
Cuando la testabilidad es crítica
8. Riesgos y Limitaciones
Anemic Domain Model
Al aplicar SoC excesivamente, existe el riesgo de crear un Anemic Domain Model:
Las entidades del dominio quedan como simples contenedores de datos
Toda la lógica se mueve a Services
Se pierde la riqueza del modelo de dominio
¿Cómo evitarlo?
Reglas invariantes del dominio deben vivir en las entidades
Solo las orquestaciones van en Services
Ejemplo: User podría tener un método ChangePassword que valide fuerza
Sobre-ingeniería
Aplicar estos principios en exceso puede generar:
Demasiadas capas de abstracción
Código difícil de seguir por exceso de indirección
Pérdida de productividad en proyectos pequeños
Balance necesario:
Aplicar principios según el tamaño y complejidad del proyecto
No anticipar necesidades futuras que quizás nunca lleguen (YAGNI)
9. Reflexión Arquitectónica
¿Cómo se Refleja SoC en el Diagrama de Clases?
En el diagrama AFTER se observa:
Capas claramente separadas - Domain, Infrastructure, Architecture
Dependencias unidireccionales - Controller → Service → Repository
Uso de interfaces - IUserRepository desacopla Service de implementación
Clases cohesionadas - Cada una con una responsabilidad clara
Evolución del Sistema
Esta arquitectura permite que el sistema evolucione de manera controlada:
Nuevos casos de uso = Nuevos controllers (sin tocar los existentes)
Nueva BD = Nueva implementación de Repository (sin tocar Services)
Nuevas validaciones = Agregar en Validator (sin tocar Controllers)
Nuevas reglas = Agregar en Service (sin tocar Infraestructura)
10. Conclusiones
Separation of Concerns no es solo dividir código en archivos. Es organizar el sistema de manera que cada parte tenga una razón clara para existir y cambiar.
Controller (GRASP) no es quien hace el trabajo pesado. Es el director de orquesta que coordina quién hace qué y en qué orden.
El Fat Controller es un síntoma de mezclar concerns. La solución es redistribuir responsabilidades en capas especializadas.
Aplicar estos principios tiene costos reales (más archivos, más líneas, curva de aprendizaje), pero en proyectos que evolucionan, los beneficios superan ampliamente los costos.
No existe una receta única: el balance entre simplicidad y estructura depende del contexto, tamaño del proyecto y expectativas de cambio.
En resumen: SoC + Controller nos permite construir sistemas que son fáciles de entender, mantener, probar y extender.

