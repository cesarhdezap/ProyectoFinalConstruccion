﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ClasesDominio;
using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos <see cref="DocumentoDeEntregaUnica"/> en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos <see cref="DocumentoDeEntregaUnica"/>.
	/// </summary>
	public class DocumentoDeEntregaUnicaDAO : IDocumentoDeEntregaUnicaDAO
	{
		/// <summary>
		/// Carga un <see cref="DocumentoDeEntregaUnica"/> dado <see cref="DocumentoDeEntregaUnica.IDDocumento"/>/>.
		/// </summary>
		/// <param name="IDDocumento"><see cref="DocumentoDeEntregaUnica.IDDocumento"/> del <see cref="DocumentoDeEntregaUnica"/> a cargar.</param>
		/// <returns>El <see cref="DocumentoDeEntregaUnica"/> con <see cref="DocumentoDeEntregaUnica.IDDocumento"/> dado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
		public DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDDocumento)
        {
            if (IDDocumento <= 0)
            {
                throw new AccesoADatosException("Error al cargar DocumentoDeEntregaUnica Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }

            DataTable tablaDeDocumentoDeEntregaUnica = new DataTable();
            SqlParameter[] parametroIDDocumentoDeEntregaUnica = new SqlParameter[1];

            parametroIDDocumentoDeEntregaUnica[0] = new SqlParameter
            {
                ParameterName = "@IDDocumento",
                Value = IDDocumento
            };

            try
            {
                tablaDeDocumentoDeEntregaUnica = AccesoADatos.EjecutarSelect(QuerysDeDocumentoDeEntregaUnica.CARGAR_DOCUMENTO_DE_ENTREGA_UNICA_POR_ID, parametroIDDocumentoDeEntregaUnica);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDDocumento);
			}

			DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica();

            try
            {
                documentoDeEntregaUnica = ConvertirDataTableADocumentoDeEntregaUnica(tablaDeDocumentoDeEntregaUnica);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir DocumentoDeEntregaUnica al en cargar DocumentoDeEntregaUnica con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return documentoDeEntregaUnica;
        }

		/// <summary>
		/// Carga una lista de <see cref="DocumentoDeEntregaUnica"/> con solo <see cref="DocumentoDeEntregaUnica.IDDocumento"/> inicializado y sus demas atributos como null basado en <see cref="Asignacion.IDAsignacion"/> de la <see cref="Asignacion"/> relacionada al <see cref="DocumentoDeEntregaUnica"/>.
		/// </summary>
		/// <param name="IDAsignacion"><see cref="Asignacion.IDAsignacion"/> de la <see cref="DocumentoDeEntregaUnica"/> relacionada a <see cref="DocumentoDeEntregaUnica.IDDocumento"/> de <see cref="DocumentoDeEntregaUnica"/> a cargar.</param>
		/// <returns>Una <see cref="List{T}"/> de <see cref="DocumentoDeEntregaUnica"/> con solo <see cref="DocumentoDeEntregaUnica.IDDocumento"/></returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
		public List<DocumentoDeEntregaUnica> CargarIDsPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDs de DocumentoDeEntregaUnica Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }

            DataTable tablaDeDocumentosDeEntregaUnica = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];

            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };

            try
            {
                tablaDeDocumentosDeEntregaUnica = AccesoADatos.EjecutarSelect(QuerysDeDocumentoDeEntregaUnica.CARGAR_IDS_POR_IDASIGNACION, parametroIDAsignacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDAsignacion);
			}

			List<DocumentoDeEntregaUnica> listaDeDocumentosDeEntregaUnica = new List<DocumentoDeEntregaUnica>();

            try
            {
                listaDeDocumentosDeEntregaUnica = ConvertirDataTableAListaDeDocumentosDeEntregaUnicaConSoloIDDocumento(tablaDeDocumentosDeEntregaUnica);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocumentoDeEntregaUnica en cargas IDs con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return listaDeDocumentosDeEntregaUnica;
        }

		/// <summary>
		/// Guarda un <see cref="DocumentoDeEntregaUnica"/> en la base de datos.
		/// </summary>
		/// <param name="documentoDeEntregaUnica">El <see cref="DocumentoDeEntregaUnica"/> a guardar.</param>
		/// <param name="IDAsignacion"><see cref="Asignacion.IDAsignacion"/> de la <see cref="Asignacion"/> asociada al <see cref="DocumentoDeEntregaUnica"/>.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
		public void GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica, int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al guardar DocumentoDeEntregaUnica Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }

            SqlParameter[] parametrosDocumentoDeEntregaUnica = InicializarParametrosDeSQL(documentoDeEntregaUnica, IDAsignacion);
            int filasAfectadas = 0;

            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeDocumentoDeEntregaUnica.GUARDAR_DOCUMENTO_DE_ENTREGA_UNICA, parametrosDocumentoDeEntregaUnica);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, documentoDeEntregaUnica);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El DocumentoDeEntregaUnica: " + documentoDeEntregaUnica.ToString() + " no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
        }

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{DocumentoDeEntregaUnica}"/> de <see cref="DocumentoDeEntregaUnica"/> con solo <see cref="DocumentoDeEntregaUnica.IDDocumento"/> inicializado y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeDocumentosDeEntregaUnica">La <see cref="DataTable"/> que contiene datos del <see cref="DocumentoDeEntregaUnica"/></param>
		/// <returns>Una <see cref="List{DocumentoDeEntregaUnica}"/> de <see cref="DocumentoDeEntregaUnica"/> con solo <see cref="DocumentoDeEntregaUnica.IDDocumento"/> inicizalizado.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<DocumentoDeEntregaUnica> ConvertirDataTableAListaDeDocumentosDeEntregaUnicaConSoloIDDocumento(DataTable tablaDeDocumentosDeEntregaUnica)
        {
            ImagenDAO imagenDAO = new ImagenDAO();
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            List<DocumentoDeEntregaUnica> listaDeDocumentosDeEntregaUnica = new List<DocumentoDeEntregaUnica>();

            foreach (DataRow fila in tablaDeDocumentosDeEntregaUnica.Rows)
            {
                DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica
                {
                    IDDocumento = (int)fila["IDDocumento"],
                };

                listaDeDocumentosDeEntregaUnica.Add(documentoDeEntregaUnica);
            }

            return listaDeDocumentosDeEntregaUnica;
        }

		/// <summary>
		///  Convierte una <see cref="DataTable"/> a un <see cref="DocumentoDeEntregaUnica"/>.
		/// </summary>
		/// <param name="tablaDeDocumentoDeEntregaUnica">La <see cref="DataTable"/> que contiene datos del <see cref="DocumentoDeEntregaUnica"/>.</param>
		/// <returns>El <see cref="DocumentoDeEntregaUnica"/> en la <see cref="DataTable"/>.</returns>
		/// /// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private DocumentoDeEntregaUnica ConvertirDataTableADocumentoDeEntregaUnica(DataTable tablaDeDocumentoDeEntregaUnica)
        {
            ImagenDAO imagenDAO = new ImagenDAO();
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica();

            foreach (DataRow fila in tablaDeDocumentoDeEntregaUnica.Rows)
            {
                documentoDeEntregaUnica.IDDocumento = (int)fila["IDDocumento"];
                documentoDeEntregaUnica.FechaDeEntrega = DateTime.Parse(fila["FechaDeEntrega"].ToString());
                documentoDeEntregaUnica.TipoDeDocumento = (TipoDeDocumento)fila["TipoDeDocumento"];
                documentoDeEntregaUnica.DocenteAcademico = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal((int)fila["IDDocumento"]);
                documentoDeEntregaUnica.Imagen = imagenDAO.CargarImagenPorIDDocumentoYTipoDeDocumentoEnImagen((int)fila["IDDocumento"], TipoDeDocumentoEnImagen.DocumentoDeEntregaUnica);
            }

            return documentoDeEntregaUnica;
        }

		/// <summary>
		/// Inicializa un arreglo de <see cref="SqlParameter"/> basado en un <see cref="DocumentoDeEntregaUnica"/>.
		/// </summary>
		/// <param name="documentoDeEntregaUnica">El <see cref="DocumentoDeEntregaUnica"/> para inicializar los parametros.</param>
		/// <param name="IDAsignacion"><see cref="Asignacion.IDAsignacion"/> de la <see cref="Asignacion"/> asociada al <see cref="DocumentoDeEntregaUnica"/>.</param>
		/// <returns>Un arreglo de <see cref="SqlParameter"/> donde cada posición es uno de los atributoss del <see cref="DocumentoDeEntregaUnica"/>.</returns>
		private SqlParameter[] InicializarParametrosDeSQL(DocumentoDeEntregaUnica documentoDeEntregaUnica, int IDAsignacion)
        {
            SqlParameter[] parametrosDeDocumentoDeEntregaUnica = new SqlParameter[5];

            for (int i = 0; i < parametrosDeDocumentoDeEntregaUnica.Length; i++)
            {
                parametrosDeDocumentoDeEntregaUnica[i] = new SqlParameter();
            }

            parametrosDeDocumentoDeEntregaUnica[0].ParameterName = "@IDDocumento";
            parametrosDeDocumentoDeEntregaUnica[0].Value = documentoDeEntregaUnica.IDDocumento;
            parametrosDeDocumentoDeEntregaUnica[1].ParameterName = "@FechaDeEntrega";
            parametrosDeDocumentoDeEntregaUnica[1].Value = documentoDeEntregaUnica.FechaDeEntrega.ToString();
            parametrosDeDocumentoDeEntregaUnica[2].ParameterName = "@TipoDeDocumento";
            parametrosDeDocumentoDeEntregaUnica[2].Value = (int)documentoDeEntregaUnica.TipoDeDocumento;
            parametrosDeDocumentoDeEntregaUnica[3].ParameterName = "@DocenteAdministrativo";
            parametrosDeDocumentoDeEntregaUnica[3].Value = documentoDeEntregaUnica.DocenteAcademico.IDPersonal;
            parametrosDeDocumentoDeEntregaUnica[4].ParameterName = "@IDAsignacion";
            parametrosDeDocumentoDeEntregaUnica[4].Value = IDAsignacion;

            return parametrosDeDocumentoDeEntregaUnica;
        }

		/// <summary>
		/// Obtiene el ultimo <see cref="DocumentoDeEntregaUnica.IDDocumento"/> insertado en la tabla de <see cref="DocumentoDeEntregaUnica"/> en la base de datos.
		/// </summary>
		/// <returns>El ultimo <see cref="DocumentoDeEntregaUnica.IDDocumento"/> insertado en la tabla de <see cref="DocumentoDeEntregaUnica"/></returns>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiró una excepción. </exception>
		/// <exception cref="InvalidCastException">Tira esta excepción si la base de datos no regresa un valor entero.</exception>
		public int ObtenerUltimoIDInsertado()
		{
			int ultimoIDInsertado = 0;

			try
			{
				ultimoIDInsertado = AccesoADatos.EjecutarOperacionEscalar(QuerysDeDocumentoDeEntregaUnica.OBTENER_ULTIMO_ID_INSERTADO);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}
			catch (InvalidCastException e)
			{
				throw new AccesoADatosException("Error al obtener Ultimo ID Insertado en DocumentoDeEntregaUnicaDAO", e, TipoDeErrorDeAccesoADatos.IDInvalida);
			}

			return ultimoIDInsertado;
		}
    }
}

