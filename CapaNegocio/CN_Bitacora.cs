using CapaEntidad;
using CapaDatos;
using System.Collections.Generic;
using System;

namespace CapaNegocio
{
    public class CN_Bitacora
    {
        private CD_Bitacora cdBitacora = new CD_Bitacora();

        public List<Bitacora> Consultar(DateTime fechaInicio, DateTime fechaFin, int idUsuario)
        {
            if (fechaInicio > fechaFin)
            {
                return new List<Bitacora>();
            }

            return cdBitacora.Consultar(fechaInicio, fechaFin, idUsuario);
        }
    }
}