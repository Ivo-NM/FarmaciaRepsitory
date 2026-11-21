
CREATE DATABASE [Grupo_4_Farmacia_1w1]
GO

USE Grupo_4_Farmacia_1w1
GO
------------------------ CREANDO TABLAS DE LOCALIZACIÓN -PROVINCIA-LOCALIDAD-BARRIO--------------------------------------------


Create Table PROVINCIAS(
Id_Provincia int identity (1,1),
Nombre_Provincia varchar(30) not null,
Constraint PK_PROVINCIAS PRIMARY KEY (Id_Provincia)
)

Create Table LOCALIDADES(
Id_Localidad int identity (1,1),
Nombre_Localidad varchar(30) not null,
Id_Provincia int not null,
Constraint PK_LOCALIDAD PRIMARY KEY (Id_Localidad),
Constraint FK_LOCALIDA_PROVINCIA FOREIGN KEY (Id_Provincia) REFERENCES PROVINCIAS(Id_Provincia)
)

Create Table BARRIOS(
Id_Barrio int identity (1,1),
Nombre_Barrio varchar(30) not null,
Id_Localidad int not null,
Constraint PK_BARRIO PRIMARY KEY (Id_Barrio),
Constraint FK_BARRIO_LOCALIDAD FOREIGN KEY (Id_Localidad) REFERENCES LOCALIDADES(Id_Localidad)
)

--------------------------------------------------FIN TABLAS DE LOCALIZACIÓN ----------------------------------------------------------


--------------------------------------------------TABLAS PRODUCTOS Y OBRA SOCIAL ------------------------------------------------------

Create Table OBRAS_SOCIALES(
Cod_Obra_Social int identity (1,1),
Detalle varchar(30)not null,
CONSTRAINT FK_OBRA_SOCIAL PRIMARY KEY (Cod_Obra_Social)
)

Create Table MEDICAMENTOS (
Cod_Medicamento int identity (1,1),
Nombre_Medicamento varchar(50) not null,
Prescripcion_Medica bit not null,
Descripcion varchar(50),
Indicaciones_Uso varchar(100),
Precio_Unitario decimal(10,2) not null,
CONSTRAINT PK_SUMINISTRO PRIMARY KEY (Cod_Medicamento)
)


CREATE TABLE LOTES(
Nro_Lote int not null,
Cod_Medicamento int not null,
Fecha_Fabricacion datetime not null,
Fecha_Vencimiento datetime not null,
CONSTRAINT PK_LOTE PRIMARY KEY (Nro_lote),
CONSTRAINT FK_LOTE_MEDICAMENTO FOREIGN KEY (Cod_Medicamento) REFERENCES MEDICAMENTOS (Cod_Medicamento)
)



CREATE TABLE COMPONENTES(
Cod_componente int identity(1,1),
Detalle varchar(50),
CONSTRAINT PK_COMPOSICION PRIMARY KEY (Cod_componente)
)

--TABLA QUE UNE VARIOS COMPONENTES CON UN TIPO DE MEDICAMENTO--
CREATE TABLE MEDICAMENTOS_COMPONENTES (
id INT IDENTITY(1,1),
Cod_medicamento INT NOT NULL,
Cod_componente INT NOT NULL,
cantidad DECIMAL(10, 2),
unidad_medida VARCHAR(20),
constraint PK_Med_componentes primary key(id),
constraint FK_MED_VARIOS FOREIGN KEY (Cod_medicamento) REFERENCES MEDICAMENTOS(Cod_medicamento),
constraint FK_COMP_VARIOS FOREIGN KEY (Cod_componente) REFERENCES COMPONENTES(Cod_componente),
)

--TABLA QUE UNE LOS MEDICAMENTOS CON LA OBRA SOCIAL Y DETERMINA SI ESTA TIENE COBERTURA Y DE QUE PORCENTAJE
create table COBERTURAS(
Cod_Cobertura int identity(1,1),
Cod_Obra_Social int not null,
Cod_Medicamento int not null,
Tipo_Cobertura varchar(20),
CONSTRAINT PK_COBERTURA PRIMARY KEY (Cod_Cobertura),
CONSTRAINT FK_COBERTURA_OBRASOCIAL FOREIGN KEY (Cod_Obra_Social) REFERENCES OBRAS_SOCIALES(Cod_Obra_Social),
CONSTRAINT FK_COBERTURA_MEDICAMENTOS FOREIGN KEY (Cod_Medicamento) REFERENCES MEDICAMENTOS(Cod_Medicamento)
)


Create Table OTROS_PRODUCTOS(
Cod_Producto int identity(1,1),
detalle varchar(30) not null,
Pre_unitario decimal(10,2)not null,
CONSTRAINT PK_PRODUCTO PRIMARY KEY (Cod_Producto)
)


------------------------------------------------------FIN PRODUCTOS Y OBRA SOCIAL------------------------------------------------------


------------------------------------------------------TABLAS DE ENTIDADES--------------------------------------------------------------



CREATE TABLE CLIENTES(
Id_Cliente int identity (1,1),
Nombre varchar(20) not null,
Apellido varchar (20) not null,
Documento int not null,
Calle varchar(30),
Altura int,
Id_Barrio int not null,
Nro_telefono int,
[e-mail] varchar(30),
Cod_Obra_Social int,
CONSTRAINT PK_Cliente PRIMARY KEY (id_cliente),
CONSTRAINT FK_CLIENTE_ObraSocial FOREIGN KEY (Cod_Obra_Social) REFERENCES OBRAS_SOCIALES(Cod_Obra_Social),
CONSTRAINT FK_CLIENTE_BARRIO FOREIGN KEY (Id_Barrio) REFERENCES BARRIOS(Id_Barrio)
)


Create Table SUCURSALES(
Nro_sucursal int identity(1,1),
Nom_Sede varchar (30)not null,
Calle varchar(30),
Altura int,
Id_Barrio int not null,
CONSTRAINT PK_FARMACIA PRIMARY KEY (Nro_sucursal),
CONSTRAINT FK_SUCURSAL_BARRIO FOREIGN KEY (Id_Barrio) REFERENCES BARRIOS(Id_Barrio)
)


Create Table EMPLEADOS(
Id_Empleado int identity(1,1),
Nom_Empleado varchar(20) not null,
Apell_Empleado varchar(20) not null,
Calle varchar(30),
Altura int,
Id_Barrio int not null,
Ocupacion varchar(50) not null,
Nro_sucursal int not null
CONSTRAINT PK_EMPLEADO PRIMARY KEY (Id_Empleado)
CONSTRAINT FK_EMPLEADO_FARMACIA FOREIGN KEY (Nro_sucursal) REFERENCES SUCURSALES(Nro_sucursal),
CONSTRAINT FK_EMPLEADO_BARRIO FOREIGN KEY (Id_Barrio) REFERENCES BARRIOS(Id_Barrio)
)
--------------------------------------------------FIN TABLAS DE ENTIDADES--------------------------------------------------------------

--------------------------------------------------TABLA DE ENTREGAS CON SUS DETALLES, SE SEPARAN POR SUCURSAL--------------------------
Create Table ENTREGAS(
Nro_Entrega int Identity (1,1),
Nro_sucursal int not null,
Fecha datetime,
Firma_Empresa Varchar(30) not null,
Resp_Entrega varchar(20) not null,
Id_Empleado int not null,
CONSTRAINT PK_ENTREGA PRIMARY KEY (Nro_Entrega),
CONSTRAINT FK_ENTREGA_FARMACIA FOREIGN KEY (Nro_sucursal) REFERENCES SUCURSALES(Nro_sucursal),
CONSTRAINT FK_ENTREGA_EMPLEADO FOREIGN KEY (Id_Empleado) REFERENCES EMPLEADOS(Id_Empleado)
)


Create Table DETALLE_ENTREGAS(
Nro_Detalle int identity(1,1),
Nro_Entrega int,
Cod_Medicamento int,
Cod_Producto int,
Cant int not null,
CONSTRAINT PK_DETALLE_ENTR PRIMARY KEY (Nro_Detalle),
CONSTRAINT FK_DETALLE_ENTREGA FOREIGN KEY (Nro_Entrega) REFERENCES ENTREGAS(Nro_Entrega),
CONSTRAINT FK_ENTREGA_MEDICAMENTO FOREIGN KEY (Cod_Medicamento) REFERENCES MEDICAMENTOS(Cod_Medicamento),
CONSTRAINT FK_ENTREGA_PRODUCTOS FOREIGN KEY (Cod_Producto) REFERENCES OTROS_PRODUCTOS(Cod_Producto)
)
-----------------------------------------------------FIN TABLAS DE ENTREGAS-----------------------------------------------------------

-----------------------------------------------------TABLA DE INVENTARIOS CON SUS DETALLES, SE SEPARAN POR SUCURSAL-------------------
Create Table INVENTARIOS(
Nro_Inventario int identity(1,1),
Nro_sucursal int not null,
fecha_inventario datetime,
Id_Empleado int not null,
CONSTRAINT PK_INVENTARIO PRIMARY KEY (Nro_Inventario),
CONSTRAINT FK_INVENTARIO_FARMACIA FOREIGN KEY (Nro_sucursal) REFERENCES SUCURSALES(Nro_sucursal),
CONSTRAINT FK_INVENTARIO_EMPLEADO FOREIGN KEY (Id_Empleado)REFERENCES EMPLEADOS (Id_Empleado)
)

Create Table DETALLE_INVENTARIOS(
Nro_Detalle int identity(1,1),
Nro_Inventario int,
Cod_Medicamento int,
Cod_Producto int,
Stock int not null,
CONSTRAINT PK_DETALLE_INV PRIMARY KEY (Nro_Detalle),
CONSTRAINT FK_DETALLE_INVENTARIO FOREIGN KEY (Nro_Inventario) REFERENCES INVENTARIOS(Nro_Inventario),
CONSTRAINT FK_INVENTARIO_MEDICAMENTO FOREIGN KEY (Cod_Medicamento) REFERENCES MEDICAMENTOS(Cod_Medicamento),
CONSTRAINT FK_INVENTARIO_PRODUCTOS FOREIGN KEY (Cod_Producto) REFERENCES OTROS_PRODUCTOS(Cod_Producto)
)
------------------------------------------------------FIN TABLAS DE INVENTARIOS---------------------------------------------------------


------------------------------------------------------TABLA DE FACTURAS CON SUS DETALLES Y FORMA DE PAGO, SE SEPARAN POR SUCURSAL-------

Create table FORMAS_DE_PAGO(
id_forma_pago int identity (1,1),
descripcion varchar(20),
constraint PK_forma_pago primary key(id_forma_pago)
)


Create Table FACTURAS(
Nro_Factura int identity(1,1),
Nro_sucursal int not null,
Fecha datetime not null,
Id_Empleado int not null,
Id_Cliente int not null,
id_forma_pago int not null,
CONSTRAINT PK_FACTURA PRIMARY KEY (Nro_Factura),
CONSTRAINT FK_FACTURA_FARMACIA FOREIGN KEY (Nro_sucursal) REFERENCES SUCURSALES(Nro_sucursal),
CONSTRAINT FK_FACTURAS_VENDEDOR FOREIGN KEY (Id_Empleado) REFERENCES EMPLEADOS(Id_Empleado),
CONSTRAINT FK_FACTURAS_CLIENTE FOREIGN KEY (Id_Cliente) REFERENCES CLIENTES(Id_Cliente),
constraint FK_Factura_FormaPago foreign key (id_forma_pago) references formas_de_pago(id_forma_pago)
)

Create Table DETALLE_FACTURAS(
Nro_Detalle int identity(1,1),
Nro_Factura int not null,
Cod_Medicamento int,
Cod_Producto int,
Pre_Unitario numeric(18,2) not null,
Cantidad int not null,
Descuento_por_obra_Social decimal(18,2),
CONSTRAINT PK_DETALLE_FAC PRIMARY KEY (Nro_Detalle),
CONSTRAINT FK_DETALLE_FACTURA FOREIGN KEY (Nro_Factura) REFERENCES FACTURAS(Nro_Factura),
CONSTRAINT FK_FACTURA_MEDICAMENTO FOREIGN KEY (Cod_Medicamento) REFERENCES MEDICAMENTOS(Cod_Medicamento),
CONSTRAINT FK_FACTURA_PRODUCTOS FOREIGN KEY (Cod_Producto) REFERENCES OTROS_PRODUCTOS(Cod_Producto)
)
--------------------------------------------------------FIN TABLAS DE FACTURAS----------------------------------------------------------



/*
**
**
Segunda parte
**
**
*/

USE [Grupo_4_Farmacia_1w1]
GO


-------------------------------------------------- TABLAS DE LOCALIZACIÓN ----------------------------------------------------------

INSERT INTO PROVINCIAS (Nombre_Provincia) VALUES
('Córdoba'),                   --1
('Buenos Aires'),              --2
('Santa Fe'),                  --3
('Mendoza'),                   --4
('Tucumán'),                   --5
('Salta'),                     --6
('Neuquén'),                   --7
('Entre Ríos'),                --8
('Chaco'),                     --9
('Misiones')                   --10

INSERT INTO LOCALIDADES (Nombre_Localidad, Id_Provincia) VALUES
('Córdoba Capital', 1),       --1
('Villa Carlos Paz', 1),      --2
('Río Cuarto', 1),            --3
('La Plata', 2),              --4
('Mar del Plata', 2),         --5
('Rosario', 3),               --6
('Mendoza Capital', 4),       --7
('San Miguel de Tucumán', 5), --8
('Salta Capital', 6),         --9
('Neuquén Capital', 7)        --10

INSERT INTO BARRIOS (Nombre_Barrio, Id_Localidad) VALUES
('Nueva Córdoba', 1),         --1
('Alberdi', 1),               --2
('Cerro de las Rosas', 1),    --3
('Villa del Lago', 2),        --4
('Banda Norte', 3),           --5
('Los Hornos', 4),            --6
('La Perla', 5),              --7
('Pichincha', 6),             --8
('Quinta Sección', 7),        --9
('Barrio Norte', 9)           --10

-------------------------------------------------- TABLAS PRODUCTOS Y OBRA SOCIAL ------------------------------------------------------

INSERT INTO OBRAS_SOCIALES (Detalle) VALUES
('OSDE'),                     --1
('Swiss Medical'),            --2
('PAMI'),                     --3
('Galeno'),                   --4
('Medifé'),                   --5
('OSECAC'),                   --6
('IOMA'),                     --7
('APROSS'),                   --8
('Sancor Salud'),             --9
('Jerárquicos Salud')         --10

INSERT INTO MEDICAMENTOS (Nombre_Medicamento, Prescripcion_Medica, Descripcion, Indicaciones_Uso, Precio_Unitario) VALUES
('Ibuprofeno 400 mg', 0, 'Antiinflamatorio no esteroideo', 'Tomar 1 comprimido cada 8 horas con las comidas', 2450.50), --1
('Paracetamol 500 mg', 0, 'Analgésico y antipirético', 'Tomar 1 comprimido cada 6 u 8 horas', 1800.00),                 --2
('Amoxicilina 500 mg', 1, 'Antibiótico de amplio espectro', 'Tomar 1 cápsula cada 8 horas durante 7 días', 5200.75),    --3
('Losartán 50 mg', 1, 'Antihipertensivo', 'Tomar 1 comprimido por día por la mañana', 4100.00),                         --4
('Omeprazol 20 mg', 0, 'Inhibidor de la bomba de protones', 'Tomar 1 cápsula en ayunas', 3300.25),                      --5
('Metformina 850 mg', 1, 'Antidiabético oral', 'Tomar 1 comprimido con el desayuno y la cena', 3950.00),                --6
('Loratadina 10 mg', 0, 'Antihistamínico', 'Tomar 1 comprimido por día', 2100.90),                                      --7
('Atorvastatina 20 mg', 1, 'Reductor del colesterol', 'Tomar 1 comprimido por la noche', 6800.00),                      --8
('Diclofenac 75 mg', 1, 'Antiinflamatorio y analgésico', 'Tomar 1 comprimido cada 12 horas', 2900.40),                  --9
('Salbutamol 100 mcg', 1, 'Broncodilatador en aerosol', 'Inhalar 1 o 2 disparos cada 6 horas', 4750.00),                 --10
('Ibupirac Plus / Melox / Relajante', 1, 'Analgésico, relajante muscular y antiinflamatorio', 'Tomar 1 comprimido cada 12 horas', 3850.00), -- 11
('Migral / Compositum', 0, 'Analgésico y vasopresor para cefaleas', 'Tomar 1 comprimido ante el primer síntoma de dolor de cabeza', 2950.50), -- 12
('Paracetamol c/ Antihistamínico', 0, 'Alivio de síntomas gripales y congestión', 'Tomar 1 comprimido cada 8 horas', 2300.00); -- 13

