
-- CREACIÓN DE LA BASE DE DATOS
CREATE DATABASE biblioteca
GO

USE biblioteca
GO

-- CREACIÓN DE TABLAS (Estructura limpia sin datos)

---- Tabla Libros
CREATE TABLE Libro
(
  id INT PRIMARY KEY IDENTITY(100,1),  -- Autonumeración que inicia en 100
  nombre VARCHAR(50) NOT NULL,
  disponible BIT DEFAULT 1            -- Columna booleana (1 = Disponible por defecto)
)
GO

---- Tabla Usuario
CREATE TABLE Usuario
(
  cedula VARCHAR(12) PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  edad INT DEFAULT 1
)
GO

---- Tabla Reservacion
CREATE TABLE Reservacion
(
   IdLibro INT,
   cedulaUsuario VARCHAR(12),
   fecha_reserva DATETIME,
   CONSTRAINT FK_LIBRO FOREIGN KEY (IdLibro) REFERENCES Libro(id),
   CONSTRAINT FK_CEDULAUSUARIO FOREIGN KEY (cedulaUsuario) REFERENCES Usuario(cedula)
)
GO

-- CREACIÓN DE TRIGGERS (Automatización de estados)

---- 1. Trigger para cuando se inserta una reserva (Ocupar libro)
CREATE TRIGGER TR_ActualizarDisponibilidad_Reserva
ON Reservacion
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Libro
    SET disponible = 0
    FROM Libro L
    INNER JOIN inserted I ON L.id = I.IdLibro;
END;
GO

---- 2. Trigger para cuando se elimina una reserva (Liberar libro)
CREATE TRIGGER TR_RestaurarDisponibilidad_Cancelacion
ON Reservacion
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Libro
    SET disponible = 1
    FROM Libro L
    INNER JOIN deleted D ON L.id = D.IdLibro;
END;
GO
