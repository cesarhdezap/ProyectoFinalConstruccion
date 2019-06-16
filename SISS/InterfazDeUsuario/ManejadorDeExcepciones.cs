using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocios.Excepciones;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario
{
	public static class ManejadorDeExcepciones
	{

		public static MensajeDeErrorParaMessageBox ManejarExcepcionDeAccesoADatos(AccesoADatosException e)
		{
			LogearExcepcion(e);
			MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = ObtenerMensajeDeErrorParaMessageBox(e);
			return mensajeDeErrorParaMessageBox;

		}

		private static void LogearExcepcion(AccesoADatosException e)
		{
			System.Console.WriteLine("Exepcion: " + e.Message + "StackTrace: " + e.StackTrace + System.Environment.NewLine + "Inner: " + e.InnerException.Message + e.InnerException.StackTrace);
		}

		private static MensajeDeErrorParaMessageBox ObtenerMensajeDeErrorParaMessageBox(AccesoADatosException e)
		{
			MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
			if (e.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(MATRICULA_DUPLICADA_MENSAJE, MATRICULA_DUPLICADA_TITULO);
			}
			else if (e.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(CONEXION_FALLIDA_MENSAJE, CONEXION_FALLIDA_TITULO);
			}
			else if (e.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(ERROR_GUARDAR_REGISTRO, ERROR_DESCONOCIDO_TITULO);
			}
			else if (e.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(ERROR_OBJETO_NO_EXISTE_MENSAJE, ERROR_OBJETO_NO_EXISTE_TITULO);
			}
			else if (e.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(ERROR_AL_CONVERTIR_OBJETO, ERROR_INTERNO_TITULO);
			}
			else if (e.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(ERROR_PETICION_MENSAJE, ERROR_INTERNO_TITULO);
			}
			else if (e.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(ERROR_DESCONOCIDO_MENSAJE, ERROR_DESCONOCIDO_TITULO);
			}
			else
			{
				mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox(ERROR_DESCONOCIDO_MENSAJE, ERROR_DESCONOCIDO_TITULO);
			}
			return mensajeDeErrorParaMessageBox;
		}
	}
}
