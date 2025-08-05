using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_TipoGasto
    {
        private CD_TipoGasto cdgasto = new CD_TipoGasto();

        public List<TipoGasto> Listar()
        {
            return cdgasto.Listar();
        }

        public int Registrar(TipoGasto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre del tipo de gasto no puede ser vacío.";
                return 0;
            }

            return cdgasto.Registrar(obj, out Mensaje);
        }

        public bool Editar(TipoGasto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_tipo_gasto == 0)
            {
                Mensaje = "El ID del tipo de gasto no es válido.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre del tipo de gasto no puede ser vacío.";
                return false;
            }

            return cdgasto.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del tipo de gasto no es válido.";
                return false;
            }

            // Validación: verifica si el tipo de gasto está asociado a algún gasto
            if (cdgasto.TipoGastoAsociadoAGasto(id))
            {
                Mensaje = "No se puede eliminar este tipo de gasto porque está asociado a uno o más gastos.";
                return false;
            }

            // Si no está asociado, procede con la eliminación
            return cdgasto.Eliminar(id, out Mensaje);
        }
    }
                
}
