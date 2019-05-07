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
        public enum EresultadoDeAutenticacion
        {
            Valido = 1,
            NoValido = 0,
        }
        public static EresultadoDeAutenticacion AutenticarCredenciales(string correoElectronico, string contraseña)
        {
            EresultadoDeAutenticacion resultadoDeAutenticacion = EresultadoDeAutenticacion.NoValido;

			AutenticacionDAO autenticacionDAO = new AutenticacionDAO();

            List<string> correosDeUsuario = autenticacionDAO.CargarCorreoDeUsuarios();
            string contraseñaDeUsuario = string.Empty;
            for (int i=0; i < correosDeUsuario.Count; i++)
            {
                if (correoElectronico == correosDeUsuario[i])
                {
                     contraseñaDeUsuario = autenticacionDAO.CargarContraseñaPorCorreo(correoElectronico);
                }
            }

            contraseña = EncriptarContraseña(contraseña);

            if (contraseñaDeUsuario != null && contraseña == contraseñaDeUsuario )
            {
				resultadoDeAutenticacion = EresultadoDeAutenticacion.Valido;
            }
          
            return resultadoDeAutenticacion;
        }

        public static string EncriptarContraseña(string contraseña)
        {
            StringBuilder cadenaFinal = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                try
                {
                    byte[] ContrasenaEncriptada = hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));

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
