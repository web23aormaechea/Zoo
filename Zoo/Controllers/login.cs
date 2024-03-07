using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Zoo.Models;
using System.Data.SqlClient;
using System.Data;

namespace Zoo.Controllers
{
    public class login : Controller
    {

        static string cadena = "Data Source=(HZ397174\\SQLEXPRESS); Initial Catalog= ZOO_LOGIN; Integrated Security=true";



        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registrar(USUARIO oUsuario)
        {
            bool registrado;
            string mensaje;

            if (oUsuario.Clave == oUsuario.ConfirmarClave)
            {
                oUsuario.Clave = ConvertirSha256(oUsuario.Clave);
            }
            else
            {
                ViewData["Mnesaje"] = "Pasahitzak ez datoz bat";

                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena)) {

                SqlCommand cmd = new SqlCommand("sp_registrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mnesaje", SqlDbType.Bit).Direction = ParameterDirection.Output;
            }


            return View();
        }


        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }

  
}
