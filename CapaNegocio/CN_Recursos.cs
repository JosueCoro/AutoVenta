using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        //sha 256
        public string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }
        public string EncriptarSHA256(string texto)
        {
            // Crea un objeto SHA256
            SHA256 sHA256 = SHA256.Create();

            // Convierte el texto de entrada a un array de bytes
            byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(texto));

            // Convierte el array de bytes a una cadena hexadecimal (debe coincidir con el formato de la DB)
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                // Formato 'X2' asegura que se use el formato de 2 dígitos hexadecimales en mayúsculas
                stringBuilder.Append(bytes[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