INSERT INTO LOTES (Nro_Lote, Cod_Medicamento, Fecha_Fabricacion, Fecha_Vencimiento) VALUES
(1001, 1, '20250110', '20270110'),
(1002, 2, '20250215', '20270215'),
(1003, 3, '20250320', '20260920'),
(1004, 4, '20250405', '20270405'),
(1005, 5, '20250518', '20270518'),
(1006, 6, '20250622', '20270622'),
(1007, 7, '20250730', '20270730'),
(1008, 8, '20250812', '20270812'),
(1009, 9, '20250925', '20270925'),
(1010, 10, '20251008', '20271008')

INSERT INTO COMPONENTES (Detalle) VALUES 
('Ibuprofeno'),                  --1
('Paracetamol'),                 --2
('Amoxicilina trihidrato'),      --3
('Losartán potásico'),           --4
('Omeprazol magnésico'),         --5
('Metformina clorhidrato'),      --6
('Loratadina'),                  --7
('Atorvastatina cálcica'),       --8
('Diclofenac sódico'),           --9
('Salbutamol sulfato'),           --10
('Orfenadrina citrato'),       -- 11
('Cafeína anhidra'),            -- 12
('Clorfenamina maleato');       -- 13

INSERT INTO MEDICAMENTOS_COMPONENTES (Cod_medicamento, Cod_componente, cantidad, unidad_medida) VALUES
(1, 1, 400.00, 'mg'),            --1
(2, 2, 500.00, 'mg'),            --2
(3, 3, 500.00, 'mg'),            --3
(4, 4, 50.00, 'mg'),             --4
(5, 5, 20.00, 'mg'),             --5
(6, 6, 850.00, 'mg'),            --6
(7, 7, 10.00, 'mg'),             --7
(8, 8, 20.00, 'mg'),             --8
(9, 9, 75.00, 'mg'),             --9
(10, 10, 100.00, 'mcg'),          --10
-- Medicamento 11: Ibuprofeno 400 mg + Orfenadrina citrato 35 mg (Relajante muscular)
(11, 1, 400.00, 'mg'),
(11, 11, 35.00, 'mg'),
-- Medicamento 12: Ergotamina/Paracetamol 500 mg + Cafeína 100 mg (Para migrañas)
(12, 2, 500.00, 'mg'),
(12, 12, 100.00, 'mg'),
-- Medicamento 13: Paracetamol 500 mg + Clorfenamina maleato 4 mg (Antigripal)
(13, 2, 500.00, 'mg'),
(13, 13, 4.00, 'mg');


INSERT INTO COBERTURAS (Cod_Obra_Social, Cod_Medicamento, Tipo_Cobertura) VALUES
--ibuprofeno
(1, 1, '40%'),    --1 Ibuprofeno 400 mg (OSDE)
(2, 1, '50%'),    --2 Swiss Medical
(3, 1, '70%'),    --3 PAMI
(4, 1, '40%'),    --4 Galeno
--paracetamol
(2, 2, '50%'),    --5 Paracetamol 500 mg (Swiss Medical)
(2, 1, '50%'),    --6 Swiss Medical
(3, 1, '70%'),    --7 PAMI
(4, 1, '40%'),    --8 Galeno
--amoxicilina
(3, 3, '70%'),    --9 Amoxicilina 500 mg (PAMI)
(1, 3, '40%'),    --10 OSDE
(2, 3, '50%'),    --11 Swiss Medical
(8, 3, '70%'),    --12 APROS
--demas
(4, 4, '40%'),    --13 Losartán 50 mg (Galeno)
(5, 5, '50%'),    --14 Omeprazol 20 mg (Medifé)
(6, 6, '100%'),   --15 Metformina 850 mg (OSECAC)
(7, 7, '40%'),    --16 Loratadina 10 mg (IOMA)
(8, 8, '70%'),    --17 Atorvastatina 20 mg (APROSS)
(9, 9, '50%'),    --18 Diclofenac 75 mg (Sancor Salud)
(10, 10, '70%'),  --19 Salbutamol 100 mcg (Jerárquicos Salud)
(1, 11, '40%'),   --20 Ibupirac Plus / Melox / Relajante (OSDE)
(2, 12, '50%'),   --21 Migral / Compositum (Swiss Medical)
(3, 13, '60%');   --22 Paracetamol c/ Antihistamínico (PAMI)


INSERT INTO OTROS_PRODUCTOS (detalle, Pre_unitario) VALUES
('Alcohol en gel 250 ml', 2300.00),       --1
('Barbijo descartable x50', 4800.00),     --2
('Termómetro digital', 7500.00),          --3
('Jabón neutro', 1500.00),                --4
('Protector solar FPS 50', 12900.00),     --5
('Cepillo dental', 2100.00),              --6
('Crema hidratante', 6400.00),            --7
('Gasas estériles x10', 1900.00),         --8
('Curitas x20', 1200.00),                 --9
('Shampoo anticaspa', 5600.00)            --10

------------------------------------------------------ TABLAS DE ENTIDADES --------------------------------------------------------------

INSERT INTO CLIENTES (Nombre, Apellido, Documento, Calle, Altura, Id_Barrio, Nro_telefono, [e-mail], Cod_Obra_Social) VALUES
-- Sucursal 1, 2 y 3: Localidad Córdoba Capital (Barrios disponibles: 1, 2, 3)
('Lucía', 'Gómez', 30123456, 'Av. Colón', 450, 1, 351234567, 'lucia.gomez@mail.com', 1),         --1
('Martín', 'Fernández', 28456789, 'San Martín', 1200, 2, 351345678, 'martin.fernandez@mail.com', 2), --2
('Sofía', 'Rodríguez', 35789012, 'Belgrano', 320, 3, 351456789, 'sofia.rodriguez@mail.com', 3),       --3
('Diego', 'López', 27345678, 'Sarmiento', 875, 2, 354123456, 'diego.lopez@mail.com', 4),             --4
('Camila', 'Martínez', 38901234, 'Rivadavia', 150, 3, 358234567, 'camila.martinez@mail.com', 5),       --5
('Julián', 'Pérez', 32567890, 'Mitre', 640, 1, 221345678, 'julian.perez@mail.com', 3),               --6
('Valentina', 'Sosa', 40123789, 'Alsina', 980, 3, 223456789, 'valentina.sosa@mail.com', 7),           --7
('Nicolás', 'Díaz', 29876543, 'Urquiza', 2100, 1, 341567890, 'nicolas.diaz@mail.com', 8),              --8
('Florencia', 'Torres', 36234567, 'Lavalle', 75, 2, 261678901, 'flor.torres@mail.com', NULL),         --9
-- Sucursal 4: Carlos Paz (Localidad Villa Carlos Paz - Barrio: 4)
('Agustín', 'Ramírez', 33456123, 'Güemes', 430, 4, 387789012, 'agustin.ramirez@mail.com', 10),        --10
('Mariana', 'Giménez', 34567890, 'San Martín', 220, 4, 351654321, 'mariana.gimenez@mail.com', 1),     --11
('Federico', 'Ruiz', 31234567, 'Gral. Paz', 510, 4, 351789123, 'federico.ruiz@mail.com', 2),          --12
-- Sucursal 5: Río Cuarto (Localidad Río Cuarto - Barrio: 5)
('Esteban', 'Moreno', 29012345, 'Rivadavia', 890, 5, 358412345, 'esteban.moreno@mail.com', 3),         --13
('Natalia', 'Acosta', 37890123, 'Alvear', 310, 5, 358456789, 'natalia.acosta@mail.com', 4),            --14
('Lucas', 'Benítez', 35678901, 'Belgrano', 1020, 5, 358478901, 'lucas.benitez@mail.com', 5),           --15
-- Sucursal 6: La Plata (Localidad La Plata - Barrio: 6)
('Paula', 'Vera', 32456789, 'Calle 8', 750, 6, 221412345, 'paula.vera@mail.com', 6),                   --16
('Gonzalo', 'Silva', 28901234, 'Calle 12', 1340, 6, 221456789, 'gonzalo.silva@mail.com', 7),           --17
('Carla', 'Medina', 39012345, 'Calle 50', 520, 6, 221478901, 'carla.medina@mail.com', 8),             --18
-- Sucursal 7: Mar del Plata (Localidad Mar del Plata - Barrio: 7)
('Ezequiel', 'Rojas', 31567890, 'San Martín', 1890, 7, 223412345, 'ezequiel.rojas@mail.com', 9),       --19
('Brenda', 'Flores', 36789012, 'Rivadavia', 430, 7, 223456789, 'brenda.flores@mail.com', 10),          --20
('Matías', 'Ortiz', 27890123, 'Independencia', 2150, 7, 223478901, 'matias.ortiz@mail.com', 1),        --21
-- Sucursal 8: Rosario (Localidad Rosario - Barrio: 8)
('Romina', 'Peralta', 33123456, 'Córdoba', 1120, 8, 341412345, 'romina.peralta@mail.com', 2),          --22
('Gabriel', 'Castro', 29678901, 'San Luis', 890, 8, 341456789, 'gabriel.castro@mail.com', 3),          --23
('Valeria', 'Soria', 38123456, 'Rioja', 450, 8, 341478901, 'valeria.soria@mail.com', 4),               --24
-- Sucursal 9: Mendoza (Localidad Mendoza Capital - Barrio: 9)
('Ignacio', 'Varela', 30456789, 'San Martín', 1230, 9, 261412345, 'ignacio.varela@mail.com', 5),       --25
('Daniela', 'Romero', 35123456, 'Las Heras', 670, 9, 261456789, 'daniela.romero@mail.com', 6),          --26
('Franco', 'Morales', 28123456, 'Chile', 940, 9, 261478901, 'franco.morales@mail.com', 7),             --27
-- Sucursal 10: Salta (Localidad Salta Capital - Barrio: 10)
('Camila', 'Navarro', 37123456, 'España', 520, 10, 387412345, 'camila.navarro@mail.com', 8),           --28
('Joaquín', 'Duarte', 31901234, 'Mitre', 310, 10, 387456789, 'joaquin.duarte@mail.com', 9),            --29
('Agustina', 'Luna', 36456789, 'Buenos Aires', 840, 10, 387478901, 'agustina.luna@mail.com', 10);      --30

INSERT INTO SUCURSALES (Nom_Sede, Calle, Altura, Id_Barrio) VALUES
('Sucursal Nueva Córdoba', 'Av. Hipólito Yrigoyen', 500, 1),  --1
('Sucursal Alberdi', 'Av. Colón', 1800, 2),                   --2
('Sucursal Cerro', 'Av. Rafael Núñez', 4200, 3),              --3
('Sucursal Carlos Paz', 'Av. Cárcano', 300, 4),               --4
('Sucursal Río Cuarto', 'Constitución', 650, 5),              --5
('Sucursal La Plata', 'Calle 7', 1100, 6),                    --6
('Sucursal Mar del Plata', 'Av. Luro', 2300, 7),              --7
('Sucursal Rosario', 'Av. Pellegrini', 900, 8),               --8
('Sucursal Mendoza', 'Av. San Martín', 1500, 9),              --9
('Sucursal Salta', 'Caseros', 700, 10)                        --10

INSERT INTO EMPLEADOS (Nom_Empleado, Apell_Empleado, Calle, Altura, Id_Barrio, Ocupacion, Nro_sucursal) VALUES
-- Sucursal 1: Nueva Córdoba (Id_Barrio: 1)
('Carolina', 'Benítez', 'Buenos Aires', 450, 1, 'Farmacéutica', 1),         --1
('Juan', 'Pérez', 'Rondeau', 320, 1, 'Cajero', 1),                          --2
('Lucía', 'Giménez', 'San Lorenzo', 150, 1, 'Repositor', 1),                --3
-- Sucursal 2: Alberdi (Id_Barrio: 2)
('Facundo', 'Acosta', '9 de Julio', 1200, 2, 'Cajero', 2),                  --4
('Valeria', 'Ríos', 'Duarte Quirós', 1500, 2, 'Farmacéutica', 2),           --5
('Esteban', 'Paz', 'Tafí', 430, 2, 'Auxiliar', 2),                          --6
-- Sucursal 3: Cerro (Id_Barrio: 3)
('Mariana', 'Herrera', 'Menéndez Pidal', 3500, 3, 'Gerente de sucursal', 3),--7
('Lucas', 'Vera', 'Tejada', 220, 3, 'Farmacéutico', 3),                     --8
('Camila', 'Soria', 'Gavier', 780, 3, 'Cajera', 3),                         --9
-- Sucursal 4: Carlos Paz (Id_Barrio: 4)
('Lautaro', 'Molina', 'Illia', 400, 4, 'Auxiliar de farmacia', 4),          --10
('Sofía', 'Navarro', 'San Martín', 150, 4, 'Cajera', 4),                    --11
('Mateo', 'Gómez', 'Leloir', 520, 4, 'Farmacéutico', 4),                    --12
-- Sucursal 5: Río Cuarto (Id_Barrio: 5)
('Rocío', 'Castro', 'Paso de los Libres', 300, 5, 'Farmacéutica', 5),       --13
('Joaquín', 'Duarte', 'Sabattini', 1100, 5, 'Repositor', 5),                --14
('Agustina', 'Luna', 'Mosconi', 620, 5, 'Cajera', 5),                       --15
-- Sucursal 6: La Plata (Id_Barrio: 6)
('Matías', 'Ortiz', 'Calle 65', 800, 6, 'Repositor', 6),                    --16
('Martina', 'Silva', 'Calle 60', 1200, 6, 'Farmacéutica', 6),               --17
('Tomás', 'Benítez', 'Calle 66', 950, 6, 'Cajero', 6),                      --18
-- Sucursal 7: Mar del Plata (Id_Barrio: 7)
('Daniela', 'Silva', 'Colón', 1500, 7, 'Cajera', 7),                        --19
('Bruno', 'Alvarez', 'San Juan', 2000, 7, 'Farmacéutico', 7),               --20
('Florencia', 'Medina', 'España', 1100, 7, 'Auxiliar', 7),                  --21
-- Sucursal 8: Rosario (Id_Barrio: 8)
('Sebastián', 'Romero', 'Salguero', 400, 8, 'Farmacéutico', 8),             --22
('Natalia', 'Juárez', 'Brown', 650, 8, 'Cajera', 8),                        --23
('Ignacio', 'Varela', 'Alem', 300, 8, 'Repositor', 8),                      --24
-- Sucursal 9: Mendoza (Id_Barrio: 9)
('Paula', 'Medina', 'Boulogne Sur Mer', 500, 9, 'Auxiliar de farmacia', 9), --25
('Franco', 'Morales', 'Martín Zapata', 250, 9, 'Cajero', 9),                --26
('Carla', 'Iglesias', 'Martínez de Rozas', 710, 9, 'Farmacéutica', 9),      --27
-- Sucursal 10: Salta (Id_Barrio: 10)
('Gonzalo', 'Rojas', 'Zuviría', 300, 10, 'Gerente de sucursal', 10),        --28
('Brenda', 'Flores', 'Pellegrini', 550, 10, 'Farmacéutica', 10),            --29
('Ezequiel', 'Castillo', 'Necochea', 120, 10, 'Cajero', 10);                --30

