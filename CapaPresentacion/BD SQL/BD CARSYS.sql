-- Generado por Oracle SQL Developer Data Modeler 23.1.0.087.0806
--   en:        2025-10-18 22:02:38 VET
--   sitio:      SQL Server 2012
--   tipo:      SQL Server 2012
CREATE DATABASE CARSYS 

USE CARSYS
GO

CREATE SCHEMA administracion
GO

CREATE SCHEMA comercial
GO




CREATE TABLE administracion.asesor 
    (
     id_asesor INTEGER NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (150) NOT NULL , 
     apellidos VARCHAR (150) NOT NULL , 
     telefono NVARCHAR (15) NOT NULL , 
     ci VARCHAR (50) NOT NULL , 
     direccion VARCHAR (255) NOT NULL 
    )
GO

ALTER TABLE administracion.asesor ADD CONSTRAINT asesor_PK PRIMARY KEY CLUSTERED (id_asesor)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE administracion.bitacora 
    (
     id_bitacora INTEGER NOT NULL IDENTITY(1,1), 
     accion NVARCHAR (255) NOT NULL , 
     fecha DATE NOT NULL , 
     hora TIME NOT NULL , 
     id_usuario INTEGER NOT NULL 
    )
GO

ALTER TABLE administracion.bitacora ADD CONSTRAINT bitacora_PK PRIMARY KEY CLUSTERED (id_bitacora)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.cliente 
    (
     id_cliente INTEGER NOT NULL IDENTITY(1,1), 
     nombre_completo VARCHAR (150) NOT NULL , 
     ci_nit VARCHAR (150) NOT NULL , 
     telefono VARCHAR (20) NOT NULL , 
     direccion VARCHAR (150) 
    )
GO

ALTER TABLE comercial.cliente ADD CONSTRAINT cliente_PK PRIMARY KEY CLUSTERED (id_cliente)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.comision 
    (
     id_comision INTEGER NOT NULL IDENTITY(1,1), 
     monto DECIMAL (18,2) NOT NULL , 
     fecha_pago DATETIME2 , 
     fecha_generacion DATETIME2 NOT NULL , 
     estado VARCHAR (50) NOT NULL , 
     observaciones NVARCHAR (255) NOT NULL , 
     id_venta INTEGER NOT NULL , 
     id_asesor INTEGER NOT NULL 
    )
GO

ALTER TABLE comercial.comision ADD CONSTRAINT comision_PK PRIMARY KEY CLUSTERED (id_comision)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.detalle_venta 
    (
     id_detalle_venta INTEGER NOT NULL IDENTITY(1,1), 
     precio_venta DECIMAL (30,3) NOT NULL , 
     id_vehiculo INTEGER NOT NULL , 
     id_venta INTEGER NOT NULL 
    )
GO

ALTER TABLE comercial.detalle_venta ADD CONSTRAINT detalle_venta_PK PRIMARY KEY CLUSTERED (id_detalle_venta)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.gasto 
    (
     id_gasto INTEGER NOT NULL IDENTITY(1,1), 
     descripcion VARCHAR (250) NOT NULL , 
     monto DECIMAL (30,3) NOT NULL , 
     fecha DATE NOT NULL , 
     id_tipo_gasto INTEGER NOT NULL , 
     id_vehiculo INTEGER , 
     id_venta INTEGER 
    )
GO

ALTER TABLE comercial.gasto ADD CONSTRAINT gasto_PK PRIMARY KEY CLUSTERED (id_gasto)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.marca 
    (
     id_marca INTEGER NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (150) NOT NULL 
    )
GO

ALTER TABLE comercial.marca ADD CONSTRAINT marca_PK PRIMARY KEY CLUSTERED (id_marca)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.pago 
    (
     id_pago INTEGER NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (150) NOT NULL , 
     fecha DATE NOT NULL , 
     hora TIME NOT NULL , 
     id_tipo_pago INTEGER NOT NULL , 
     id_venta INTEGER NOT NULL,
	 monto DECIMAL (30,3) NOT NULL 
    )
GO

