using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marca
    {
        private CD_Marca cdmarca = new CD_Marca();

        public List<Marca> Listar()
        {
            return cdmarca.Listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre de la marca no puede ser vacío.";
                return 0;
            }

            return cdmarca.Registrar(obj, out Mensaje);
        }

        public bool Editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_marca == 0)
            {
                Mensaje = "El ID de la marca no es válido.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre de la marca no puede ser vacío.";
                return false;
            }

            return cdmarca.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID de la marca no es válido.";
                return false;
            }

            // Validación agregada: verifica si la marca está asociada antes de eliminar
            if (cdmarca.MarcaAsociadaAVehiculo(id))
            {
                Mensaje = "No se puede eliminar la marca porque está asociada a uno o más vehículos.";
                return false;
            }

            // Si no está asociada, procede con la eliminación
            return cdmarca.Eliminar(id, out Mensaje);
        }
    }
}
