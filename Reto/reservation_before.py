# Reto: Refactorizar aplicando SoC y GRASP Controller.
# Problema: El controlador mezcla validación, reglas de negocio,
# persistencia y cálculo, convirtiéndose en un Fat Controller.

#Reto Técnico
#Se proporciona un fragmento de código que implementa la creación de una reservación de hotel.
#El diseño actual presenta un Fat Controller y múltiples violaciones al principio Separation of Concerns (SoC),
#ya que concentra en una sola clase la validación, reglas de negocio, cálculo y persistencia.

#Objetivo
#Refactorizar el código manteniendo la misma funcionalidad y el mismo dominio, 
#aplicando Separation of Concerns (SoC) y GRASP Controller para lograr una mejor distribución
#de responsabilidades y un diseño más mantenible.

class Reservacion:
    def __init__(self, nombre_cliente, tipo_habitacion, noches, precio_noche):
        self.nombre_cliente = nombre_cliente
        self.tipo_habitacion = tipo_habitacion
        self.noches = noches
        self.precio_noche = precio_noche


class Controlador_Reservas:
    def __init__(self):
        self.database = []

    def crear_reservacion(self, nombre_cliente, tipo_habitacion, noches, precio_noche):

        if noches <= 0:
            return "Número de noches inválido"

        if precio_noche <= 0:
            return "Precio inválido"

        for r in self.database:
            if r.nombre_cliente == nombre_cliente and r.tipo_habitacion == tipo_habitacion:
                return "La reserva ya existe"

        total = noches * precio_noche

        reservacion = Reservacion(nombre_cliente, tipo_habitacion, noches, precio_noche)

        self.database.append(reservacion)

        return f"Reserva creada. Total: {total}"
