using System;

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
            TipoDeError = tipoDeError;
        }

        public AccesoADatosException(string mensaje, Exception excepcionInterna, TipoDeErrorDeAccesoADatos tipoDeError)
            :base (mensaje, excepcionInterna)
        {
            TipoDeError = tipoDeError;
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
