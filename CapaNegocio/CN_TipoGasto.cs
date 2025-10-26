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
        private CD_TipoGasto cdTipoGasto = new CD_TipoGasto();

        public List<TipoGasto> Listar()
        {
            return cdTipoGasto.Listar();
        }

        public int Registrar(TipoGasto obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre del tipo de gasto no puede ser vacío.";
                return 0;
            }

            return cdTipoGasto.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(TipoGasto obj, int idUsuario, out string Mensaje)
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

            return cdTipoGasto.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del tipo de gasto no es válido.";
                return false;
            }

            return cdTipoGasto.Eliminar(id, idUsuario, out Mensaje);
        }
    }
                
}