-------------------------------------------------- TABLAS DE ENTREGAS --------------------------------------------------------------

INSERT INTO ENTREGAS (Nro_sucursal, Fecha, Firma_Empresa, Resp_Entrega, Id_Empleado) VALUES
(1, '20251001', 'Droguería del Sur', 'Pablo Ledesma', 1),       --1 (Sucursal 1 - Empleado de Sucursal 1)
(2, '20251002', 'Distribuidora Médica', 'Ariel Núñez', 4),      --2 (Sucursal 2 - Empleado de Sucursal 2)
(3, '20251003', 'Farmadistribuciones', 'Lorena Paz', 7),         --3 (Sucursal 3 - Empleado de Sucursal 3)
(4, '20251004', 'Droguería Central', 'Hugo Barrios', 10),       --4 (Sucursal 4 - Empleado de Sucursal 4)
(5, '20251005', 'Medicorp Argentina', 'Natalia Vega', 13),      --5 (Sucursal 5 - Empleado de Sucursal 5)
(6, '20251006', 'Droguería del Sur', 'Pablo Ledesma', 16),      --6 (Sucursal 6 - Empleado de Sucursal 6)
(7, '20251007', 'Distribuidora Médica', 'Ariel Núñez', 19),     --7 (Sucursal 7 - Empleado de Sucursal 7)
(8, '20251008', 'Farmadistribuciones', 'Lorena Paz', 22),       --8 (Sucursal 8 - Empleado de Sucursal 8)
(9, '20251009', 'Droguería Central', 'Hugo Barrios', 25),       --9 (Sucursal 9 - Empleado de Sucursal 9)
(10, '20251010', 'Medicorp Argentina', 'Natalia Vega', 28);     --10 (Sucursal 10 - Empleado de Sucursal 10)

INSERT INTO DETALLE_ENTREGAS (Nro_Entrega, Cod_Medicamento, Cod_Producto, Cant) VALUES
-- Sucursal 1 (Córdoba Capital): Dos detalles (1 Medicamento y 1 Producto)
(1, 1, NULL, 50),     --1 (Detalle de Medicamento: Ibuprofeno 400 mg)
(1, NULL, 1, 30),     --2 (Detalle de Producto: Alcohol en gel 250 ml)

-- Sucursal 2 (Córdoba Capital): Dos detalles (1 Medicamento y 1 Producto)
(2, 2, NULL, 40),     --3 (Detalle de Medicamento: Paracetamol 500 mg)
(2, NULL, 2, 25),     --4 (Detalle de Producto: Barbijo descartable x50)

-- Sucursal 3 (Córdoba Capital): Dos detalles (1 Medicamento y 1 Producto)
(3, 3, NULL, 60),     --5 (Detalle de Medicamento: Amoxicilina 500 mg)
(3, NULL, 3, 15),     --6 (Detalle de Producto: Termómetro digital)

-- Sucursal 4 (Carlos Paz): Un detalle (Producto)
(4, NULL, 4, 50),     --7 (Jabón neutro)
-- Sucursal 5 (Río Cuarto): Un detalle (Medicamento)
(5, 5, NULL, 45),     --8 (Omeprazol 20 mg)
-- Sucursal 6 (La Plata): Un detalle (Producto)
(6, NULL, 5, 20),     --9 (Protector solar FPS 50)
-- Sucursal 7 (Mar del Plata): Un detalle (Medicamento)
(7, 7, NULL, 35),     --10 (Loratadina 10 mg)
-- Sucursal 8 (Rosario): Un detalle (Producto)
(8, NULL, 6, 40),     --11 (Cepillo dental)
-- Sucursal 9 (Mendoza): Un detalle (Medicamento)
(9, 9, NULL, 30),     --12 (Diclofenac 75 mg)
-- Sucursal 10 (Salta): Un detalle (Producto)
(10, NULL, 8, 50);    --13 (Gasas estériles x10)

-------------------------------------------------- TABLAS DE INVENTARIOS -----------------------------------------------------------

INSERT INTO INVENTARIOS (Nro_sucursal, fecha_inventario, Id_Empleado) VALUES
(1, '20251015 10:00:00', 1),   --1 (Sucursal 1 - Empleado 1)
(2, '20251015 10:30:00', 4),   --2 (Sucursal 2 - Empleado 4)
(3, '20251015 11:00:00', 7),   --3 (Sucursal 3 - Empleado 7)
(4, '20251015 11:30:00', 10),  --4 (Sucursal 4 - Empleado 10)
(5, '20251015 12:00:00', 13),  --5 (Sucursal 5 - Empleado 13)
(6, '20251015 12:30:00', 16),  --6 (Sucursal 6 - Empleado 16)
(7, '20251015 13:00:00', 19),  --7 (Sucursal 7 - Empleado 19)
(8, '20251015 13:30:00', 22),  --8 (Sucursal 8 - Empleado 22)
(9, '20251015 14:00:00', 25),  --9 (Sucursal 9 - Empleado 25)
(10, '20251015 14:30:00', 28); --10 (Sucursal 10 - Empleado 28)

INSERT INTO DETALLE_INVENTARIOS (Nro_Inventario, Cod_Medicamento, Cod_Producto, Stock) VALUES
-- Inventario 1 (Sucursal 1) -> 4 medicamentos y 1 producto
(1, 1, NULL, 85),     --1
(1, 2, NULL, 60),     --2
(1, 3, NULL, 40),     --3
(1, 4, NULL, 75),     --4
(1, NULL, 1, 30),     --5
-- Inventario 2 (Sucursal 2) -> 3 medicamentos y 2 productos
(2, 5, NULL, 50),     --6
(2, 6, NULL, 90),     --7
(2, 7, NULL, 45),     --8
(2, NULL, 2, 25),     --9
(2, NULL, 3, 15),     --10
-- Inventario 3 (Sucursal 3) -> 5 medicamentos
(3, 8, NULL, 100),    --11
(3, 9, NULL, 35),     --12
(3, 10, NULL, 55),    --13
(3, 11, NULL, 20),    --14
(3, 12, NULL, 40),    --15
-- Inventario 4 (Sucursal 4) -> 4 medicamentos y 1 producto
(4, 1, NULL, 65),     --16
(4, 3, NULL, 50),     --17
(4, 5, NULL, 80),     --18
(4, 7, NULL, 30),     --19
(4, NULL, 4, 45),     --20
-- Inventario 5 (Sucursal 5) -> 3 medicamentos y 2 productos
(5, 2, NULL, 70),     --21
(5, 4, NULL, 95),     --22
(5, 6, NULL, 40),     --23
(5, NULL, 5, 20),     --24
(5, NULL, 6, 35),     --25
-- Inventario 6 (Sucursal 6) -> 5 medicamentos
(6, 8, NULL, 60),     --26
(6, 9, NULL, 85),     --27
(6, 10, NULL, 50),    --28
(6, 11, NULL, 30),    --29
(6, 13, NULL, 45),    --30
-- Inventario 7 (Sucursal 7) -> 4 medicamentos y 1 producto
(7, 1, NULL, 90),     --31
(7, 2, NULL, 45),     --32
(7, 3, NULL, 60),     --33
(7, 4, NULL, 75),     --34
(7, NULL, 7, 25),     --35
-- Inventario 8 (Sucursal 8) -> 3 medicamentos y 2 productos
(8, 5, NULL, 55),     --36
(8, 6, NULL, 70),     --37
(8, 7, NULL, 40),     --38
(8, NULL, 8, 50),     --39
(8, NULL, 9, 30),     --40
-- Inventario 9 (Sucursal 9) -> 5 medicamentos
(9, 8, NULL, 80),     --41
(9, 9, NULL, 65),     --42
(9, 10, NULL, 35),    --43
(9, 11, NULL, 50),    --44
(9, 12, NULL, 25),    --45
-- Inventario 10 (Sucursal 10) -> 4 medicamentos y 1 producto
(10, 1, NULL, 70),    --46
(10, 2, NULL, 55),    --47
(10, 3, NULL, 90),    --48
(10, 4, NULL, 40),    --49
(10, NULL, 10, 35);   --50

-------------------------------------------------- TABLAS DE FACTURAS --------------------------------------------------------------

INSERT INTO FORMAS_DE_PAGO (descripcion) VALUES
('Efectivo'),                 --1
('Tarjeta de débito'),        --2
('Tarjeta de crédito'),       --3
('Transferencia'),            --4
('Mercado Pago'),             --5
('Cheque'),                   --6
('Billetera virtual'),        --7
('Débito automático'),        --8
('Cuenta corriente'),         --9
('Código QR')                 --10

INSERT INTO FACTURAS (Nro_sucursal, Fecha, Id_Empleado, Id_Cliente, id_forma_pago) VALUES
-- --- AÑO 20223 ---
(1, '20230315 10:30:00', 1, 1, 1),   -- 1
(1, '20230720 15:45:00', 2, 1, 2),   -- 2
(2, '20230510 11:00:00', 4, 2, 3),   -- 3
(3, '20230912 16:20:00', 7, 3, 1),   -- 4
(4, '20230405 09:15:00', 10, 4, 2),  -- 5
(6, '20231118 14:30:00', 16, 6, 5),  -- 6
-- --- AÑO 2024 ---
(1, '20240210 10:00:00', 3, 1, 1),   -- 7
(2, '20240614 12:30:00', 5, 2, 4),   -- 8
(2, '20241005 17:00:00', 6, 2, 2),   -- 9
(3, '20240322 11:20:00', 8, 3, 1),   -- 10
(3, '20240815 15:10:00', 9, 3, 3),   -- 11
(5, '20240530 10:45:00', 13, 5, 2),  -- 12
(7, '20240919 16:00:00', 19, 7, 1),  -- 13
(8, '20241125 12:15:00', 22, 8, 5),  -- 14
-- --- AÑO 2025 ---
(1, '20250112 09:45:00', 1, 1, 2),   -- 15
(1, '20250418 14:20:00', 2, 1, 1),   -- 16
(1, '20250822 11:30:00', 3, 1, 3),   -- 17
(2, '20250308 15:15:00', 4, 2, 1),   -- 18
(3, '20250214 10:10:00', 7, 3, 2),   -- 19
(3, '20250619 16:40:00', 8, 3, 4),   -- 20
(3, '20251011 12:00:00', 9, 3, 1),   -- 21
(9, '20250527 11:50:00', 25, 9, 2),  -- 22
(10, '20250903 14:10:00', 28, 10, 5), -- 23
-- --- AÑO 2026 (Actual) ---
(1, '20260115 10:00:00', 1, 1, 1),   -- 24
(2, '20260220 11:30:00', 5, 2, 2),   -- 25
(3, '20260310 15:00:00', 7, 3, 1);   -- 26


INSERT INTO DETALLE_FACTURAS (Nro_Factura, Cod_Medicamento, Cod_Producto, Pre_Unitario, Cantidad, Descuento_por_obra_Social) VALUES
-- Factura 1
(1, 1, NULL, 2450.50, 1, 980.20),
(1, NULL, 1, 2300.00, 1, 0.00),
-- Factura 2
(2, NULL, 2, 4800.00, 1, 0.00),
-- Factura 3
(3, NULL, 3, 7500.00, 1, 0.00),
-- Factura 4
(4, 3, NULL, 5200.75, 1, 3640.52),
-- Factura 5
(5, NULL, 4, 1500.00, 2, 0.00),
-- Factura 6
(6, NULL, 5, 12900.00, 1, 0.00),
-- Factura 7
(7, NULL, 6, 2100.00, 1, 0.00),
-- Factura 8
(8, 2, NULL, 1800.00, 1, 900.00),
-- Factura 9
(9, NULL, 7, 6400.00, 1, 0.00),
-- Factura 10
(10, 4, NULL, 4100.00, 1, 1640.00),
-- Factura 11
(11, 5, NULL, 3300.25, 1, 1650.12),
-- Factura 12
(12, NULL, 8, 1900.00, 2, 0.00),
-- Factura 13
(13, NULL, 9, 1200.00, 1, 0.00),
-- Factura 14
(14, NULL, 10, 5600.00, 1, 0.00),
-- Factura 15
(15, 6, NULL, 3950.00, 1, 3950.00),
-- Factura 16
(16, NULL, 1, 2300.00, 1, 0.00),
-- Factura 17
(17, 8, NULL, 6800.00, 1, 4760.00),
-- Factura 18
(18, NULL, 2, 4800.00, 1, 0.00),
-- Factura 19
(19, 10, NULL, 4750.00, 1, 3325.00),
-- Factura 20
(20, 1, NULL, 2450.50, 2, 1960.40),
-- Factura 21
(21, 3, NULL, 5200.75, 1, 3640.52),
-- Factura 22
(22, NULL, 3, 7500.00, 1, 0.00),
-- Factura 23
(23, NULL, 4, 1500.00, 1, 0.00),
-- Factura 24
(24, 2, NULL, 1800.00, 1, 900.00),
-- Factura 25
(25, NULL, 5, 12900.00, 1, 0.00),
-- Factura 26
(26, 4, NULL, 4100.00, 1, 1640.00);


ALTER TABLE DETALLE_FACTURAS 
ADD reembolsado BIT NOT NULL DEFAULT 0;
Go


UPDATE df
SET df.reembolsado = 1
FROM DETALLE_FACTURAS df
JOIN FACTURAS f ON df.Nro_Factura = f.Nro_Factura
WHERE df.Descuento_por_obra_Social > 0
  AND YEAR(f.Fecha) < 2026;
go



alter table EMPLEADOS
ADD email varchar(100)
go


create table USUARIOS
(
id_empleado int primary key,
contrasenia varchar(255) not null,
correo varchar(100) not null
)
go

UPDATE EMPLEADOS
SET email = CASE Id_Empleado
    WHEN 1 THEN 'carolina.benitez@farmacia.com'
    WHEN 2 THEN 'juan.perez@farmacia.com'
    WHEN 3 THEN 'lucia.gimenez@farmacia.com'
    WHEN 4 THEN 'facundo.acosta@farmacia.com'
    WHEN 5 THEN 'valeria.rios@farmacia.com'
    WHEN 6 THEN 'esteban.paz@farmacia.com'
    WHEN 7 THEN 'mariana.herrera@farmacia.com'
    WHEN 8 THEN 'lucas.vera@farmacia.com'
    WHEN 9 THEN 'camila.soria@farmacia.com'
    WHEN 10 THEN 'lautaro.molina@farmacia.com'
    WHEN 11 THEN 'sofia.navarro@farmacia.com'
    WHEN 12 THEN 'mateo.gomez@farmacia.com'
    WHEN 13 THEN 'rocio.castro@farmacia.com'
    WHEN 14 THEN 'joaquin.duarte@farmacia.com'
    WHEN 15 THEN 'agustina.luna@farmacia.com'
    WHEN 16 THEN 'matias.ortiz@farmacia.com'
    WHEN 17 THEN 'martina.silva@farmacia.com'
    WHEN 18 THEN 'tomas.benitez@farmacia.com'
    WHEN 19 THEN 'daniela.silva@farmacia.com'
    WHEN 20 THEN 'bruno.alvarez@farmacia.com'
    WHEN 21 THEN 'florencia.medina@farmacia.com'
    WHEN 22 THEN 'sebastian.romero@farmacia.com'
    WHEN 23 THEN 'natalia.juarez@farmacia.com'
    WHEN 24 THEN 'ignacio.varela@farmacia.com'
    WHEN 25 THEN 'paula.medina@farmacia.com'
    WHEN 26 THEN 'franco.morales@farmacia.com'
    WHEN 27 THEN 'carla.iglesias@farmacia.com'
    WHEN 28 THEN 'gonzalo.rojas@farmacia.com'
    WHEN 29 THEN 'brenda.flores@farmacia.com'
    WHEN 30 THEN 'ezequiel.castillo@farmacia.com'
