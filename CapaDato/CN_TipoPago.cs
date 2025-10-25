using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato
{
    public class CN_TipoPago
    {
        private CD_TipoPago cdTipoPago = new CD_TipoPago();

        public List<TipoPago> Listar()
        {
            return cdTipoPago.Listar();
        }

        public int Registrar(TipoPago obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre del tipo de pago no puede ser vacío.";
                return 0;
            }

            return cdTipoPago.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(TipoPago obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_tipo_pago == 0)
            {
                Mensaje = "El ID del tipo de pago no es válido.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre del tipo de pago no puede ser vacío.";
                return false;
            }

            return cdTipoPago.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del tipo de pago no es válido.";
                return false;
            }

            // La verificación de dependencia con Pago ya está en el SP
            return cdTipoPago.Eliminar(id, idUsuario, out Mensaje);
        }
    }
}
