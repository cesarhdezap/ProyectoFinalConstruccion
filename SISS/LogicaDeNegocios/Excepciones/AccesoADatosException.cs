using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class AccesoADatosException : Exception
    {
        public TipoDeErrorDeAccesoADatos TipoDeError { get; } = TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos;
        
        public AccesoADatosException()
        {

        }

        public AccesoADatosException(string mensaje)
            :base (mensaje)
        {
        }

        public AccesoADatosException(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        {
        }

        public AccesoADatosException(string mensaje, TipoDeErrorDeAccesoADatos tipoDeError)
            : base(mensaje)
        {
            this.TipoDeError = tipoDeError;
        }

        public AccesoADatosException(string mensaje, Exception excepcionInterna, TipoDeErrorDeAccesoADatos tipoDeError)
            :base (mensaje, excepcionInterna)
        {
            this.TipoDeError = tipoDeError;
        }
    }

    public enum TipoDeErrorDeAccesoADatos
    {
        ErrorDesconocidoDeAccesoABaseDeDatos,
        ConexionABaseDeDatosFallida,
        ObjetoNoExiste,
        InsercionFallidaPorLlavePrimariDuplicada,
        ErrorAlGuardarObjeto,
		ErrorAlConvertirObjeto, 
		IDInvalida
    }
}
