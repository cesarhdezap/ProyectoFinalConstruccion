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
			if (ErrorDeConexion(e.Number))
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			else if (e.Number == (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada)
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString(), e, TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada);
			}
			else

			{
				throw new AccesoADatosException(e.Message + e.Number + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
		}

		public static void EncadenarExcepcionDeSql(SqlException e)
		{
			if (ErrorDeConexion(e.Number))
			{
				throw new AccesoADatosException(e.Message, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			else if (e.Number == (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada)
			{
				throw new AccesoADatosException(e.Message, e, TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada);
			}
			else
			{
				throw new AccesoADatosException(e.Message, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
		}

		public static void EncadenarExcepcionDeSql(SqlException e, object objetoAsociado, int IDAsociada)
		{
			if (ErrorDeConexion(e.Number))
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString() + System.Environment.NewLine + "IDAsociada: " + IDAsociada, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			else if (e.Number == (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada)
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString() + System.Environment.NewLine + "IDAsociada: " + IDAsociada, e, TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada);
			}
			else
			{
				throw new AccesoADatosException(e.Message + System.Environment.NewLine + "Objeto asociado: " + objetoAsociado.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
		}

		private static bool ErrorDeConexion(int numeroDeError)
		{
			bool esErrorDeConexion = false;

			if (numeroDeError == (int)CodigoDeErrorDeSqlException.ConexionAServidorFallida
			 || numeroDeError == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado
			 || numeroDeError == (int)CodigoDeErrorDeSqlException.ServidorNoRespondio
			 || numeroDeError == (int)CodigoDeErrorDeSqlException.TiempoDeEsperaExpirado
			 || numeroDeError == (int)CodigoDeErrorDeSqlException.ConexionRemotaFallida
			 || numeroDeError == (int)CodigoDeErrorDeSqlException.TiempoDeEsperaExpirado
			 || numeroDeError == (int)CodigoDeErrorDeSqlException.LoginFallido
             || numeroDeError == (int)CodigoDeErrorDeSqlException.ServicioSQLPausado)
			{
				esErrorDeConexion = true;
			}

			return esErrorDeConexion;
		}
	}
}
