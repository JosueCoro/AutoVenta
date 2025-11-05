using CapaDato;
using CapaEntidad;
using System;

namespace CapaNegocio
{
    public class CN_Pago
    {
        private CD_Pago cdPago = new CD_Pago();

        public int Registrar(Pago objPago, int idUsuarioAuditoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objPago.id_venta == 0)
            {
                Mensaje = "El ID de la venta es obligatorio para registrar un pago.";
                return 0;
            }
            if (objPago.monto <= 0)
            {
                Mensaje = "El monto del pago debe ser mayor a cero.";
                return 0;
            }
            if (objPago.monto > 1000000)
            {
                Mensaje = "El monto excede el límite permitido sin aprobación del gerente.";
                return 0;
            }
            if (objPago.id_tipo_pago == 0)
            {
                Mensaje = "Debe seleccionar un tipo de pago válido.";
                return 0;
            }


            return cdPago.Registrar(objPago, idUsuarioAuditoria, out Mensaje);
        }
    }
}