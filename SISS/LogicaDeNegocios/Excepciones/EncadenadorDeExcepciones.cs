using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AccesoABaseDeDatos;

namespace LogicaDeNegocios.Excepciones
{
	public static class EncadenadorDeExcepciones
	{
		public static void EncadenarExcepcionDeSql(SqlException e, object objetoAsociado)
		{
			if (e.Number == (int)CodigoDeErrorDeSqlException.ConexionAServidorFallida 
			 || e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado 
			 || e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoRespondio 
			 || e.Number == (int)CodigoDeErrorDeSqlException.TiempoDeEsperaExpirado)
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			else if (e.Number == (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada)
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString(), e, TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada);
			}
			else
			{
				throw new AccesoADatosException(e.Message, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
		}

		public static void EncadenarExcepcionDeSql(SqlException e, object objetoAsociado, int IDAsociada)
		{
			if (e.Number == (int)CodigoDeErrorDeSqlException.ConexionAServidorFallida
			 || e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado
			 || e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoRespondio
			 || e.Number == (int)CodigoDeErrorDeSqlException.TiempoDeEsperaExpirado)
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString() + System.Environment.NewLine + "IDAsociada: " + IDAsociada, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			else if (e.Number == (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada)
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString() + System.Environment.NewLine + "IDAsociada: " + IDAsociada, e, TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada);
			}
			else
			{
				throw new AccesoADatosException(e.Message, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
		}
	}
}
