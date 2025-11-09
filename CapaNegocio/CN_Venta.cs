using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDato;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta cdVenta = new CD_Venta();
        private CD_Pago cdPago = new CD_Pago();

        public List<VentaSimple> Listar()
        {
            return cdVenta.ListarSimples();
        }
        private CD_Venta objetoCD = new CD_Venta();


        public List<VentaSimple> ReporteVenta(string fechaInicio, string fechaFin)
        {
            return cdVenta.ReporteVenta(fechaInicio, fechaFin);
        }

        public int RegistrarSoloVenta(Venta objVenta, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (objVenta.id_cliente == 0)
            {
                Mensaje = "Debe seleccionar un cliente para realizar la venta.";
                return 0;
            }

            if (objVenta.id_asesor == 0)
            {
                Mensaje = "Debe asignar un asesor a la venta para registrar la comisión.";
                return 0;
            }

            if (objVenta.id_usuario == 0)
            {
                Mensaje = "El usuario que registra la venta es obligatorio.";
                return 0;
            }

            if (objVenta.comision < 0)
            {
                Mensaje = "El monto de la comisión no puede ser negativo.";
                return 0;
            }

            if (objVenta.oDetalleVenta == null || objVenta.oDetalleVenta.Count == 0)
            {
                Mensaje = "Debe seleccionar al menos un vehículo para la venta.";
                return 0;
            }

            foreach (DetalleVenta detalle in objVenta.oDetalleVenta)
            {
                if (detalle.id_vehiculo == 0)
                {
                    Mensaje = "Un vehículo en el detalle no tiene un ID válido.";
                    return 0;
                }
                if (detalle.precio_venta <= 0)
                {
                    Mensaje = $"El precio de venta del vehículo ID {detalle.id_vehiculo} debe ser mayor a cero.";
                    return 0;
                }
            }

            return cdVenta.Registrar(objVenta, out Mensaje);
        }

        public int RegistrarVentaYPago(Venta objVenta, decimal montoPago, int idTipoPago, int idUsuarioAuditoria, out string Mensaje)
        {
            Mensaje = string.Empty;
            int idVentaGenerada = 0;

            if (idTipoPago == 0)
            {
                Mensaje = "Debe seleccionar un tipo de pago.";
                return 0;
            }
            if (montoPago <= 0)
            {
                Mensaje = "El monto del pago debe smayor a 0.";
                return 0;
            }

            idVentaGenerada = RegistrarSoloVenta(objVenta, out Mensaje);

            if (idVentaGenerada > 0)
            {

                Pago objPago = new Pago()
                {
                    id_venta = idVentaGenerada,
                    monto = montoPago,
                    id_tipo_pago = idTipoPago
                };

                int idPagoGenerado = cdPago.Registrar(objPago, idUsuarioAuditoria, out Mensaje);

                if (idPagoGenerado > 0)
                {
                    Mensaje = $"Venta N° {idVentaGenerada} y Pago N° {idPagoGenerado} registrados con éxito.";
                    return idVentaGenerada;
                }
                else
                {
                    return 0; 
                }
            }
            else
            {
                return 0;
            }
        }

    }
}