END
WHERE Id_Empleado BETWEEN 1 AND 30;
go

alter table empleados
alter column email varchar(100) not null
go

UPDATE EMPLEADOS SET email = 'carolina.benitez@gmail.com' WHERE Id_Empleado = 1;
UPDATE EMPLEADOS SET email = 'juan.perez@gmail.com' WHERE Id_Empleado = 2;
UPDATE EMPLEADOS SET email = 'lucia.gimenez@gmail.com' WHERE Id_Empleado = 3;
UPDATE EMPLEADOS SET email = 'facundo.acosta@gmail.com' WHERE Id_Empleado = 4;
UPDATE EMPLEADOS SET email = 'valeria.rios@gmail.com' WHERE Id_Empleado = 5;
UPDATE EMPLEADOS SET email = 'esteban.paz@gmail.com' WHERE Id_Empleado = 6;
UPDATE EMPLEADOS SET email = 'mariana.herrera@gmail.com' WHERE Id_Empleado = 7;
UPDATE EMPLEADOS SET email = 'lucas.vera@gmail.com' WHERE Id_Empleado = 8;
UPDATE EMPLEADOS SET email = 'camila.soria@gmail.com' WHERE Id_Empleado = 9;
UPDATE EMPLEADOS SET email = 'lautaro.molina@gmail.com' WHERE Id_Empleado = 10;
UPDATE EMPLEADOS SET email = 'sofia.navarro@gmail.com' WHERE Id_Empleado = 11;
UPDATE EMPLEADOS SET email = 'mateo.gomez@gmail.com' WHERE Id_Empleado = 12;
UPDATE EMPLEADOS SET email = 'rocio.castro@gmail.com' WHERE Id_Empleado = 13;
UPDATE EMPLEADOS SET email = 'joaquin.duarte@gmail.com' WHERE Id_Empleado = 14;
UPDATE EMPLEADOS SET email = 'agustina.luna@gmail.com' WHERE Id_Empleado = 15;
UPDATE EMPLEADOS SET email = 'matias.ortiz@gmail.com' WHERE Id_Empleado = 16;
UPDATE EMPLEADOS SET email = 'martina.silva@gmail.com' WHERE Id_Empleado = 17;
UPDATE EMPLEADOS SET email = 'tomas.benitez@gmail.com' WHERE Id_Empleado = 18;
UPDATE EMPLEADOS SET email = 'daniela.silva@gmail.com' WHERE Id_Empleado = 19;
UPDATE EMPLEADOS SET email = 'bruno.alvarez@gmail.com' WHERE Id_Empleado = 20;
UPDATE EMPLEADOS SET email = 'florencia.medina@gmail.com' WHERE Id_Empleado = 21;
UPDATE EMPLEADOS SET email = 'sebastian.romero@gmail.com' WHERE Id_Empleado = 22;
UPDATE EMPLEADOS SET email = 'natalia.juarez@gmail.com' WHERE Id_Empleado = 23;
UPDATE EMPLEADOS SET email = 'ignacio.varela@gmail.com' WHERE Id_Empleado = 24;
UPDATE EMPLEADOS SET email = 'paula.medina@gmail.com' WHERE Id_Empleado = 25;
UPDATE EMPLEADOS SET email = 'franco.morales@gmail.com' WHERE Id_Empleado = 26;
UPDATE EMPLEADOS SET email = 'carla.iglesias@gmail.com' WHERE Id_Empleado = 27;
UPDATE EMPLEADOS SET email = 'gonzalo.rojas@gmail.com' WHERE Id_Empleado = 28;
UPDATE EMPLEADOS SET email = 'brenda.flores@gmail.com' WHERE Id_Empleado = 29;
UPDATE EMPLEADOS SET email = 'ezequiel.castillo@gmail.com' WHERE Id_Empleado = 30;
go

INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (1, 'CafeConEstrellas#81', 'carolina.benitez@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (2, 'MateAmargo_739!', 'juan.perez@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (3, 'LunaDeCristal#42', 'lucia.gimenez@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (4, 'DragonRojo!928', 'facundo.acosta@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (5, 'VioletasEnMarzo#7', 'valeria.rios@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (6, 'PixelPerdido_502!', 'esteban.paz@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (7, 'MariposaNocturna#63', 'mariana.herrera@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (8, 'CafeConCaos!184', 'lucas.vera@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (9, 'NubeDeAlgodon#91', 'camila.soria@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (10, 'RayoDeMedianoche_27!', 'lautaro.molina@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (11, 'DulceTormenta#508', 'sofia.navarro@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (12, 'GatoAstronauta!36', 'mateo.gomez@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (13, 'RosaDeInvierno_721', 'rocio.castro@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (14, 'TruenoAzul#194', 'joaquin.duarte@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (15, 'MielYCanela!853', 'agustina.luna@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (16, 'CometaVeloz#62', 'matias.ortiz@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (17, 'BosqueEncantado_419!', 'martina.silva@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (18, 'PizzaIntergalactica#8', 'tomas.benitez@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (19, 'EstrellaFugaz!637', 'daniela.silva@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (20, 'TiburonConSombrero#25', 'bruno.alvarez@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (21, 'JardinSecreto_908!', 'florencia.medina@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (22, 'VolcanDeChocolate#51', 'sebastian.romero@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (23, 'ZorroDeMedianoche!73', 'natalia.juarez@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (24, 'RelojDeArena_284#', 'ignacio.varela@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (25, 'CerezaExplosiva!619', 'paula.medina@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (26, 'FantasmaEnBicicleta#4', 'franco.morales@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (27, 'AuroraDeColores_752!', 'carla.iglesias@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (28, 'PulpoDetective#386', 'gonzalo.rojas@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (29, 'ChocolateCosmico!94', 'brenda.flores@gmail.com');
INSERT INTO USUARIOS (id_empleado, contrasenia, correo)
VALUES (30, 'MapacheElegante_613#', 'ezequiel.castillo@gmail.com');
go

alter table usuarios
add constraint fk_usuarios_empleados foreign key (id_empleado) references empleados (Id_empleado)
go


-- CONTINUACIÓN DE FarmaciaRegistros.sql
-- Ejecutar DESPUÉS de FarmaciaRegistros.sql (las numeraciones de Nro_Entrega, Nro_Detalle y Nro_Factura
-- continúan desde la última fila de ese script: entregas desde la 11, detalle de entregas desde el 14, facturas desde la 27).
-- Período: octubre 2026 y noviembre 2026 (hasta el 15). Cada empleado pertenece a la sucursal de la fila.

-------------------------------------------------- TABLAS DE ENTREGAS --------------------------------------------------------------

INSERT INTO ENTREGAS (Nro_sucursal, Fecha, Firma_Empresa, Resp_Entrega, Id_Empleado) VALUES
-- Sucursal 1: Nueva Córdoba (Empleados 1, 2 y 3)
(1, '20261008', 'Farmadistribuciones', 'Lorena Paz', 1),                      --11 (Sucursal 1 - Empleado 1 de Sucursal 1)
(1, '20261012', 'Droguería Central', 'Hugo Barrios', 2),                      --12 (Sucursal 1 - Empleado 2 de Sucursal 1)
(1, '20261114', 'Medicorp Argentina', 'Natalia Vega', 3),                     --13 (Sucursal 1 - Empleado 3 de Sucursal 1)

-- Sucursal 2: Alberdi (Empleados 4, 5 y 6)
(2, '20261004', 'Distribuidora Médica', 'Ariel Núñez', 4),                    --14 (Sucursal 2 - Empleado 4 de Sucursal 2)
(2, '20261011', 'Farmadistribuciones', 'Lorena Paz', 5),                      --15 (Sucursal 2 - Empleado 5 de Sucursal 2)
(2, '20261105', 'Droguería Central', 'Hugo Barrios', 6),                      --16 (Sucursal 2 - Empleado 6 de Sucursal 2)

-- Sucursal 3: Cerro (Empleados 7, 8 y 9)
(3, '20261006', 'Droguería del Sur', 'Pablo Ledesma', 7),                     --17 (Sucursal 3 - Empleado 7 de Sucursal 3)
(3, '20261013', 'Distribuidora Médica', 'Ariel Núñez', 8),                    --18 (Sucursal 3 - Empleado 8 de Sucursal 3)
(3, '20261109', 'Farmadistribuciones', 'Lorena Paz', 9),                      --19 (Sucursal 3 - Empleado 9 de Sucursal 3)

-- Sucursal 4: Carlos Paz (Empleados 10, 11 y 12)
(4, '20261013', 'Medicorp Argentina', 'Natalia Vega', 10),                    --20 (Sucursal 4 - Empleado 10 de Sucursal 4)
(4, '20261026', 'Droguería del Sur', 'Pablo Ledesma', 11),                    --21 (Sucursal 4 - Empleado 11 de Sucursal 4)
(4, '20261114', 'Distribuidora Médica', 'Ariel Núñez', 12),                   --22 (Sucursal 4 - Empleado 12 de Sucursal 4)

-- Sucursal 5: Río Cuarto (Empleados 13, 14 y 15)
(5, '20261005', 'Droguería Central', 'Hugo Barrios', 13),                     --23 (Sucursal 5 - Empleado 13 de Sucursal 5)
(5, '20261029', 'Medicorp Argentina', 'Natalia Vega', 14),                    --24 (Sucursal 5 - Empleado 14 de Sucursal 5)
(5, '20261112', 'Droguería del Sur', 'Pablo Ledesma', 15),                    --25 (Sucursal 5 - Empleado 15 de Sucursal 5)

-- Sucursal 6: La Plata (Empleados 16, 17 y 18)
(6, '20261015', 'Farmadistribuciones', 'Lorena Paz', 16),                     --26 (Sucursal 6 - Empleado 16 de Sucursal 6)
(6, '20261019', 'Droguería Central', 'Hugo Barrios', 17),                     --27 (Sucursal 6 - Empleado 17 de Sucursal 6)
(6, '20261102', 'Medicorp Argentina', 'Natalia Vega', 18),                    --28 (Sucursal 6 - Empleado 18 de Sucursal 6)

-- Sucursal 7: Mar del Plata (Empleados 19, 20 y 21)
(7, '20261016', 'Distribuidora Médica', 'Ariel Núñez', 19),                   --29 (Sucursal 7 - Empleado 19 de Sucursal 7)
(7, '20261026', 'Farmadistribuciones', 'Lorena Paz', 20),                     --30 (Sucursal 7 - Empleado 20 de Sucursal 7)
(7, '20261106', 'Droguería Central', 'Hugo Barrios', 21),                     --31 (Sucursal 7 - Empleado 21 de Sucursal 7)

-- Sucursal 8: Rosario (Empleados 22, 23 y 24)
(8, '20261002', 'Droguería del Sur', 'Pablo Ledesma', 22),                    --32 (Sucursal 8 - Empleado 22 de Sucursal 8)
(8, '20261007', 'Distribuidora Médica', 'Ariel Núñez', 23),                   --33 (Sucursal 8 - Empleado 23 de Sucursal 8)
(8, '20261115', 'Farmadistribuciones', 'Lorena Paz', 24),                     --34 (Sucursal 8 - Empleado 24 de Sucursal 8)

-- Sucursal 9: Mendoza (Empleados 25, 26 y 27)
(9, '20261003', 'Medicorp Argentina', 'Natalia Vega', 25),                    --35 (Sucursal 9 - Empleado 25 de Sucursal 9)
(9, '20261016', 'Droguería del Sur', 'Pablo Ledesma', 26),                    --36 (Sucursal 9 - Empleado 26 de Sucursal 9)
(9, '20261106', 'Distribuidora Médica', 'Ariel Núñez', 27),                   --37 (Sucursal 9 - Empleado 27 de Sucursal 9)

-- Sucursal 10: Salta (Empleados 28, 29 y 30)
(10, '20261011', 'Droguería Central', 'Hugo Barrios', 28),                    --38 (Sucursal 10 - Empleado 28 de Sucursal 10)
(10, '20261012', 'Medicorp Argentina', 'Natalia Vega', 29),                   --39 (Sucursal 10 - Empleado 29 de Sucursal 10)
(10, '20261107', 'Droguería del Sur', 'Pablo Ledesma', 30);                   --40 (Sucursal 10 - Empleado 30 de Sucursal 10)

INSERT INTO DETALLE_ENTREGAS (Nro_Entrega, Cod_Medicamento, Cod_Producto, Cant) VALUES
-- Entrega 11 (Sucursal 1 - Nueva Córdoba)
(11, 11, NULL, 65),           --14 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)
(11, 10, NULL, 40),           --15 (Detalle de Medicamento: Salbutamol 100 mcg)
(11, NULL, 2, 15),            --16 (Detalle de Producto: Barbijo descartable x50)

-- Entrega 12 (Sucursal 1 - Nueva Córdoba)
(12, 6, NULL, 25),            --17 (Detalle de Medicamento: Metformina 850 mg)

-- Entrega 13 (Sucursal 1 - Nueva Córdoba)
(13, 11, NULL, 65),           --18 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)

-- Entrega 14 (Sucursal 2 - Alberdi)
(14, 10, NULL, 80),           --19 (Detalle de Medicamento: Salbutamol 100 mcg)
(14, 11, NULL, 30),           --20 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)
(14, 2, NULL, 60),            --21 (Detalle de Medicamento: Paracetamol 500 mg)

-- Entrega 15 (Sucursal 2 - Alberdi)
(15, 13, NULL, 75),           --22 (Detalle de Medicamento: Paracetamol c/ Antihistamínico)
(15, 9, NULL, 55),            --23 (Detalle de Medicamento: Diclofenac 75 mg)

-- Entrega 16 (Sucursal 2 - Alberdi)
(16, 1, NULL, 55),            --24 (Detalle de Medicamento: Ibuprofeno 400 mg)
(16, 12, NULL, 65),           --25 (Detalle de Medicamento: Migral / Compositum)

-- Entrega 17 (Sucursal 3 - Cerro)
(17, 4, NULL, 25),            --26 (Detalle de Medicamento: Losartán 50 mg)
(17, NULL, 2, 50),            --27 (Detalle de Producto: Barbijo descartable x50)

-- Entrega 18 (Sucursal 3 - Cerro)
(18, 1, NULL, 55),            --28 (Detalle de Medicamento: Ibuprofeno 400 mg)

-- Entrega 19 (Sucursal 3 - Cerro)
(19, 11, NULL, 40),           --29 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)
(19, NULL, 3, 15),            --30 (Detalle de Producto: Termómetro digital)

-- Entrega 20 (Sucursal 4 - Carlos Paz)
(20, 8, NULL, 45),            --31 (Detalle de Medicamento: Atorvastatina 20 mg)
(20, 12, NULL, 70),           --32 (Detalle de Medicamento: Migral / Compositum)
(20, 13, NULL, 60),           --33 (Detalle de Medicamento: Paracetamol c/ Antihistamínico)

-- Entrega 21 (Sucursal 4 - Carlos Paz)
(21, 10, NULL, 80),           --34 (Detalle de Medicamento: Salbutamol 100 mcg)

-- Entrega 22 (Sucursal 4 - Carlos Paz)
(22, 6, NULL, 40),            --35 (Detalle de Medicamento: Metformina 850 mg)

-- Entrega 23 (Sucursal 5 - Río Cuarto)
(23, 1, NULL, 40),            --36 (Detalle de Medicamento: Ibuprofeno 400 mg)
(23, 10, NULL, 70),           --37 (Detalle de Medicamento: Salbutamol 100 mcg)
(23, 4, NULL, 45),            --38 (Detalle de Medicamento: Losartán 50 mg)

-- Entrega 24 (Sucursal 5 - Río Cuarto)
(24, 5, NULL, 40),            --39 (Detalle de Medicamento: Omeprazol 20 mg)

-- Entrega 25 (Sucursal 5 - Río Cuarto)
(25, 7, NULL, 30),            --40 (Detalle de Medicamento: Loratadina 10 mg)
(25, NULL, 10, 45),           --41 (Detalle de Producto: Shampoo anticaspa)

-- Entrega 26 (Sucursal 6 - La Plata)
(26, 11, NULL, 25),           --42 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)
(26, NULL, 8, 60),            --43 (Detalle de Producto: Gasas estériles x10)

-- Entrega 27 (Sucursal 6 - La Plata)
(27, 9, NULL, 70),            --44 (Detalle de Medicamento: Diclofenac 75 mg)
(27, NULL, 7, 10),            --45 (Detalle de Producto: Crema hidratante)
(27, 6, NULL, 30),            --46 (Detalle de Medicamento: Metformina 850 mg)

-- Entrega 28 (Sucursal 6 - La Plata)
(28, 7, NULL, 55),            --47 (Detalle de Medicamento: Loratadina 10 mg)
(28, NULL, 5, 60),            --48 (Detalle de Producto: Protector solar FPS 50)

-- Entrega 29 (Sucursal 7 - Mar del Plata)
(29, 3, NULL, 65),            --49 (Detalle de Medicamento: Amoxicilina 500 mg)

-- Entrega 30 (Sucursal 7 - Mar del Plata)
(30, 13, NULL, 25),           --50 (Detalle de Medicamento: Paracetamol c/ Antihistamínico)

-- Entrega 31 (Sucursal 7 - Mar del Plata)
(31, 11, NULL, 65),           --51 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)
(31, NULL, 8, 30),            --52 (Detalle de Producto: Gasas estériles x10)

-- Entrega 32 (Sucursal 8 - Rosario)
(32, 13, NULL, 65),           --53 (Detalle de Medicamento: Paracetamol c/ Antihistamínico)
(32, 11, NULL, 70),           --54 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)

