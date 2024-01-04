create database db_gastos;

use db_gastos;

-- Crear la tabla de Categorías
CREATE TABLE Categorias (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL
);

-- Crear la tabla de Gastos
CREATE TABLE Gastos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Monto DECIMAL(10, 2) NOT NULL,
    Descripcion NVARCHAR(255),
    CategoriaID INT,
    FOREIGN KEY (CategoriaID) REFERENCES Categorias(ID)
);
