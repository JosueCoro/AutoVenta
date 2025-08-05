using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Vehiculo
    {
        /*CREATE TABLE vehiculo (
            id_vehiculo INT NOT NULL AUTO_INCREMENT,
            modelo VARCHAR(20) NOT NULL,
            año VARCHAR(10) NOT NULL,
            placa VARCHAR(10) NOT NULL,
            color VARCHAR(150) NOT NULL,
            estado VARCHAR(150) NOT NULL,
            fecha_ingreso DATE NOT NULL,
            precio_compra DECIMAL(30,3) NOT NULL,
            usuario_id_usuario INT NOT NULL,
            marca_id_marca INT NOT NULL,
            PRIMARY KEY (id_vehiculo)
        );*/
        public int id_vehiculo { get; set; }
        public string modelo { get; set; }
        public string año { get; set; }
        public string placa { get; set; }
        public string color { get; set; }
        public string estado { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public decimal precio_compra { get; set; }
        public int usuario_id_usuario { get; set; }
        public int marca_id_marca { get; set; }
        public Usuario _usuario { get; set; }
        public Marca _marca { get; set; }
    }
}
