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

        public int Registrar(Marca obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre de la marca no puede ser vacío.";
                return 0;
            }

            return cdmarca.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(Marca obj, int idUsuario, out string Mensaje)
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

            return cdmarca.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID de la marca no es válido.";
                return false;
            }


            return cdmarca.Eliminar(id, idUsuario, out Mensaje);
        }
    }
}