ALTER TABLE comercial.pago ADD CONSTRAINT pago_PK PRIMARY KEY CLUSTERED (id_pago)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE administracion.permiso 
    (
     id_permiso INTEGER NOT NULL IDENTITY(1,1), 
     accion VARCHAR (150) NOT NULL , 
     estado BIT NOT NULL 
    )
GO

ALTER TABLE administracion.permiso ADD CONSTRAINT permiso_PK PRIMARY KEY CLUSTERED (id_permiso)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE administracion.rol 
    (
     id_rol INTEGER NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (150) NOT NULL , 
     estado BIT NOT NULL 
    )
GO

ALTER TABLE administracion.rol ADD CONSTRAINT rol_PK PRIMARY KEY CLUSTERED (id_rol)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE administracion.rolespermisos 
    (
     id_roles_permisos INTEGER NOT NULL IDENTITY(1,1), 
     id_permiso INTEGER NOT NULL , 
     id_rol INTEGER NOT NULL 
    )
GO

ALTER TABLE administracion.rolespermisos ADD CONSTRAINT rolespermisos_PK PRIMARY KEY CLUSTERED (id_roles_permisos)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.tipo_gasto 
    (
     id_tipo_gasto INTEGER NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (150) NOT NULL 
    )
GO

ALTER TABLE comercial.tipo_gasto ADD CONSTRAINT tipo_gasto_PK PRIMARY KEY CLUSTERED (id_tipo_gasto)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.tipo_pago 
    (
     id_tipo_pago INTEGER NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (150) NOT NULL 
    )
GO

ALTER TABLE comercial.tipo_pago ADD CONSTRAINT tipo_pago_PK PRIMARY KEY CLUSTERED (id_tipo_pago)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE administracion.tipo_vehiculo 
    (
     id_tp_vehiculo INTEGER NOT NULL IDENTITY(1,1), 
     descripcion VARCHAR (20) NOT NULL 
    )
GO

ALTER TABLE administracion.tipo_vehiculo ADD CONSTRAINT tipo_vehiculo_PK PRIMARY KEY CLUSTERED (id_tp_vehiculo)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE administracion.usuario 
    (
     id_usuario INTEGER NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (150) NOT NULL , 
     apellido VARCHAR (150) NOT NULL , 
     ci VARCHAR (50) NOT NULL , 
     correo VARCHAR (150) NOT NULL , 
     contraseña VARCHAR (150) NOT NULL , 
     estado BIT NOT NULL , 
     id_rol INTEGER NOT NULL , 
     telefono NVARCHAR (15) NOT NULL 
    )
GO

ALTER TABLE administracion.usuario ADD CONSTRAINT usuario_PK PRIMARY KEY CLUSTERED (id_usuario)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.vehiculo 
    (
     id_vehiculo INTEGER NOT NULL IDENTITY(1,1), 
     modelo VARCHAR (20) NOT NULL , 
     año VARCHAR (10) NOT NULL , 
     placa VARCHAR (10) NOT NULL , 
     color VARCHAR (150) NOT NULL , 
     estado VARCHAR (150) NOT NULL , 
     fecha_ingreso DATE NOT NULL , 
     precio_compra DECIMAL (30,3) NOT NULL , 
     id_usuario INTEGER NOT NULL , 
     id_marca INTEGER NOT NULL , 
     imagen VARCHAR (250) NOT NULL , 
     id_tp_vehiculo INTEGER NOT NULL , 
     precio_venta DECIMAL (30,3) NOT NULL 
    )
GO

ALTER TABLE comercial.vehiculo ADD CONSTRAINT vehiculo_PK PRIMARY KEY CLUSTERED (id_vehiculo)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE comercial.venta 
    (
     id_venta INTEGER NOT NULL IDENTITY(1,1), 
     fecha DATE NOT NULL , 
     total DECIMAL (30,3) NOT NULL , 
     observaciones VARCHAR (250) , 
     id_cliente INTEGER NOT NULL , 
     id_usuario INTEGER NOT NULL , 
     comision DECIMAL (18,2) NOT NULL 
    )
GO

ALTER TABLE comercial.venta ADD CONSTRAINT venta_PK PRIMARY KEY CLUSTERED (id_venta)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

