using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LogicaDeNegocios.Servicios
{
	public class ServiciosDeAutenticacion
    {
        public static bool AutenticarCredenciales(string correoElectronico, string contraseña)
        {
            bool resultadoDeAutenticacion = false;
			AutenticacionDAO autenticacionDAO = new AutenticacionDAO();
            List<string> correosDeUsuario = autenticacionDAO.CargarCorreoDeUsuarios();
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
