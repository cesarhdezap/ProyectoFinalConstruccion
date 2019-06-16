using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class DocumentoDeEntregaUnicaDAO : IDocumentoDeEntregaUnicaDAO
	{
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
				throw new AccesoADatosException("Error al obetner Ultimo ID Insertado en DocumentoDeEntregaUnicaDAO", e, TipoDeErrorDeAccesoADatos.IDInvalida);
			}
			return ultimoIDInsertado;
		}
    }
}

