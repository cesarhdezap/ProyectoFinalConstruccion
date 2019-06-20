using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LogicaDeNegocios.Servicios
{
	/// <summary>
	/// Clase para autententicar credenciales.r
	/// Contiene los metodos para autenticar credenciales y encriptar contraseña.
	/// </summary>
	public class ServiciosDeAutenticacion
    {
        /// <summary>
        /// Valida el correo electronico y contraseña.
        /// </summary>
        /// <param name="correoElectronico">Correo del usuario en cadena de carácteres.</param>
        /// <param name="contraseña">Contraseña del usuario en cadena de carácteres.</param>
        /// <returns>Si las credenciales son validas.</returns>
        public static bool AutenticarCredenciales(string correoElectronico, string contraseña)
        {
            bool resultadoDeAutenticacion = false;
			AutenticacionDAO autenticacionDAO = new AutenticacionDAO();
            List<string> correosDeUsuario = autenticacionDAO.CargarCorreosDeUsuarios();
            string contraseñaDeUsuario = string.Empty;

            for (int i=0; i < correosDeUsuario.Count; i++)
            {
                if (correoElectronico == correosDeUsuario[i])
                {
                    contraseñaDeUsuario = autenticacionDAO.CargarContraseñaPorCorreo(correoElectronico);
                    break;
                }
            }

            contraseña = EncriptarContraseña(contraseña);

            if (contraseñaDeUsuario != string.Empty && contraseña == contraseñaDeUsuario )
            {
				resultadoDeAutenticacion = true;
            }
          
            return resultadoDeAutenticacion;
        }

        /// <summary>
        /// Encripta con hash 256 la contraseña del usuario.
        /// </summary>
        /// <param name="contraseña">Contraseña en cadena de carácteres.</param>
        /// <returns>Cadena con la contraseña en SHA256.</returns>
        public static string EncriptarContraseña(string contraseña)
        {
            StringBuilder cadenaFinal = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                byte[] ContrasenaEncriptada = hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));

                try
                {
                    for (int indice = 0; indice < ContrasenaEncriptada.Length; indice++)
                    {
                        cadenaFinal.Append(ContrasenaEncriptada[indice].ToString("x2"));
                    }

                }
                catch (IOException excepcionIO)
                {
                    Console.WriteLine("\n Excepcion: " + excepcionIO.StackTrace.ToString());
                }
            }

            return cadenaFinal.ToString();
        }
    }
    
}
