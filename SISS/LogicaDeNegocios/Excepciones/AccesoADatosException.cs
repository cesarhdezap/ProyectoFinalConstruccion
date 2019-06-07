using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class AccesoADatosException : Exception
    {
        public TipoDeError TipoDeError { get; set; } = TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos;
        
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

        public AccesoADatosException(string mensaje, TipoDeError tipoDeError)
            : base(mensaje)
        {
            this.TipoDeError = tipoDeError;
        }

        public AccesoADatosException(string mensaje, Exception excepcionInterna, TipoDeError tipoDeError)
            :base (mensaje, excepcionInterna)
        {
            this.TipoDeError = tipoDeError;
        }
    }

    public enum TipoDeError
    {
        ErrorDesconocidoDeAccesoABaseDeDatos,
        ConexionABaseDeDatosFallida,
        InsercionFallidaPorLlavePrimariDuplicada
    }
}