ALTER TABLE administracion.bitacora 
    ADD CONSTRAINT bitacora_usuario_FK FOREIGN KEY 
    ( 
     id_usuario
    ) 
    REFERENCES administracion.usuario 
    ( 
     id_usuario 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.comision 
    ADD CONSTRAINT comision_asesor_FK FOREIGN KEY 
    ( 
     id_asesor
    ) 
    REFERENCES administracion.asesor 
    ( 
     id_asesor 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.comision 
    ADD CONSTRAINT comision_venta_FK FOREIGN KEY 
    ( 
     id_venta
    ) 
    REFERENCES comercial.venta 
    ( 
     id_venta 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.detalle_venta 
    ADD CONSTRAINT detalle_venta_vehiculo_FK FOREIGN KEY 
    ( 
     id_vehiculo
    ) 
    REFERENCES comercial.vehiculo 
    ( 
     id_vehiculo 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.detalle_venta 
    ADD CONSTRAINT detalle_venta_venta_FK FOREIGN KEY 
    ( 
     id_venta
    ) 
    REFERENCES comercial.venta 
    ( 
     id_venta 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.gasto 
    ADD CONSTRAINT gasto_tipo_gasto_FK FOREIGN KEY 
    ( 
     id_tipo_gasto
    ) 
    REFERENCES comercial.tipo_gasto 
    ( 
     id_tipo_gasto 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.gasto 
    ADD CONSTRAINT gasto_vehiculo_FK FOREIGN KEY 
    ( 
     id_vehiculo
    ) 
    REFERENCES comercial.vehiculo 
    ( 
     id_vehiculo 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.gasto 
    ADD CONSTRAINT gasto_venta_FK FOREIGN KEY 
    ( 
     id_venta
    ) 
    REFERENCES comercial.venta 
    ( 
     id_venta 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.pago 
    ADD CONSTRAINT pago_tipo_pago_FK FOREIGN KEY 
    ( 
     id_tipo_pago
    ) 
    REFERENCES comercial.tipo_pago 
    ( 
     id_tipo_pago 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.pago 
    ADD CONSTRAINT pago_venta_FK FOREIGN KEY 
    ( 
     id_venta
    ) 
    REFERENCES comercial.venta 
    ( 
     id_venta 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE administracion.rolespermisos 
    ADD CONSTRAINT rolespermisos_permiso_FK FOREIGN KEY 
    ( 
     id_permiso
    ) 
    REFERENCES administracion.permiso 
    ( 
     id_permiso 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE administracion.rolespermisos 
    ADD CONSTRAINT rolespermisos_rol_FK FOREIGN KEY 
    ( 
     id_rol
    ) 
    REFERENCES administracion.rol 
    ( 
     id_rol 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE administracion.usuario 
    ADD CONSTRAINT usuario_rol_FK FOREIGN KEY 
    ( 
     id_rol
    ) 
    REFERENCES administracion.rol 
    ( 
     id_rol 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.vehiculo 
    ADD CONSTRAINT vehiculo_marca_FK FOREIGN KEY 
    ( 
     id_marca
    ) 
    REFERENCES comercial.marca 
    ( 
     id_marca 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.vehiculo 
    ADD CONSTRAINT vehiculo_tipo_vehiculo_FK FOREIGN KEY 
    ( 
     id_tp_vehiculo
    ) 
    REFERENCES administracion.tipo_vehiculo 
    ( 
     id_tp_vehiculo 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.vehiculo 
    ADD CONSTRAINT vehiculo_usuario_FK FOREIGN KEY 
    ( 
     id_usuario
    ) 
    REFERENCES administracion.usuario 
    ( 
     id_usuario 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.venta 
    ADD CONSTRAINT venta_cliente_FK FOREIGN KEY 
    ( 
     id_cliente
    ) 
    REFERENCES comercial.cliente 
    ( 
     id_cliente 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE comercial.venta 
    ADD CONSTRAINT venta_usuario_FK FOREIGN KEY 
    ( 
     id_usuario
    ) 
    REFERENCES administracion.usuario 
    ( 
     id_usuario 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO



-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            17
-- CREATE INDEX                             0
-- ALTER TABLE                             35
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE SEQUENCE                          0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
