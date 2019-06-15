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
