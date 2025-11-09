using System;
using CapaDato;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Dashboard
    {
        private CD_Dashboard objCdResumen = new CD_Dashboard();

        public Dictionary<string, object> ObtenerResumenDashboard()
        {
            //aquí podríamos añadir lógica de cacheo o validaciones de seguridad
            //antes de devolver los resultados.

            return objCdResumen.ObtenerResumenDashboard();
        }
    }
}
