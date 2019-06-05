using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    class ConexionFallidaException : AccesoADatosException
    {
        public ConexionFallidaException()
        {

        }

        public ConexionFallidaException(string mensaje)
            : base(mensaje)
        {

        }

        public ConexionFallidaException(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        {

        }
    }
}
