using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace LogicaDeNegocios.Servicios
{
    public class ServiciosDeAutenticacion
    {
        public enum ResultadoDeAutenticacion
        {
            Valido = 1,
            NoValido = 0,
        }
        public static ResultadoDeAutenticacion AutenticarCredenciales(string correo, string contrasena)
        {
            ResultadoDeAutenticacion resultadoDeAutenticacion = ResultadoDeAutenticacion.NoValido;

            IAutenticacionDAO InstanciaAutenticacion = new AutenticacionDAO();
            List<string> CorreosDeUsuario = InstanciaAutenticacion.CargarCorreoDeUsuarios();
            List<string> ContrasenasDeUsuario = null;
            for (int i=0; i < CorreosDeUsuario.Count; i++)
            {
                if (correo == CorreosDeUsuario[i])
                {
                     ContrasenasDeUsuario = InstanciaAutenticacion.CargarContraseñasPorCorreo(correo);
                }
            }

            contrasena = EncriptarContraseña(contrasena);

            if (ContrasenasDeUsuario != null)
            {
                for (int i=0; i < ContrasenasDeUsuario.Count; i++)
                {
                    if (contrasena == ContrasenasDeUsuario[i])
                    {
                        resultadoDeAutenticacion = ResultadoDeAutenticacion.Valido;
                    }
                }
            }
            

            return resultadoDeAutenticacion;
        }

        private static string EncriptarContraseña(string contrasena)
        {
            StringBuilder CadenaFinal = new StringBuilder();

            using (SHA256 MiHash = SHA256.Create())
            {
                try
                {
                    Encoding Unicode = Encoding.Unicode;
                    byte[] ContrasenaEncriptada = MiHash.ComputeHash(Unicode.GetBytes(contrasena));
                    foreach (byte byteI in ContrasenaEncriptada)
                    {
                        CadenaFinal.Append(byteI.ToString());
                    }

                }
                catch (IOException ExcepcionIO)
                {
                    Console.WriteLine("\n Excepcion: " + ExcepcionIO.StackTrace.ToString());
                }
            }

            return CadenaFinal.ToString();
        }
    }
    
}
