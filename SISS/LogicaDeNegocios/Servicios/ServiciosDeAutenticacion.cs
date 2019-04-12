using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            

            return resultadoDeAutenticacion;
        }
    }
    
}