-- Entrega 33 (Sucursal 8 - Rosario)
(33, 1, NULL, 40),            --55 (Detalle de Medicamento: Ibuprofeno 400 mg)
(33, 6, NULL, 55),            --56 (Detalle de Medicamento: Metformina 850 mg)

-- Entrega 34 (Sucursal 8 - Rosario)
(34, 5, NULL, 50),            --57 (Detalle de Medicamento: Omeprazol 20 mg)
(34, 1, NULL, 20),            --58 (Detalle de Medicamento: Ibuprofeno 400 mg)
(34, 11, NULL, 30),           --59 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)

-- Entrega 35 (Sucursal 9 - Mendoza)
(35, 9, NULL, 50),            --60 (Detalle de Medicamento: Diclofenac 75 mg)
(35, 8, NULL, 80),            --61 (Detalle de Medicamento: Atorvastatina 20 mg)

-- Entrega 36 (Sucursal 9 - Mendoza)
(36, 8, NULL, 80),            --62 (Detalle de Medicamento: Atorvastatina 20 mg)
(36, 11, NULL, 30),           --63 (Detalle de Medicamento: Ibupirac Plus / Melox / Relajante)
(36, NULL, 5, 30),            --64 (Detalle de Producto: Protector solar FPS 50)

-- Entrega 37 (Sucursal 9 - Mendoza)
(37, 6, NULL, 45),            --65 (Detalle de Medicamento: Metformina 850 mg)
(37, NULL, 6, 20),            --66 (Detalle de Producto: Cepillo dental)
(37, 2, NULL, 80),            --67 (Detalle de Medicamento: Paracetamol 500 mg)

-- Entrega 38 (Sucursal 10 - Salta)
(38, 2, NULL, 80),            --68 (Detalle de Medicamento: Paracetamol 500 mg)
(38, NULL, 1, 55),            --69 (Detalle de Producto: Alcohol en gel 250 ml)

-- Entrega 39 (Sucursal 10 - Salta)
(39, 3, NULL, 45),            --70 (Detalle de Medicamento: Amoxicilina 500 mg)
(39, NULL, 8, 45),            --71 (Detalle de Producto: Gasas estériles x10)
(39, NULL, 6, 25),            --72 (Detalle de Producto: Cepillo dental)

-- Entrega 40 (Sucursal 10 - Salta)
(40, 1, NULL, 70),            --73 (Detalle de Medicamento: Ibuprofeno 400 mg)
(40, NULL, 8, 20),            --74 (Detalle de Producto: Gasas estériles x10)
(40, NULL, 4, 15);            --75 (Detalle de Producto: Jabón neutro)

-------------------------------------------------- TABLAS DE FACTURAS --------------------------------------------------------------

INSERT INTO FACTURAS (Nro_sucursal, Fecha, Id_Empleado, Id_Cliente, id_forma_pago) VALUES
-- Sucursal 1: Nueva Córdoba (Empleados 1, 2 y 3)
-- --- OCTUBRE 2026 ---
(1, '20261003 17:45:00', 1, 7, 1),                     -- 27
(1, '20261007 10:20:00', 2, 7, 7),                     -- 28
(1, '20261009 19:40:00', 3, 6, 5),                     -- 29
(1, '20261011 20:30:00', 1, 4, 3),                     -- 30
(1, '20261013 19:40:00', 2, 9, 1),                     -- 31
(1, '20261014 12:45:00', 3, 9, 2),                     -- 32
(1, '20261014 19:10:00', 1, 5, 2),                     -- 33
(1, '20261015 09:45:00', 2, 4, 4),                     -- 34
(1, '20261016 10:00:00', 3, 6, 1),                     -- 35
(1, '20261020 09:00:00', 1, 3, 10),                    -- 36
(1, '20261021 17:00:00', 2, 2, 2),                     -- 37
(1, '20261022 20:00:00', 3, 8, 3),                     -- 38
(1, '20261023 15:50:00', 1, 4, 3),                     -- 39
(1, '20261024 10:50:00', 2, 1, 3),                     -- 40
(1, '20261024 15:50:00', 3, 2, 3),                     -- 41
(1, '20261025 16:00:00', 1, 4, 7),                     -- 42
(1, '20261030 16:30:00', 2, 5, 10),                    -- 43
-- --- NOVIEMBRE 2026 (Actual) ---
(1, '20261101 19:00:00', 3, 2, 2),                     -- 44
(1, '20261103 13:15:00', 1, 7, 2),                     -- 45
(1, '20261105 18:30:00', 2, 8, 10),                    -- 46
(1, '20261106 16:30:00', 3, 8, 3),                     -- 47
(1, '20261107 20:00:00', 1, 9, 4),                     -- 48
(1, '20261109 10:40:00', 2, 7, 2),                     -- 49
(1, '20261111 15:30:00', 3, 7, 4),                     -- 50
(1, '20261111 16:50:00', 1, 6, 1),                     -- 51
(1, '20261112 10:30:00', 2, 8, 5),                     -- 52
(1, '20261112 10:45:00', 3, 1, 10),                    -- 53
(1, '20261112 18:00:00', 1, 8, 2),                     -- 54
(1, '20261113 19:40:00', 2, 2, 7),                     -- 55
(1, '20261114 17:40:00', 3, 1, 1),                     -- 56

-- Sucursal 2: Alberdi (Empleados 4, 5 y 6)
-- --- OCTUBRE 2026 ---
(2, '20261001 14:10:00', 4, 1, 5),                     -- 57
(2, '20261012 11:00:00', 5, 1, 7),                     -- 58
(2, '20261013 09:00:00', 6, 4, 3),                     -- 59
(2, '20261024 15:10:00', 4, 6, 3),                     -- 60
(2, '20261027 10:30:00', 5, 9, 10),                    -- 61
-- --- NOVIEMBRE 2026 (Actual) ---
(2, '20261107 19:15:00', 6, 3, 2),                     -- 62
(2, '20261107 19:30:00', 4, 3, 7),                     -- 63
(2, '20261108 12:00:00', 5, 4, 3),                     -- 64
(2, '20261110 14:30:00', 6, 3, 3),                     -- 65
(2, '20261115 11:30:00', 4, 3, 1),                     -- 66

-- Sucursal 3: Cerro (Empleados 7, 8 y 9)
-- --- OCTUBRE 2026 ---
(3, '20261001 17:00:00', 8, 9, 4),                     -- 67
(3, '20261004 18:45:00', 9, 8, 2),                     -- 68
(3, '20261008 12:00:00', 7, 9, 2),                     -- 69
(3, '20261017 10:20:00', 8, 6, 1),                     -- 70
(3, '20261024 10:10:00', 9, 6, 2),                     -- 71
-- --- NOVIEMBRE 2026 (Actual) ---
(3, '20261105 14:30:00', 7, 9, 2),                     -- 72
(3, '20261109 17:10:00', 8, 1, 2),                     -- 73
(3, '20261110 17:50:00', 9, 3, 4),                     -- 74
(3, '20261111 13:40:00', 7, 5, 1),                     -- 75
(3, '20261115 11:10:00', 8, 9, 5),                     -- 76

-- Sucursal 4: Carlos Paz (Empleados 10, 11 y 12)
-- --- OCTUBRE 2026 ---
(4, '20261013 19:45:00', 12, 11, 10),                  -- 77
(4, '20261018 16:10:00', 10, 10, 1),                   -- 78
(4, '20261022 14:40:00', 11, 11, 4),                   -- 79
(4, '20261029 19:15:00', 12, 10, 1),                   -- 80
(4, '20261031 13:20:00', 10, 10, 2),                   -- 81
-- --- NOVIEMBRE 2026 (Actual) ---
(4, '20261106 20:40:00', 11, 11, 1),                   -- 82
(4, '20261107 17:20:00', 12, 10, 4),                   -- 83
(4, '20261109 10:15:00', 10, 11, 1),                   -- 84
(4, '20261112 14:45:00', 11, 11, 10),                  -- 85
(4, '20261112 18:30:00', 12, 12, 1),                   -- 86

-- Sucursal 5: Río Cuarto (Empleados 13, 14 y 15)
-- --- OCTUBRE 2026 ---
(5, '20261003 12:30:00', 13, 13, 1),                   -- 87
(5, '20261004 13:50:00', 14, 15, 7),                   -- 88
(5, '20261009 15:30:00', 15, 14, 10),                  -- 89
(5, '20261011 15:50:00', 13, 15, 7),                   -- 90
(5, '20261029 18:30:00', 14, 15, 3),                   -- 91
-- --- NOVIEMBRE 2026 (Actual) ---
(5, '20261109 16:15:00', 15, 15, 5),                   -- 92
(5, '20261110 12:20:00', 13, 13, 2),                   -- 93
(5, '20261110 12:30:00', 14, 14, 2),                   -- 94
(5, '20261111 14:20:00', 15, 15, 1),                   -- 95
(5, '20261113 09:30:00', 13, 14, 1),                   -- 96

-- Sucursal 6: La Plata (Empleados 16, 17 y 18)
-- --- OCTUBRE 2026 ---
(6, '20261011 16:10:00', 17, 16, 2),                   -- 97
(6, '20261017 12:45:00', 18, 18, 4),                   -- 98
(6, '20261022 18:10:00', 16, 17, 1),                   -- 99
(6, '20261025 15:20:00', 17, 16, 4),                   -- 100
(6, '20261027 17:15:00', 18, 16, 5),                   -- 101
-- --- NOVIEMBRE 2026 (Actual) ---
(6, '20261101 13:45:00', 16, 17, 7),                   -- 102
(6, '20261105 17:00:00', 17, 16, 1),                   -- 103
(6, '20261106 14:50:00', 18, 16, 3),                   -- 104
(6, '20261111 20:20:00', 16, 18, 5),                   -- 105
(6, '20261112 12:00:00', 17, 18, 5),                   -- 106

-- Sucursal 7: Mar del Plata (Empleados 19, 20 y 21)
-- --- OCTUBRE 2026 ---
(7, '20261003 13:50:00', 21, 21, 2),                   -- 107
(7, '20261010 11:15:00', 19, 20, 1),                   -- 108
(7, '20261025 10:50:00', 20, 21, 5),                   -- 109
(7, '20261026 14:20:00', 21, 21, 2),                   -- 110
(7, '20261029 15:15:00', 19, 20, 10),                  -- 111
-- --- NOVIEMBRE 2026 (Actual) ---
(7, '20261104 18:45:00', 20, 19, 1),                   -- 112
(7, '20261108 18:20:00', 21, 19, 1),                   -- 113
(7, '20261111 16:10:00', 19, 21, 2),                   -- 114
(7, '20261111 17:15:00', 20, 21, 2),                   -- 115
(7, '20261112 13:40:00', 21, 20, 3),                   -- 116

-- Sucursal 8: Rosario (Empleados 22, 23 y 24)
-- --- OCTUBRE 2026 ---
(8, '20261007 15:20:00', 22, 23, 1),                   -- 117
(8, '20261013 19:10:00', 23, 23, 7),                   -- 118
(8, '20261017 15:45:00', 24, 22, 7),                   -- 119
(8, '20261018 17:50:00', 22, 22, 4),                   -- 120
(8, '20261021 11:10:00', 23, 24, 5),                   -- 121
-- --- NOVIEMBRE 2026 (Actual) ---
(8, '20261106 15:00:00', 24, 23, 7),                   -- 122
(8, '20261112 10:45:00', 22, 24, 2),                   -- 123
(8, '20261112 15:00:00', 23, 22, 10),                  -- 124
(8, '20261113 19:40:00', 24, 24, 4),                   -- 125
(8, '20261114 17:40:00', 22, 22, 1),                   -- 126

-- Sucursal 9: Mendoza (Empleados 25, 26 y 27)
-- --- OCTUBRE 2026 ---
(9, '20261016 20:45:00', 26, 27, 2),                   -- 127
(9, '20261021 12:20:00', 27, 25, 4),                   -- 128
(9, '20261023 14:40:00', 25, 25, 2),                   -- 129
(9, '20261024 13:20:00', 26, 26, 3),                   -- 130
(9, '20261024 15:00:00', 27, 27, 7),                   -- 131
-- --- NOVIEMBRE 2026 (Actual) ---
(9, '20261103 14:15:00', 25, 27, 3),                   -- 132
(9, '20261105 19:50:00', 26, 26, 2),                   -- 133
(9, '20261107 13:15:00', 27, 26, 1),                   -- 134
(9, '20261109 14:30:00', 25, 27, 7),                   -- 135
(9, '20261111 18:45:00', 26, 25, 7),                   -- 136

-- Sucursal 10: Salta (Empleados 28, 29 y 30)
-- --- OCTUBRE 2026 ---
(10, '20261018 19:40:00', 30, 30, 1),                  -- 137
(10, '20261021 11:30:00', 28, 28, 10),                 -- 138
(10, '20261023 16:45:00', 29, 28, 2),                  -- 139
(10, '20261023 18:30:00', 30, 30, 4),                  -- 140
(10, '20261029 10:20:00', 28, 28, 2),                  -- 141
-- --- NOVIEMBRE 2026 (Actual) ---
(10, '20261102 20:15:00', 29, 29, 2),                  -- 142
(10, '20261107 19:45:00', 30, 30, 4),                  -- 143
(10, '20261110 12:40:00', 28, 28, 5),                  -- 144
(10, '20261112 12:45:00', 29, 29, 1),                  -- 145
(10, '20261115 14:50:00', 30, 28, 1);                  -- 146

