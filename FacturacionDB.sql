CREATE DATABASE Facturacion
GO
USE Facturacion
GO

-- CREACIÓN DE TABLAS --
CREATE TABLE FormasPago(
    id_formapago INT IDENTITY PRIMARY KEY,
    nombre VARCHAR(30) NOT NULL
)

CREATE TABLE Facturas(
    nro_factura INT IDENTITY PRIMARY KEY,
    fecha DATETIME NOT NULL,
    cliente VARCHAR(50) NOT NULL,
    id_formapago INT NOT NULL,
    CONSTRAINT fk_formapago FOREIGN KEY (id_formapago) REFERENCES FormasPago (id_formapago)
)

CREATE TABLE Articulos(
    id_articulo INT IDENTITY PRIMARY KEY,
    nombre VARCHAR(30) NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL
)

CREATE TABLE DetallesFactura(
    id_detalle INT IDENTITY PRIMARY KEY,
    id_articulo INT NOT NULL,
    cantidad INT NOT NULL,
    nro_factura INT NOT NULL,
    CONSTRAINT fk_id_articulo FOREIGN KEY (id_articulo) REFERENCES Articulos (id_articulo),
    CONSTRAINT fk_nro_factura FOREIGN KEY (nro_factura) REFERENCES Facturas (nro_factura)
)
GO

-- SP GUARDAR ARTICULO
CREATE PROCEDURE SP_GUARDAR_ARTICULO
(@id_articulo INT,
 @nombre VARCHAR(30),
 @precio DECIMAL(10,2)
)
AS
BEGIN
    IF @id_articulo = 0
        INSERT INTO Articulos (nombre, precioUnitario)
        VALUES (@nombre, @precio)
    ELSE
        UPDATE Articulos
        SET nombre = @nombre, precioUnitario = @precio
        WHERE id_articulo = @id_articulo
END
GO

-- Recuperar artículos
CREATE PROCEDURE SP_RECUPERAR_ARTICULOS
AS
BEGIN
    SELECT * FROM Articulos
END
GO

-- Recuperar artículo por ID
CREATE PROCEDURE SP_RECUPERAR_ARTICULO_POR_ID
@id_articulo INT
AS
BEGIN
    SELECT * FROM Articulos WHERE id_articulo = @id_articulo
END
GO

-- Insertar Factura
CREATE PROCEDURE SP_INSERTAR_FACTURA
@cliente VARCHAR(50),
@id_formapago INT,
@id_factura INT OUTPUT
AS
BEGIN
    INSERT INTO Facturas (fecha, cliente, id_formapago)
    VALUES (GETDATE(), @cliente, @id_formapago)

    SET @id_factura = SCOPE_IDENTITY()
END
GO

-- Recuperar facturas
CREATE PROCEDURE SP_RECUPERAR_FACTURAS
AS
BEGIN
    SELECT f.*, p.nombre AS FormaPago
    FROM Facturas f
    INNER JOIN FormasPago p ON f.id_formapago = p.id_formapago
    ORDER BY f.nro_factura
END
GO

-- Recuperar factura con detalles
CREATE PROCEDURE SP_RECUPERAR_FACTURA_POR_ID
@nro_factura INT
AS
BEGIN
    SELECT f.nro_factura, f.fecha, f.cliente, p.nombre AS FormaPago,
           d.id_detalle, d.cantidad, a.nombre AS Articulo, a.precioUnitario
    FROM Facturas f
    INNER JOIN FormasPago p ON f.id_formapago = p.id_formapago
    INNER JOIN DetallesFactura d ON f.nro_factura = d.nro_factura
    INNER JOIN Articulos a ON d.id_articulo = a.id_articulo
    WHERE f.nro_factura = @nro_factura
END
GO

-- Insertar Detalle
CREATE PROCEDURE SP_INSERTAR_DETALLE
@nro_factura INT,
@id_articulo INT,
@cantidad INT
AS
BEGIN
    INSERT INTO DetallesFactura (id_articulo, cantidad, nro_factura)
    VALUES (@id_articulo, @cantidad, @nro_factura)
END
GO

-- Insertar forma de pago (ajustada)
CREATE PROCEDURE SP_INSERTAR_FORMAPAGO
@nombre VARCHAR(30)
AS
BEGIN
    INSERT INTO FormasPago (nombre) VALUES (@nombre)
END
GO

-- Recuperar formas de pago
CREATE PROCEDURE SP_RECUPERAR_FORMASPAGO
AS
BEGIN
    SELECT * FROM FormasPago
END
GO

--Insert--
-- Formas de Pago
INSERT INTO FormasPago (nombre) VALUES ('Efectivo')
INSERT INTO FormasPago (nombre) VALUES ('Tarjeta Crédito')
INSERT INTO FormasPago (nombre) VALUES ('Tarjeta Débito')
INSERT INTO FormasPago (nombre) VALUES ('Transferencia')

-- Artículos
INSERT INTO Articulos (nombre, precioUnitario) VALUES ('Coca Cola 1.5L', 1500.00)
INSERT INTO Articulos (nombre, precioUnitario) VALUES ('Yerba Mate 1Kg', 3500.00)
INSERT INTO Articulos (nombre, precioUnitario) VALUES ('Pan Lactal', 1200.00)
INSERT INTO Articulos (nombre, precioUnitario) VALUES ('Azúcar 1Kg', 950.00)
INSERT INTO Articulos (nombre, precioUnitario) VALUES ('Leche 1L', 1100.00)

-- Factura + detalles de ejemplo
DECLARE @id_factura INT
EXEC SP_INSERTAR_FACTURA 'Juan Pérez', 1, @id_factura OUTPUT
EXEC SP_INSERTAR_DETALLE @id_factura, 1, 2   -- 2 Coca Cola
EXEC SP_INSERTAR_DETALLE @id_factura, 2, 1   -- 1 Yerba Mate
EXEC SP_INSERTAR_DETALLE @id_factura, 5, 6   -- 6 Leches

EXEC SP_RECUPERAR_FACTURA_POR_ID 1
