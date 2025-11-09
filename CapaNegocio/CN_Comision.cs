using CapaDato;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Comision
    {
        private CD_Comision objCdComision = new CD_Comision();

        public List<Comision> Listar()
        {
            return objCdComision.Listar();
        }

        public bool Pagar(int idComision, int idUsuarioAuditoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (idComision <= 0)
            {
                Mensaje = "ID de comisión no válido.";
                return false;
            }


            return objCdComision.Pagar(idComision, idUsuarioAuditoria, out Mensaje);
        }
    }
}