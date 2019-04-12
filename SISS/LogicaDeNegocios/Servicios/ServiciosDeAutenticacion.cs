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
        public enum EResultadoDeAutenticacion
        {
            Valido,
            NoValido,
        }
        public static EResultadoDeAutenticacion AutenticarCredenciales(string correoElectronico, string contraseña)
        {
            EResultadoDeAutenticacion resultadoDeAutenticacion = EResultadoDeAutenticacion.NoValido;

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
				resultadoDeAutenticacion = EResultadoDeAutenticacion.Valido;
            }
          
            return resultadoDeAutenticacion;
        }

        private static string EncriptarContraseña(string contraseña)
        {
            StringBuilder cadenaFinal = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                try
                {
                    Encoding unicode = Encoding.Unicode;
                    byte[] ContrasenaEncriptada = hash.ComputeHash(unicode.GetBytes(contraseña));
                    foreach (byte byteI in ContrasenaEncriptada)
                    {
                        cadenaFinal.Append(byteI.ToString());
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