INSERT INTO DETALLE_FACTURAS (Nro_Factura, Cod_Medicamento, Cod_Producto, Pre_Unitario, Cantidad, Descuento_por_obra_Social) VALUES
-- Factura 27 (Sucursal 1 - Cliente 7)
(27, NULL, 9, 1200.00, 1, 0.00),                          -- Curitas x20
-- Factura 28 (Sucursal 1 - Cliente 7)
(28, 7, NULL, 2100.90, 2, 1680.72),                       -- Loratadina 10 mg (40% obra social)
(28, 8, NULL, 6800.00, 1, 0.00),                          -- Atorvastatina 20 mg
(28, NULL, 8, 1900.00, 1, 0.00),                          -- Gasas estériles x10
-- Factura 29 (Sucursal 1 - Cliente 6)
(29, 12, NULL, 2950.50, 2, 0.00),                         -- Migral / Compositum
-- Factura 30 (Sucursal 1 - Cliente 4)
(30, NULL, 6, 2100.00, 1, 0.00),                          -- Cepillo dental
(30, 1, NULL, 2450.50, 1, 980.20),                        -- Ibuprofeno 400 mg (40% obra social)
-- Factura 31 (Sucursal 1 - Cliente 9)
(31, 6, NULL, 3950.00, 3, 0.00),                          -- Metformina 850 mg
(31, 2, NULL, 1800.00, 2, 0.00),                          -- Paracetamol 500 mg
-- Factura 32 (Sucursal 1 - Cliente 9)
(32, 6, NULL, 3950.00, 1, 0.00),                          -- Metformina 850 mg
-- Factura 33 (Sucursal 1 - Cliente 5)
(33, 5, NULL, 3300.25, 2, 3300.25),                       -- Omeprazol 20 mg (50% obra social)
-- Factura 34 (Sucursal 1 - Cliente 4)
(34, 4, NULL, 4100.00, 1, 1640.00),                       -- Losartán 50 mg (40% obra social)
(34, 1, NULL, 2450.50, 3, 2940.60),                       -- Ibuprofeno 400 mg (40% obra social)
-- Factura 35 (Sucursal 1 - Cliente 6)
(35, 2, NULL, 1800.00, 1, 0.00),                          -- Paracetamol 500 mg
-- Factura 36 (Sucursal 1 - Cliente 3)
(36, 13, NULL, 2300.00, 1, 1380.00),                      -- Paracetamol c/ Antihistamínico (60% obra social)
(36, NULL, 2, 4800.00, 3, 0.00),                          -- Barbijo descartable x50
-- Factura 37 (Sucursal 1 - Cliente 2)
(37, 1, NULL, 2450.50, 2, 2450.50),                       -- Ibuprofeno 400 mg (50% obra social)
-- Factura 38 (Sucursal 1 - Cliente 8)
(38, 8, NULL, 6800.00, 2, 9520.00),                       -- Atorvastatina 20 mg (70% obra social)
(38, 12, NULL, 2950.50, 2, 0.00),                         -- Migral / Compositum
(38, 3, NULL, 5200.75, 1, 3640.52),                       -- Amoxicilina 500 mg (70% obra social)
-- Factura 39 (Sucursal 1 - Cliente 4)
(39, 1, NULL, 2450.50, 1, 980.20),                        -- Ibuprofeno 400 mg (40% obra social)
(39, 2, NULL, 1800.00, 1, 0.00),                          -- Paracetamol 500 mg
-- Factura 40 (Sucursal 1 - Cliente 1)
(40, 6, NULL, 3950.00, 2, 0.00),                          -- Metformina 850 mg
-- Factura 41 (Sucursal 1 - Cliente 2)
(41, 13, NULL, 2300.00, 2, 0.00),                         -- Paracetamol c/ Antihistamínico
(41, 11, NULL, 3850.00, 1, 0.00),                         -- Ibupirac Plus / Melox / Relajante
-- Factura 42 (Sucursal 1 - Cliente 4)
(42, 2, NULL, 1800.00, 3, 0.00),                          -- Paracetamol 500 mg
-- Factura 43 (Sucursal 1 - Cliente 5)
(43, 1, NULL, 2450.50, 1, 0.00),                          -- Ibuprofeno 400 mg
(43, NULL, 7, 6400.00, 1, 0.00),                          -- Crema hidratante
-- Factura 44 (Sucursal 1 - Cliente 2)
(44, NULL, 8, 1900.00, 2, 0.00),                          -- Gasas estériles x10
(44, 3, NULL, 5200.75, 2, 5200.75),                       -- Amoxicilina 500 mg (50% obra social)
-- Factura 45 (Sucursal 1 - Cliente 7)
(45, 8, NULL, 6800.00, 3, 0.00),                          -- Atorvastatina 20 mg
(45, 7, NULL, 2100.90, 1, 840.36),                        -- Loratadina 10 mg (40% obra social)
(45, 1, NULL, 2450.50, 1, 0.00),                          -- Ibuprofeno 400 mg
-- Factura 46 (Sucursal 1 - Cliente 8)
(46, NULL, 7, 6400.00, 1, 0.00),                          -- Crema hidratante
-- Factura 47 (Sucursal 1 - Cliente 8)
(47, NULL, 8, 1900.00, 1, 0.00),                          -- Gasas estériles x10
-- Factura 48 (Sucursal 1 - Cliente 9)
(48, NULL, 2, 4800.00, 1, 0.00),                          -- Barbijo descartable x50
(48, 8, NULL, 6800.00, 2, 0.00),                          -- Atorvastatina 20 mg
-- Factura 49 (Sucursal 1 - Cliente 7)
(49, 9, NULL, 2900.40, 2, 0.00),                          -- Diclofenac 75 mg
(49, 3, NULL, 5200.75, 3, 0.00),                          -- Amoxicilina 500 mg
-- Factura 50 (Sucursal 1 - Cliente 7)
(50, 11, NULL, 3850.00, 2, 0.00),                         -- Ibupirac Plus / Melox / Relajante
(50, 7, NULL, 2100.90, 3, 2521.08),                       -- Loratadina 10 mg (40% obra social)
-- Factura 51 (Sucursal 1 - Cliente 6)
(51, 3, NULL, 5200.75, 2, 7281.05),                       -- Amoxicilina 500 mg (70% obra social)
-- Factura 52 (Sucursal 1 - Cliente 8)
(52, 8, NULL, 6800.00, 1, 4760.00),                       -- Atorvastatina 20 mg (70% obra social)
(52, NULL, 3, 7500.00, 3, 0.00),                          -- Termómetro digital
(52, 3, NULL, 5200.75, 2, 7281.05),                       -- Amoxicilina 500 mg (70% obra social)
-- Factura 53 (Sucursal 1 - Cliente 1)
(53, 11, NULL, 3850.00, 1, 1540.00),                      -- Ibupirac Plus / Melox / Relajante (40% obra social)
(53, NULL, 9, 1200.00, 2, 0.00),                          -- Curitas x20
(53, NULL, 7, 6400.00, 1, 0.00),                          -- Crema hidratante
-- Factura 54 (Sucursal 1 - Cliente 8)
(54, 8, NULL, 6800.00, 1, 4760.00),                       -- Atorvastatina 20 mg (70% obra social)
(54, NULL, 2, 4800.00, 3, 0.00),                          -- Barbijo descartable x50
(54, 3, NULL, 5200.75, 1, 3640.52),                       -- Amoxicilina 500 mg (70% obra social)
-- Factura 55 (Sucursal 1 - Cliente 2)
(55, 3, NULL, 5200.75, 1, 2600.38),                       -- Amoxicilina 500 mg (50% obra social)
(55, 10, NULL, 4750.00, 2, 0.00),                         -- Salbutamol 100 mcg
-- Factura 56 (Sucursal 1 - Cliente 1)
(56, NULL, 3, 7500.00, 1, 0.00),                          -- Termómetro digital
-- Factura 57 (Sucursal 2 - Cliente 1)
(57, 3, NULL, 5200.75, 1, 2080.30),                       -- Amoxicilina 500 mg (40% obra social)
(57, 9, NULL, 2900.40, 3, 0.00),                          -- Diclofenac 75 mg
-- Factura 58 (Sucursal 2 - Cliente 1)
(58, 4, NULL, 4100.00, 1, 0.00),                          -- Losartán 50 mg
-- Factura 59 (Sucursal 2 - Cliente 4)
(59, NULL, 4, 1500.00, 1, 0.00),                          -- Jabón neutro
(59, 4, NULL, 4100.00, 2, 3280.00),                       -- Losartán 50 mg (40% obra social)
-- Factura 60 (Sucursal 2 - Cliente 6)
(60, 11, NULL, 3850.00, 1, 0.00),                         -- Ibupirac Plus / Melox / Relajante
(60, NULL, 8, 1900.00, 3, 0.00),                          -- Gasas estériles x10
-- Factura 61 (Sucursal 2 - Cliente 9)
(61, 1, NULL, 2450.50, 1, 0.00),                          -- Ibuprofeno 400 mg
(61, 4, NULL, 4100.00, 2, 0.00),                          -- Losartán 50 mg
(61, NULL, 5, 12900.00, 1, 0.00),                         -- Protector solar FPS 50
-- Factura 62 (Sucursal 2 - Cliente 3)
(62, NULL, 3, 7500.00, 3, 0.00),                          -- Termómetro digital
(62, 1, NULL, 2450.50, 3, 5146.05),                       -- Ibuprofeno 400 mg (70% obra social)
(62, NULL, 9, 1200.00, 1, 0.00),                          -- Curitas x20
-- Factura 63 (Sucursal 2 - Cliente 3)
(63, 1, NULL, 2450.50, 2, 3430.70),                       -- Ibuprofeno 400 mg (70% obra social)
(63, 3, NULL, 5200.75, 3, 10921.58),                      -- Amoxicilina 500 mg (70% obra social)
(63, NULL, 9, 1200.00, 3, 0.00),                          -- Curitas x20
-- Factura 64 (Sucursal 2 - Cliente 4)
(64, 12, NULL, 2950.50, 2, 0.00),                         -- Migral / Compositum
-- Factura 65 (Sucursal 2 - Cliente 3)
(65, 1, NULL, 2450.50, 2, 3430.70),                       -- Ibuprofeno 400 mg (70% obra social)
-- Factura 66 (Sucursal 2 - Cliente 3)
(66, 13, NULL, 2300.00, 2, 2760.00),                      -- Paracetamol c/ Antihistamínico (60% obra social)
-- Factura 67 (Sucursal 3 - Cliente 9)
(67, 9, NULL, 2900.40, 2, 0.00),                          -- Diclofenac 75 mg
-- Factura 68 (Sucursal 3 - Cliente 8)
(68, 3, NULL, 5200.75, 1, 3640.52),                       -- Amoxicilina 500 mg (70% obra social)
-- Factura 69 (Sucursal 3 - Cliente 9)
(69, 9, NULL, 2900.40, 2, 0.00),                          -- Diclofenac 75 mg
-- Factura 70 (Sucursal 3 - Cliente 6)
(70, 1, NULL, 2450.50, 1, 1715.35),                       -- Ibuprofeno 400 mg (70% obra social)
-- Factura 71 (Sucursal 3 - Cliente 6)
(71, 13, NULL, 2300.00, 2, 2760.00),                      -- Paracetamol c/ Antihistamínico (60% obra social)
-- Factura 72 (Sucursal 3 - Cliente 9)
(72, 6, NULL, 3950.00, 1, 0.00),                          -- Metformina 850 mg
-- Factura 73 (Sucursal 3 - Cliente 1)
(73, 1, NULL, 2450.50, 2, 1960.40),                       -- Ibuprofeno 400 mg (40% obra social)
-- Factura 74 (Sucursal 3 - Cliente 3)
(74, 13, NULL, 2300.00, 2, 2760.00),                      -- Paracetamol c/ Antihistamínico (60% obra social)
(74, NULL, 7, 6400.00, 1, 0.00),                          -- Crema hidratante
(74, 2, NULL, 1800.00, 2, 0.00),                          -- Paracetamol 500 mg
-- Factura 75 (Sucursal 3 - Cliente 5)
(75, 5, NULL, 3300.25, 2, 3300.25),                       -- Omeprazol 20 mg (50% obra social)
(75, 3, NULL, 5200.75, 3, 0.00),                          -- Amoxicilina 500 mg
-- Factura 76 (Sucursal 3 - Cliente 9)
(76, 3, NULL, 5200.75, 2, 0.00),                          -- Amoxicilina 500 mg
(76, 11, NULL, 3850.00, 1, 0.00),                         -- Ibupirac Plus / Melox / Relajante
(76, 10, NULL, 4750.00, 1, 0.00),                         -- Salbutamol 100 mcg
-- Factura 77 (Sucursal 4 - Cliente 11)
(77, 1, NULL, 2450.50, 1, 980.20),                        -- Ibuprofeno 400 mg (40% obra social)
-- Factura 78 (Sucursal 4 - Cliente 10)
(78, 10, NULL, 4750.00, 2, 6650.00),                      -- Salbutamol 100 mcg (70% obra social)
-- Factura 79 (Sucursal 4 - Cliente 11)
(79, 8, NULL, 6800.00, 1, 0.00),                          -- Atorvastatina 20 mg
(79, 3, NULL, 5200.75, 2, 4160.60),                       -- Amoxicilina 500 mg (40% obra social)
-- Factura 80 (Sucursal 4 - Cliente 10)
(80, NULL, 3, 7500.00, 1, 0.00),                          -- Termómetro digital
(80, 12, NULL, 2950.50, 1, 0.00),                         -- Migral / Compositum
-- Factura 81 (Sucursal 4 - Cliente 10)
(81, 10, NULL, 4750.00, 1, 3325.00),                      -- Salbutamol 100 mcg (70% obra social)
(81, 7, NULL, 2100.90, 1, 0.00),                          -- Loratadina 10 mg
-- Factura 82 (Sucursal 4 - Cliente 11)
(82, 1, NULL, 2450.50, 2, 1960.40),                       -- Ibuprofeno 400 mg (40% obra social)
-- Factura 83 (Sucursal 4 - Cliente 10)
(83, 11, NULL, 3850.00, 1, 0.00),                         -- Ibupirac Plus / Melox / Relajante
(83, NULL, 5, 12900.00, 1, 0.00),                         -- Protector solar FPS 50
-- Factura 84 (Sucursal 4 - Cliente 11)
(84, NULL, 4, 1500.00, 2, 0.00),                          -- Jabón neutro
-- Factura 85 (Sucursal 4 - Cliente 11)
(85, NULL, 3, 7500.00, 2, 0.00),                          -- Termómetro digital
(85, NULL, 7, 6400.00, 1, 0.00),                          -- Crema hidratante
(85, 6, NULL, 3950.00, 2, 0.00),                          -- Metformina 850 mg
-- Factura 86 (Sucursal 4 - Cliente 12)
(86, NULL, 5, 12900.00, 1, 0.00),                         -- Protector solar FPS 50
(86, 2, NULL, 1800.00, 1, 900.00),                        -- Paracetamol 500 mg (50% obra social)
(86, 1, NULL, 2450.50, 1, 1225.25),                       -- Ibuprofeno 400 mg (50% obra social)
-- Factura 87 (Sucursal 5 - Cliente 13)
(87, NULL, 6, 2100.00, 3, 0.00),                          -- Cepillo dental
(87, 13, NULL, 2300.00, 2, 2760.00),                      -- Paracetamol c/ Antihistamínico (60% obra social)
-- Factura 88 (Sucursal 5 - Cliente 15)
(88, 5, NULL, 3300.25, 3, 4950.38),                       -- Omeprazol 20 mg (50% obra social)
(88, 11, NULL, 3850.00, 3, 0.00),                         -- Ibupirac Plus / Melox / Relajante
-- Factura 89 (Sucursal 5 - Cliente 14)
(89, 7, NULL, 2100.90, 3, 0.00),                          -- Loratadina 10 mg
-- Factura 90 (Sucursal 5 - Cliente 15)
(90, 5, NULL, 3300.25, 3, 4950.38),                       -- Omeprazol 20 mg (50% obra social)
-- Factura 91 (Sucursal 5 - Cliente 15)
(91, 5, NULL, 3300.25, 2, 3300.25),                       -- Omeprazol 20 mg (50% obra social)
-- Factura 92 (Sucursal 5 - Cliente 15)
(92, 13, NULL, 2300.00, 1, 0.00),                         -- Paracetamol c/ Antihistamínico
(92, NULL, 1, 2300.00, 2, 0.00),                          -- Alcohol en gel 250 ml
(92, 5, NULL, 3300.25, 1, 1650.12),                       -- Omeprazol 20 mg (50% obra social)
-- Factura 93 (Sucursal 5 - Cliente 13)
(93, NULL, 7, 6400.00, 1, 0.00),                          -- Crema hidratante
-- Factura 94 (Sucursal 5 - Cliente 14)
(94, NULL, 1, 2300.00, 2, 0.00),                          -- Alcohol en gel 250 ml
(94, 4, NULL, 4100.00, 2, 3280.00),                       -- Losartán 50 mg (40% obra social)
-- Factura 95 (Sucursal 5 - Cliente 15)
(95, 5, NULL, 3300.25, 2, 3300.25),                       -- Omeprazol 20 mg (50% obra social)
-- Factura 96 (Sucursal 5 - Cliente 14)
(96, NULL, 1, 2300.00, 2, 0.00),                          -- Alcohol en gel 250 ml
(96, 1, NULL, 2450.50, 1, 980.20),                        -- Ibuprofeno 400 mg (40% obra social)
(96, 4, NULL, 4100.00, 3, 4920.00),                       -- Losartán 50 mg (40% obra social)
-- Factura 97 (Sucursal 6 - Cliente 16)
(97, NULL, 2, 4800.00, 1, 0.00),                          -- Barbijo descartable x50
-- Factura 98 (Sucursal 6 - Cliente 18)
(98, 3, NULL, 5200.75, 1, 3640.52),                       -- Amoxicilina 500 mg (70% obra social)
-- Factura 99 (Sucursal 6 - Cliente 17)
(99, 11, NULL, 3850.00, 1, 0.00),                         -- Ibupirac Plus / Melox / Relajante
-- Factura 100 (Sucursal 6 - Cliente 16)
(100, 6, NULL, 3950.00, 1, 3950.00),                      -- Metformina 850 mg (100% obra social)
(100, NULL, 9, 1200.00, 1, 0.00),                         -- Curitas x20
(100, 13, NULL, 2300.00, 1, 0.00),                        -- Paracetamol c/ Antihistamínico
-- Factura 101 (Sucursal 6 - Cliente 16)
(101, 5, NULL, 3300.25, 1, 0.00),                         -- Omeprazol 20 mg
(101, NULL, 5, 12900.00, 2, 0.00),                        -- Protector solar FPS 50
(101, NULL, 7, 6400.00, 1, 0.00),                         -- Crema hidratante
-- Factura 102 (Sucursal 6 - Cliente 17)
(102, 7, NULL, 2100.90, 1, 840.36),                       -- Loratadina 10 mg (40% obra social)
(102, 8, NULL, 6800.00, 2, 0.00),                         -- Atorvastatina 20 mg
-- Factura 103 (Sucursal 6 - Cliente 16)
(103, 6, NULL, 3950.00, 3, 11850.00),                     -- Metformina 850 mg (100% obra social)
(103, 7, NULL, 2100.90, 1, 0.00),                         -- Loratadina 10 mg
-- Factura 104 (Sucursal 6 - Cliente 16)
(104, NULL, 9, 1200.00, 2, 0.00),                         -- Curitas x20
-- Factura 105 (Sucursal 6 - Cliente 18)
(105, 3, NULL, 5200.75, 1, 3640.52),                      -- Amoxicilina 500 mg (70% obra social)
-- Factura 106 (Sucursal 6 - Cliente 18)
(106, 10, NULL, 4750.00, 2, 0.00),                        -- Salbutamol 100 mcg
(106, NULL, 7, 6400.00, 1, 0.00),                         -- Crema hidratante
-- Factura 107 (Sucursal 7 - Cliente 21)
(107, 11, NULL, 3850.00, 1, 1540.00),                     -- Ibupirac Plus / Melox / Relajante (40% obra social)
(107, 10, NULL, 4750.00, 2, 0.00),                        -- Salbutamol 100 mcg
(107, 4, NULL, 4100.00, 2, 0.00),                         -- Losartán 50 mg
-- Factura 108 (Sucursal 7 - Cliente 20)
(108, 10, NULL, 4750.00, 3, 9975.00),                     -- Salbutamol 100 mcg (70% obra social)
(108, 6, NULL, 3950.00, 1, 0.00),                         -- Metformina 850 mg
-- Factura 109 (Sucursal 7 - Cliente 21)
(109, 1, NULL, 2450.50, 3, 2940.60),                      -- Ibuprofeno 400 mg (40% obra social)
-- Factura 110 (Sucursal 7 - Cliente 21)
(110, 1, NULL, 2450.50, 1, 980.20),                       -- Ibuprofeno 400 mg (40% obra social)
-- Factura 111 (Sucursal 7 - Cliente 20)
(111, 13, NULL, 2300.00, 3, 0.00),                        -- Paracetamol c/ Antihistamínico
(111, NULL, 3, 7500.00, 1, 0.00),                         -- Termómetro digital
-- Factura 112 (Sucursal 7 - Cliente 19)
(112, 11, NULL, 3850.00, 1, 0.00),                        -- Ibupirac Plus / Melox / Relajante
(112, NULL, 7, 6400.00, 1, 0.00),                         -- Crema hidratante
(112, 9, NULL, 2900.40, 1, 1450.20),                      -- Diclofenac 75 mg (50% obra social)
-- Factura 113 (Sucursal 7 - Cliente 19)
(113, 1, NULL, 2450.50, 2, 0.00),                         -- Ibuprofeno 400 mg
-- Factura 114 (Sucursal 7 - Cliente 21)
(114, 3, NULL, 5200.75, 2, 4160.60),                      -- Amoxicilina 500 mg (40% obra social)
(114, 9, NULL, 2900.40, 1, 0.00),                         -- Diclofenac 75 mg
-- Factura 115 (Sucursal 7 - Cliente 21)
(115, 2, NULL, 1800.00, 1, 0.00),                         -- Paracetamol 500 mg
-- Factura 116 (Sucursal 7 - Cliente 20)
(116, 10, NULL, 4750.00, 3, 9975.00),                     -- Salbutamol 100 mcg (70% obra social)
-- Factura 117 (Sucursal 8 - Cliente 23)
(117, NULL, 10, 5600.00, 1, 0.00),                        -- Shampoo anticaspa
(117, 1, NULL, 2450.50, 1, 1715.35),                      -- Ibuprofeno 400 mg (70% obra social)
(117, 13, NULL, 2300.00, 3, 4140.00),                     -- Paracetamol c/ Antihistamínico (60% obra social)
-- Factura 118 (Sucursal 8 - Cliente 23)
(118, 9, NULL, 2900.40, 3, 0.00),                         -- Diclofenac 75 mg
-- Factura 119 (Sucursal 8 - Cliente 22)
(119, 1, NULL, 2450.50, 1, 1225.25),                      -- Ibuprofeno 400 mg (50% obra social)
(119, 6, NULL, 3950.00, 3, 0.00),                         -- Metformina 850 mg
-- Factura 120 (Sucursal 8 - Cliente 22)
(120, 1, NULL, 2450.50, 3, 3675.75),                      -- Ibuprofeno 400 mg (50% obra social)
-- Factura 121 (Sucursal 8 - Cliente 24)
(121, 4, NULL, 4100.00, 2, 3280.00),                      -- Losartán 50 mg (40% obra social)
(121, 5, NULL, 3300.25, 1, 0.00),                         -- Omeprazol 20 mg
-- Factura 122 (Sucursal 8 - Cliente 23)
(122, NULL, 1, 2300.00, 1, 0.00),                         -- Alcohol en gel 250 ml
(122, 13, NULL, 2300.00, 1, 1380.00),                     -- Paracetamol c/ Antihistamínico (60% obra social)
-- Factura 123 (Sucursal 8 - Cliente 24)
(123, NULL, 3, 7500.00, 1, 0.00),                         -- Termómetro digital
-- Factura 124 (Sucursal 8 - Cliente 22)
(124, 2, NULL, 1800.00, 2, 1800.00),                      -- Paracetamol 500 mg (50% obra social)
(124, 12, NULL, 2950.50, 1, 1475.25),                     -- Migral / Compositum (50% obra social)
-- Factura 125 (Sucursal 8 - Cliente 24)
(125, 4, NULL, 4100.00, 3, 4920.00),                      -- Losartán 50 mg (40% obra social)
(125, 10, NULL, 4750.00, 1, 0.00),                        -- Salbutamol 100 mcg
-- Factura 126 (Sucursal 8 - Cliente 22)
(126, NULL, 6, 2100.00, 2, 0.00),                         -- Cepillo dental
(126, 1, NULL, 2450.50, 3, 3675.75),                      -- Ibuprofeno 400 mg (50% obra social)
-- Factura 127 (Sucursal 9 - Cliente 27)
(127, 7, NULL, 2100.90, 2, 1680.72),                      -- Loratadina 10 mg (40% obra social)
-- Factura 128 (Sucursal 9 - Cliente 25)
(128, 10, NULL, 4750.00, 2, 0.00),                        -- Salbutamol 100 mcg
(128, 13, NULL, 2300.00, 1, 0.00),                        -- Paracetamol c/ Antihistamínico
-- Factura 129 (Sucursal 9 - Cliente 25)
(129, 5, NULL, 3300.25, 1, 1650.12),                      -- Omeprazol 20 mg (50% obra social)
(129, NULL, 8, 1900.00, 1, 0.00),                         -- Gasas estériles x10
-- Factura 130 (Sucursal 9 - Cliente 26)
(130, 6, NULL, 3950.00, 3, 11850.00),                     -- Metformina 850 mg (100% obra social)
(130, 7, NULL, 2100.90, 1, 0.00),                         -- Loratadina 10 mg
-- Factura 131 (Sucursal 9 - Cliente 27)
(131, 7, NULL, 2100.90, 1, 840.36),                       -- Loratadina 10 mg (40% obra social)
-- Factura 132 (Sucursal 9 - Cliente 27)
(132, 7, NULL, 2100.90, 1, 840.36),                       -- Loratadina 10 mg (40% obra social)
(132, 5, NULL, 3300.25, 1, 0.00),                         -- Omeprazol 20 mg
-- Factura 133 (Sucursal 9 - Cliente 26)
(133, NULL, 8, 1900.00, 2, 0.00),                         -- Gasas estériles x10
-- Factura 134 (Sucursal 9 - Cliente 26)
(134, 6, NULL, 3950.00, 2, 7900.00),                      -- Metformina 850 mg (100% obra social)
-- Factura 135 (Sucursal 9 - Cliente 27)
(135, 1, NULL, 2450.50, 1, 0.00),                         -- Ibuprofeno 400 mg
-- Factura 136 (Sucursal 9 - Cliente 25)
(136, 5, NULL, 3300.25, 1, 1650.12),                      -- Omeprazol 20 mg (50% obra social)
(136, 1, NULL, 2450.50, 2, 0.00),                         -- Ibuprofeno 400 mg
(136, NULL, 2, 4800.00, 2, 0.00),                         -- Barbijo descartable x50
-- Factura 137 (Sucursal 10 - Cliente 30)
(137, 4, NULL, 4100.00, 2, 0.00),                         -- Losartán 50 mg
-- Factura 138 (Sucursal 10 - Cliente 28)
(138, 8, NULL, 6800.00, 1, 4760.00),                      -- Atorvastatina 20 mg (70% obra social)
(138, 3, NULL, 5200.75, 3, 10921.58),                     -- Amoxicilina 500 mg (70% obra social)
(138, 1, NULL, 2450.50, 1, 0.00),                         -- Ibuprofeno 400 mg
-- Factura 139 (Sucursal 10 - Cliente 28)
(139, 8, NULL, 6800.00, 2, 9520.00),                      -- Atorvastatina 20 mg (70% obra social)
-- Factura 140 (Sucursal 10 - Cliente 30)
(140, 10, NULL, 4750.00, 1, 3325.00),                     -- Salbutamol 100 mcg (70% obra social)
(140, NULL, 8, 1900.00, 1, 0.00),                         -- Gasas estériles x10
-- Factura 141 (Sucursal 10 - Cliente 28)
(141, 11, NULL, 3850.00, 1, 0.00),                        -- Ibupirac Plus / Melox / Relajante
(141, NULL, 6, 2100.00, 3, 0.00),                         -- Cepillo dental
-- Factura 142 (Sucursal 10 - Cliente 29)
(142, 9, NULL, 2900.40, 1, 1450.20),                      -- Diclofenac 75 mg (50% obra social)
-- Factura 143 (Sucursal 10 - Cliente 30)
(143, NULL, 5, 12900.00, 1, 0.00),                        -- Protector solar FPS 50
(143, 4, NULL, 4100.00, 1, 0.00),                         -- Losartán 50 mg
(143, 10, NULL, 4750.00, 1, 3325.00),                     -- Salbutamol 100 mcg (70% obra social)
-- Factura 144 (Sucursal 10 - Cliente 28)
(144, 8, NULL, 6800.00, 1, 4760.00),                      -- Atorvastatina 20 mg (70% obra social)
(144, NULL, 8, 1900.00, 1, 0.00),                         -- Gasas estériles x10
-- Factura 145 (Sucursal 10 - Cliente 29)
(145, NULL, 1, 2300.00, 1, 0.00),                         -- Alcohol en gel 250 ml
(145, NULL, 10, 5600.00, 1, 0.00),                        -- Shampoo anticaspa
(145, 9, NULL, 2900.40, 3, 4350.60),                      -- Diclofenac 75 mg (50% obra social)
-- Factura 146 (Sucursal 10 - Cliente 28)
(146, NULL, 7, 6400.00, 3, 0.00),                         -- Crema hidratante
(146, 3, NULL, 5200.75, 1, 3640.52);                      -- Amoxicilina 500 mg (70% obra social)


