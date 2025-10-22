using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDato;

namespace CapaNegocio
{
    public class CN_Login
    {
        private CD_Login objDato = new CD_Login();
        private CN_Recursos CN_Recursos = new CN_Recursos();
        public Usuario_Activo ValidarUsuario(string correo, string contrasenaPlana)
        {
            string contrasenaHasheada = CN_Recursos.EncriptarSHA256(contrasenaPlana);

            Usuario_Activo usuario = objDato.ValidarUsuario(correo, contrasenaHasheada);

            return usuario;
        }
    }
}
