using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using AccesoABaseDeDatos;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocumentoDeEntregaUnicaDAO : Interfaces.IDocumentoDeEntregaUnicaDAO
	{
        public DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDDocumento)
        {
            if (IDDocumento <= 0)
            {
                throw new AccesoADatosException("Error al cargar DocumentoDeEntregaUnica Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.");
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
                tablaDeDocumentoDeEntregaUnica = AccesoADatos.EjecutarSelect("SELECT * FROM DocumentosDeEntregaUnica WHERE IDDocumento = @IDDocumento",parametroIDDocumentoDeEntregaUnica);
            }
            catch(SqlException e)   
            {
                throw new AccesoADatosException("Error al cargar DocumentoDeEntregaUnica con IDDocumento: " + IDDocumento, e);
            }
            DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica();
            try
            {
                documentoDeEntregaUnica = ConvertirDataTableADocumentoDeEntregaUnica(tablaDeDocumentoDeEntregaUnica);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir DocumentoDeEntregaUnica al en cargar DocumentoDeEntregaUnica con IDDocumento: " + IDDocumento, e);
            }
            return documentoDeEntregaUnica;
        }

        public List<DocumentoDeEntregaUnica> CargarIDsPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDs de DocumentoDeEntregaUnica Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
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
                tablaDeDocumentosDeEntregaUnica = AccesoADatos.EjecutarSelect("SELECT IDDocumento FROM DocumentosDeEntregaUnica WHERE IDAsignacion = @IDasignacion", parametroIDAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDs de DocumentoDeEntregaUnica por IDAsignacion: " + IDAsignacion, e);
            }
            List<DocumentoDeEntregaUnica> listaDeDocumentosDeEntregaUnica = new List<DocumentoDeEntregaUnica>();
            try
            {
                listaDeDocumentosDeEntregaUnica = ConvertirDataTableAListaDeDocumentosDeEntregaUnicaConSoloIDDocumento(tablaDeDocumentosDeEntregaUnica);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocumentoDeEntregaUnica en cargas IDs con IDAsignacion: " + IDAsignacion, e);
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
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO DocumentosDeEntregaUnica(FechaDeEntrega, TipoDeDocumento, Nombre, DocenteAdministrativo, IDAsignacion) VALUES(@IDDocumento, @FechaDeEntrega, @TipoDeDocumento, @Nombre, @DocenteAdministrativo, @IDAsignacion)", parametrosDocumentoDeEntregaUnica);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar DocumentoDeEntregaUnica: " + documentoDeEntregaUnica.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El DocumentoDeEntregaUnica: " + documentoDeEntregaUnica.ToString() + " no fue guardado.");
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
                documentoDeEntregaUnica.Nombre = fila["Nombre"].ToString();
                documentoDeEntregaUnica.DocenteAcademico = docenteAcademicoDAO.CargarIDPorIDDocumento((int)fila["IDDocumento"]);
                documentoDeEntregaUnica.Imagen = imagenDAO.CargarImagenPorIDDocumento((int)fila["IDDocumento"]);
            }
            return documentoDeEntregaUnica;
        }

        private SqlParameter[] InicializarParametrosDeSQL(DocumentoDeEntregaUnica documentoDeEntregaUnica, int IDAsignacion)
        {
            SqlParameter[] parametrosDeDocumentoDeEntregaUnica = new SqlParameter[6];

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
            parametrosDeDocumentoDeEntregaUnica[3].ParameterName = "@Nombre";
            parametrosDeDocumentoDeEntregaUnica[3].Value = documentoDeEntregaUnica.Nombre;
            parametrosDeDocumentoDeEntregaUnica[4].ParameterName = "@DocenteAdministrativo";
            parametrosDeDocumentoDeEntregaUnica[4].Value = documentoDeEntregaUnica.DocenteAcademico.IDPersonal;
            parametrosDeDocumentoDeEntregaUnica[5].ParameterName = "@IDAsignacion";
            parametrosDeDocumentoDeEntregaUnica[5].Value = IDAsignacion;

            return parametrosDeDocumentoDeEntregaUnica;
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}