-- COMPLETA EL INVENTARIO DE CADA SUCURSAL CON LOS INSUMOS QUE TODAVÍA NO TENÍA
-- Ejecutar DESPUÉS de FarmaciaRegistros.sql. Cada inventario queda con los 13 medicamentos y los 10 productos.
-- Así se puede registrar una entrega (o una venta) de cualquier insumo en cualquier sucursal.
-- Los insumos con Stock 0 solo figuran en el inventario; al recibir una entrega su stock sube.
-- La numeración de Nro_Detalle continúa desde el 51 (último detalle del script original: 50).

-------------------------------------------------- TABLAS DE INVENTARIOS -----------------------------------------------------------

INSERT INTO DETALLE_INVENTARIOS (Nro_Inventario, Cod_Medicamento, Cod_Producto, Stock) VALUES
-- Inventario 1 (Sucursal 1: Nueva Córdoba) -> se agregan 9 medicamentos y 9 productos
(1, 5, NULL, 0),            --51 (Medicamento: Omeprazol 20 mg, Stock 0)
(1, 6, NULL, 0),            --52 (Medicamento: Metformina 850 mg, Stock 0)
(1, 7, NULL, 0),            --53 (Medicamento: Loratadina 10 mg, Stock 0)
(1, 8, NULL, 35),           --54 (Medicamento: Atorvastatina 20 mg)
(1, 9, NULL, 35),           --55 (Medicamento: Diclofenac 75 mg)
(1, 10, NULL, 35),          --56 (Medicamento: Salbutamol 100 mcg)
(1, 11, NULL, 100),         --57 (Medicamento: Ibupirac Plus / Melox / Relajante)
(1, 12, NULL, 15),          --58 (Medicamento: Migral / Compositum)
(1, 13, NULL, 0),           --59 (Medicamento: Paracetamol c/ Antihistamínico, Stock 0)
(1, NULL, 2, 0),            --60 (Producto: Barbijo descartable x50, Stock 0)
(1, NULL, 3, 100),          --61 (Producto: Termómetro digital)
(1, NULL, 4, 60),           --62 (Producto: Jabón neutro)
(1, NULL, 5, 60),           --63 (Producto: Protector solar FPS 50)
(1, NULL, 6, 0),            --64 (Producto: Cepillo dental, Stock 0)
(1, NULL, 7, 95),           --65 (Producto: Crema hidratante)
(1, NULL, 8, 25),           --66 (Producto: Gasas estériles x10)
(1, NULL, 9, 100),          --67 (Producto: Curitas x20)
(1, NULL, 10, 55),          --68 (Producto: Shampoo anticaspa)

