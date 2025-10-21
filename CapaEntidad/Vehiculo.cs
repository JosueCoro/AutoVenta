using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Vehiculo
    {
        /*CREATE TABLE comercial.vehiculo 
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
        GO*/
        public int id_vehiculo { get; set; }
        public string modelo { get; set; }
        public string año { get; set; }
        public string placa { get; set; }
        public string color { get; set; }
        public string estado { get; set; }
        public string imagen { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public decimal precio_compra { get; set; }
        public decimal precio_venta { get; set; }
        public int id_usuario { get; set; }
        public int id_marca { get; set; }
        public int id_tp_vehiculo { get; set; }
        public Usuario _usuario { get; set; }
        public Marca _marca { get; set; }
        public TipoVehiculo _tipovehiculo { get; set; }
    }
}
