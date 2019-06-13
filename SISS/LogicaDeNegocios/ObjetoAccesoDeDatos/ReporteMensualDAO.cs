using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using AccesoABaseDeDatos;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos

{
	public class ReporteMensualDAO : IReporteMensualDAO
	{
		
        public List<ReporteMensual> CargarIDsPorIDAsignacion(int IDAsignacion)
		{
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDsReporteMensual Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }

            DataTable tablaDeReportesMensuales = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };

            try
            {
                tablaDeReportesMensuales = AccesoADatos.EjecutarSelect("SELECT IDDocumento FROM ReportesMensuales WHERE IDAsignacion = @IDAsignacion", parametroIDAsignacion);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar IDsReporteMensual por IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar IDsReporteMensual por IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			List<ReporteMensual> listaDeReportesMensuales = new List<ReporteMensual>();
            try
            {
                listaDeReportesMensuales = ConvertirDataTableAListaDeReportesMensualesConSoloID(tablaDeReportesMensuales);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a ListaDeReportesMensuales en cargar IDs con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return listaDeReportesMensuales;
        }

        public ReporteMensual CargarReporteMensualPorID(int IDDocumento)
		{
            if (IDDocumento <= 0)
            {
                throw new AccesoADatosException("Error al cargar ReporteMensual Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeReporteMensual = new DataTable();
            SqlParameter[] parametroIDDocumento = new SqlParameter[1];
            parametroIDDocumento[0] = new SqlParameter
            {
                ParameterName = "@IDDocumento",
                Value = IDDocumento
            };
            try
            {
                tablaDeReporteMensual = AccesoADatos.EjecutarSelect("SELECT * FROM ReportesMensuales WHERE IDDocumento = @IDDocumento", parametroIDDocumento);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar ReporteMensual con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar ReporteMensual con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			ReporteMensual reporteMensual = new ReporteMensual();
            try
            {
                reporteMensual = ConvertirDataTableAReporteMensual(tablaDeReporteMensual);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir DocumentoDeEntregaUnica al en cargar DocumentoDeEntregaUnica con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return reporteMensual;
        }

        private List<ReporteMensual> ConvertirDataTableAListaDeReportesMensualesConSoloID(DataTable tablaDeReportesMensuales)
        {
            List<ReporteMensual> listaDeReportesMensuales = new List<ReporteMensual>();
            foreach (DataRow fila in tablaDeReportesMensuales.Rows)
            {
                ReporteMensual reporteMensual = new ReporteMensual
                {
                    IDDocumento = (int)fila["IDDocumento"],
                };
                listaDeReportesMensuales.Add(reporteMensual);
            }
            return listaDeReportesMensuales;
        }

        private ReporteMensual ConvertirDataTableAReporteMensual (DataTable tablaDeReportesMensuales)
		{
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            ImagenDAO imagenDAO = new ImagenDAO();
            ReporteMensual reporteMensual = new ReporteMensual();

            foreach (DataRow fila in tablaDeReportesMensuales.Rows)
            {
                reporteMensual.IDDocumento = (int)fila["IDDocumento"];
                reporteMensual.HorasReportadas = (int)fila["HorasReportadas"];
                reporteMensual.NumeroDeReporte = (int)fila["NumeroDeReporte"];
                reporteMensual.DocenteAcademico = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal((int)fila["IDPersonal"]);
                reporteMensual.Imagen = imagenDAO.CargarImagenPorIDDocumentoYTipoDeDocumentoEnImagen((int)fila["IDDocumento"], TipoDeDocumentoEnImagen.ReporteMensual);
                reporteMensual.Mes = (Mes)fila["Mes"];
				reporteMensual.FechaDeEntrega = DateTime.Parse(fila["FechaDeEntrega"].ToString());
			}
            return reporteMensual;
		}

        public void GuardarReporteMensual(ReporteMensual reporteMensual, int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al guardar ReporteMensual Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            SqlParameter[] parametrosDocumentoDeEntregaUnica = InicializarParametrosDeSql(reporteMensual, IDAsignacion);

            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO ReportesMensuales(HorasReportadas, FechaDeEntrega, NumeroDeReporte, IDPersonal, Mes, IDAsignacion) VALUES(@HorasReportadas, @FechaDeEntrega, @NumeroDeReporte, @IDPersonal, @Mes, @IDAsignacion)", parametrosDocumentoDeEntregaUnica);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al guardar ReporteMensual: " + reporteMensual.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al guardar ReporteMensual: " + reporteMensual.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El ReporteMensual: " + reporteMensual.ToString() + " no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

        private SqlParameter[] InicializarParametrosDeSql(ReporteMensual reporteMensual, int IDAsignacion = 0)
        {
            SqlParameter[] parametrosDeReporteMensual = new SqlParameter[7];

            for (int i = 0; i < parametrosDeReporteMensual.Length; i++)
            {
                parametrosDeReporteMensual[i] = new SqlParameter();
            }

            parametrosDeReporteMensual[0].ParameterName = "@IDReporte";
            parametrosDeReporteMensual[0].Value = reporteMensual.IDDocumento;
            parametrosDeReporteMensual[1].ParameterName = "@HorasReportadas";
            parametrosDeReporteMensual[1].Value = reporteMensual.HorasReportadas;
            parametrosDeReporteMensual[2].ParameterName = "@FechaDeEntrega";
            parametrosDeReporteMensual[2].Value = reporteMensual.FechaDeEntrega.ToString();
            parametrosDeReporteMensual[3].ParameterName = "@NumeroDeReporte";
            parametrosDeReporteMensual[3].Value = reporteMensual.NumeroDeReporte;
            parametrosDeReporteMensual[4].ParameterName = "@IDPersonal";
            parametrosDeReporteMensual[4].Value = reporteMensual.DocenteAcademico.IDPersonal;
            parametrosDeReporteMensual[5].ParameterName = "@Mes";
            parametrosDeReporteMensual[5].Value = (int)reporteMensual.Mes;
            parametrosDeReporteMensual[6].ParameterName = "@IDAsignacion";
            parametrosDeReporteMensual[6].Value = IDAsignacion;

            return parametrosDeReporteMensual;
        }

        public void ActualizarReporteMensualPorID(int IDDocumento, ReporteMensual reporteMensual)
        {
			if (IDDocumento <= 0)
			{
				throw new AccesoADatosException("Error al Actualizar ReporteMensual Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
			}
			SqlParameter[] parametrosDrProyecto = InicializarParametrosDeSql(reporteMensual);
			int filasAfectadas = 0;
			try
			{
				filasAfectadas = AccesoADatos.EjecutarInsertInto("UPDATE ReportesMensuales SET HorasReportadas = @HorasReportadas WHERE IDDocumento = @IDReporte", parametrosDrProyecto);
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al actualizar Proyecto: " + reporteMensual.ToString() + "Con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al actualizar Proyecto: " + reporteMensual.ToString() + "Con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}

			if (filasAfectadas <= 0)
			{
				throw new AccesoADatosException("El Proyecto con IDProyecto: " + IDDocumento + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
			}
		}

        public int ObtenerUltimoIDInsertado()
        {
			int ultimoIDInsertado = 0;
			try
			{
				ultimoIDInsertado = AccesoADatos.EjecutarOperacionEscalar("SELECT IDENT_CURRENT('ReportesMensuales')");
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al obtener Ultimo ID Insertado en ReporteMensualDAO", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al obtener Ultimo ID Insertado en ReporteMensualDAO", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			return ultimoIDInsertado;
		}
    }
}