-- Inventario 2 (Sucursal 2: Alberdi) -> se agregan 10 medicamentos y 8 productos
(2, 1, NULL, 20),           --69 (Medicamento: Ibuprofeno 400 mg)
(2, 2, NULL, 90),           --70 (Medicamento: Paracetamol 500 mg)
(2, 3, NULL, 90),           --71 (Medicamento: Amoxicilina 500 mg)
(2, 4, NULL, 0),            --72 (Medicamento: Losartán 50 mg, Stock 0)
(2, 8, NULL, 30),           --73 (Medicamento: Atorvastatina 20 mg)
(2, 9, NULL, 90),           --74 (Medicamento: Diclofenac 75 mg)
(2, 10, NULL, 90),          --75 (Medicamento: Salbutamol 100 mcg)
(2, 11, NULL, 75),          --76 (Medicamento: Ibupirac Plus / Melox / Relajante)
(2, 12, NULL, 0),           --77 (Medicamento: Migral / Compositum, Stock 0)
(2, 13, NULL, 50),          --78 (Medicamento: Paracetamol c/ Antihistamínico)
(2, NULL, 1, 55),           --79 (Producto: Alcohol en gel 250 ml)
(2, NULL, 4, 70),           --80 (Producto: Jabón neutro)
(2, NULL, 5, 0),            --81 (Producto: Protector solar FPS 50, Stock 0)
(2, NULL, 6, 0),            --82 (Producto: Cepillo dental, Stock 0)
(2, NULL, 7, 0),            --83 (Producto: Crema hidratante, Stock 0)
(2, NULL, 8, 0),            --84 (Producto: Gasas estériles x10, Stock 0)
(2, NULL, 9, 70),           --85 (Producto: Curitas x20)
(2, NULL, 10, 55),          --86 (Producto: Shampoo anticaspa)

-- Inventario 3 (Sucursal 3: Cerro) -> se agregan 8 medicamentos y 10 productos
(3, 1, NULL, 0),            --87 (Medicamento: Ibuprofeno 400 mg, Stock 0)
(3, 2, NULL, 55),           --88 (Medicamento: Paracetamol 500 mg)
(3, 3, NULL, 100),          --89 (Medicamento: Amoxicilina 500 mg)
(3, 4, NULL, 0),            --90 (Medicamento: Losartán 50 mg, Stock 0)
(3, 5, NULL, 90),           --91 (Medicamento: Omeprazol 20 mg)
(3, 6, NULL, 0),            --92 (Medicamento: Metformina 850 mg, Stock 0)
(3, 7, NULL, 95),           --93 (Medicamento: Loratadina 10 mg)
(3, 13, NULL, 25),          --94 (Medicamento: Paracetamol c/ Antihistamínico)
(3, NULL, 1, 80),           --95 (Producto: Alcohol en gel 250 ml)
(3, NULL, 2, 0),            --96 (Producto: Barbijo descartable x50, Stock 0)
(3, NULL, 3, 0),            --97 (Producto: Termómetro digital, Stock 0)
(3, NULL, 4, 75),           --98 (Producto: Jabón neutro)
(3, NULL, 5, 30),           --99 (Producto: Protector solar FPS 50)
(3, NULL, 6, 25),           --100 (Producto: Cepillo dental)
(3, NULL, 7, 55),           --101 (Producto: Crema hidratante)
(3, NULL, 8, 55),           --102 (Producto: Gasas estériles x10)
(3, NULL, 9, 90),           --103 (Producto: Curitas x20)
(3, NULL, 10, 0),           --104 (Producto: Shampoo anticaspa, Stock 0)

-- Inventario 4 (Sucursal 4: Carlos Paz) -> se agregan 9 medicamentos y 9 productos
(4, 2, NULL, 0),            --105 (Medicamento: Paracetamol 500 mg, Stock 0)
(4, 4, NULL, 40),           --106 (Medicamento: Losartán 50 mg)
(4, 6, NULL, 55),           --107 (Medicamento: Metformina 850 mg)
(4, 8, NULL, 20),           --108 (Medicamento: Atorvastatina 20 mg)
(4, 9, NULL, 35),           --109 (Medicamento: Diclofenac 75 mg)
(4, 10, NULL, 0),           --110 (Medicamento: Salbutamol 100 mcg, Stock 0)
(4, 11, NULL, 15),          --111 (Medicamento: Ibupirac Plus / Melox / Relajante)
(4, 12, NULL, 80),          --112 (Medicamento: Migral / Compositum)
(4, 13, NULL, 60),          --113 (Medicamento: Paracetamol c/ Antihistamínico)
(4, NULL, 1, 0),            --114 (Producto: Alcohol en gel 250 ml, Stock 0)
(4, NULL, 2, 0),            --115 (Producto: Barbijo descartable x50, Stock 0)
(4, NULL, 3, 20),           --116 (Producto: Termómetro digital)
(4, NULL, 5, 0),            --117 (Producto: Protector solar FPS 50, Stock 0)
(4, NULL, 6, 70),           --118 (Producto: Cepillo dental)
(4, NULL, 7, 65),           --119 (Producto: Crema hidratante)
(4, NULL, 8, 0),            --120 (Producto: Gasas estériles x10, Stock 0)
(4, NULL, 9, 90),           --121 (Producto: Curitas x20)
(4, NULL, 10, 55),          --122 (Producto: Shampoo anticaspa)

-- Inventario 5 (Sucursal 5: Río Cuarto) -> se agregan 10 medicamentos y 8 productos
(5, 1, NULL, 20),           --123 (Medicamento: Ibuprofeno 400 mg)
(5, 3, NULL, 85),           --124 (Medicamento: Amoxicilina 500 mg)
(5, 5, NULL, 0),            --125 (Medicamento: Omeprazol 20 mg, Stock 0)
(5, 7, NULL, 0),            --126 (Medicamento: Loratadina 10 mg, Stock 0)
(5, 8, NULL, 0),            --127 (Medicamento: Atorvastatina 20 mg, Stock 0)
(5, 9, NULL, 0),            --128 (Medicamento: Diclofenac 75 mg, Stock 0)
(5, 10, NULL, 0),           --129 (Medicamento: Salbutamol 100 mcg, Stock 0)
(5, 11, NULL, 15),          --130 (Medicamento: Ibupirac Plus / Melox / Relajante)
(5, 12, NULL, 80),          --131 (Medicamento: Migral / Compositum)
(5, 13, NULL, 20),          --132 (Medicamento: Paracetamol c/ Antihistamínico)
(5, NULL, 1, 85),           --133 (Producto: Alcohol en gel 250 ml)
(5, NULL, 2, 40),           --134 (Producto: Barbijo descartable x50)
(5, NULL, 3, 0),            --135 (Producto: Termómetro digital, Stock 0)
(5, NULL, 4, 50),           --136 (Producto: Jabón neutro)
(5, NULL, 7, 85),           --137 (Producto: Crema hidratante)
(5, NULL, 8, 50),           --138 (Producto: Gasas estériles x10)
(5, NULL, 9, 50),           --139 (Producto: Curitas x20)
(5, NULL, 10, 55),          --140 (Producto: Shampoo anticaspa)

-- Inventario 6 (Sucursal 6: La Plata) -> se agregan 8 medicamentos y 10 productos
(6, 1, NULL, 70),           --141 (Medicamento: Ibuprofeno 400 mg)
(6, 2, NULL, 0),            --142 (Medicamento: Paracetamol 500 mg, Stock 0)
(6, 3, NULL, 50),           --143 (Medicamento: Amoxicilina 500 mg)
(6, 4, NULL, 35),           --144 (Medicamento: Losartán 50 mg)
(6, 5, NULL, 0),            --145 (Medicamento: Omeprazol 20 mg, Stock 0)
(6, 6, NULL, 0),            --146 (Medicamento: Metformina 850 mg, Stock 0)
(6, 7, NULL, 65),           --147 (Medicamento: Loratadina 10 mg)
(6, 12, NULL, 0),           --148 (Medicamento: Migral / Compositum, Stock 0)
(6, NULL, 1, 40),           --149 (Producto: Alcohol en gel 250 ml)
(6, NULL, 2, 35),           --150 (Producto: Barbijo descartable x50)
(6, NULL, 3, 35),           --151 (Producto: Termómetro digital)
(6, NULL, 4, 80),           --152 (Producto: Jabón neutro)
(6, NULL, 5, 0),            --153 (Producto: Protector solar FPS 50, Stock 0)
(6, NULL, 6, 30),           --154 (Producto: Cepillo dental)
(6, NULL, 7, 60),           --155 (Producto: Crema hidratante)
(6, NULL, 8, 100),          --156 (Producto: Gasas estériles x10)
(6, NULL, 9, 0),            --157 (Producto: Curitas x20, Stock 0)
(6, NULL, 10, 95),          --158 (Producto: Shampoo anticaspa)

-- Inventario 7 (Sucursal 7: Mar del Plata) -> se agregan 9 medicamentos y 9 productos
(7, 5, NULL, 85),           --159 (Medicamento: Omeprazol 20 mg)
(7, 6, NULL, 0),            --160 (Medicamento: Metformina 850 mg, Stock 0)
(7, 7, NULL, 25),           --161 (Medicamento: Loratadina 10 mg)
(7, 8, NULL, 55),           --162 (Medicamento: Atorvastatina 20 mg)
(7, 9, NULL, 75),           --163 (Medicamento: Diclofenac 75 mg)
(7, 10, NULL, 35),          --164 (Medicamento: Salbutamol 100 mcg)
(7, 11, NULL, 0),           --165 (Medicamento: Ibupirac Plus / Melox / Relajante, Stock 0)
(7, 12, NULL, 25),          --166 (Medicamento: Migral / Compositum)
(7, 13, NULL, 0),           --167 (Medicamento: Paracetamol c/ Antihistamínico, Stock 0)
(7, NULL, 1, 0),            --168 (Producto: Alcohol en gel 250 ml, Stock 0)
(7, NULL, 2, 35),           --169 (Producto: Barbijo descartable x50)
(7, NULL, 3, 0),            --170 (Producto: Termómetro digital, Stock 0)
(7, NULL, 4, 40),           --171 (Producto: Jabón neutro)
(7, NULL, 5, 0),            --172 (Producto: Protector solar FPS 50, Stock 0)
(7, NULL, 6, 45),           --173 (Producto: Cepillo dental)
(7, NULL, 8, 65),           --174 (Producto: Gasas estériles x10)
(7, NULL, 9, 85),           --175 (Producto: Curitas x20)
(7, NULL, 10, 80),          --176 (Producto: Shampoo anticaspa)

-- Inventario 8 (Sucursal 8: Rosario) -> se agregan 10 medicamentos y 8 productos
(8, 1, NULL, 85),           --177 (Medicamento: Ibuprofeno 400 mg)
(8, 2, NULL, 95),           --178 (Medicamento: Paracetamol 500 mg)
(8, 3, NULL, 75),           --179 (Medicamento: Amoxicilina 500 mg)
(8, 4, NULL, 100),          --180 (Medicamento: Losartán 50 mg)
(8, 8, NULL, 30),           --181 (Medicamento: Atorvastatina 20 mg)
(8, 9, NULL, 80),           --182 (Medicamento: Diclofenac 75 mg)
(8, 10, NULL, 20),          --183 (Medicamento: Salbutamol 100 mcg)
(8, 11, NULL, 85),          --184 (Medicamento: Ibupirac Plus / Melox / Relajante)
(8, 12, NULL, 55),          --185 (Medicamento: Migral / Compositum)
(8, 13, NULL, 45),          --186 (Medicamento: Paracetamol c/ Antihistamínico)
(8, NULL, 1, 0),            --187 (Producto: Alcohol en gel 250 ml, Stock 0)
(8, NULL, 2, 0),            --188 (Producto: Barbijo descartable x50, Stock 0)
(8, NULL, 3, 0),            --189 (Producto: Termómetro digital, Stock 0)
(8, NULL, 4, 0),            --190 (Producto: Jabón neutro, Stock 0)
(8, NULL, 5, 15),           --191 (Producto: Protector solar FPS 50)
(8, NULL, 6, 30),           --192 (Producto: Cepillo dental)
(8, NULL, 7, 0),            --193 (Producto: Crema hidratante, Stock 0)
(8, NULL, 10, 0),           --194 (Producto: Shampoo anticaspa, Stock 0)

-- Inventario 9 (Sucursal 9: Mendoza) -> se agregan 8 medicamentos y 10 productos
(9, 1, NULL, 85),           --195 (Medicamento: Ibuprofeno 400 mg)
(9, 2, NULL, 0),            --196 (Medicamento: Paracetamol 500 mg, Stock 0)
(9, 3, NULL, 90),           --197 (Medicamento: Amoxicilina 500 mg)
(9, 4, NULL, 0),            --198 (Medicamento: Losartán 50 mg, Stock 0)
(9, 5, NULL, 40),           --199 (Medicamento: Omeprazol 20 mg)
(9, 6, NULL, 30),           --200 (Medicamento: Metformina 850 mg)
(9, 7, NULL, 50),           --201 (Medicamento: Loratadina 10 mg)
(9, 13, NULL, 30),          --202 (Medicamento: Paracetamol c/ Antihistamínico)
(9, NULL, 1, 45),           --203 (Producto: Alcohol en gel 250 ml)
(9, NULL, 2, 0),            --204 (Producto: Barbijo descartable x50, Stock 0)
(9, NULL, 3, 40),           --205 (Producto: Termómetro digital)
(9, NULL, 4, 45),           --206 (Producto: Jabón neutro)
(9, NULL, 5, 0),            --207 (Producto: Protector solar FPS 50, Stock 0)
(9, NULL, 6, 0),            --208 (Producto: Cepillo dental, Stock 0)
(9, NULL, 7, 0),            --209 (Producto: Crema hidratante, Stock 0)
(9, NULL, 8, 15),           --210 (Producto: Gasas estériles x10)
(9, NULL, 9, 50),           --211 (Producto: Curitas x20)
(9, NULL, 10, 95),          --212 (Producto: Shampoo anticaspa)

-- Inventario 10 (Sucursal 10: Salta) -> se agregan 9 medicamentos y 9 productos
(10, 5, NULL, 75),          --213 (Medicamento: Omeprazol 20 mg)
(10, 6, NULL, 0),           --214 (Medicamento: Metformina 850 mg, Stock 0)
(10, 7, NULL, 0),           --215 (Medicamento: Loratadina 10 mg, Stock 0)
(10, 8, NULL, 95),          --216 (Medicamento: Atorvastatina 20 mg)
(10, 9, NULL, 0),           --217 (Medicamento: Diclofenac 75 mg, Stock 0)
(10, 10, NULL, 95),         --218 (Medicamento: Salbutamol 100 mcg)
(10, 11, NULL, 90),         --219 (Medicamento: Ibupirac Plus / Melox / Relajante)
(10, 12, NULL, 25),         --220 (Medicamento: Migral / Compositum)
(10, 13, NULL, 60),         --221 (Medicamento: Paracetamol c/ Antihistamínico)
(10, NULL, 1, 40),          --222 (Producto: Alcohol en gel 250 ml)
(10, NULL, 2, 85),          --223 (Producto: Barbijo descartable x50)
(10, NULL, 3, 50),          --224 (Producto: Termómetro digital)
(10, NULL, 4, 80),          --225 (Producto: Jabón neutro)
(10, NULL, 5, 0),           --226 (Producto: Protector solar FPS 50, Stock 0)
(10, NULL, 6, 0),           --227 (Producto: Cepillo dental, Stock 0)
(10, NULL, 7, 25),          --228 (Producto: Crema hidratante)
(10, NULL, 8, 80),          --229 (Producto: Gasas estériles x10)
(10, NULL, 9, 0);           --230 (Producto: Curitas x20, Stock 0)
