using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using AccesoABaseDeDatos;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos ReporteMensual en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos ReporteMensual.
	/// </summary>
	public class ReporteMensualDAO : IReporteMensualDAO
	{

		/// <summary>
		/// Carga una lista de ReporteMensual con solo sus ID incializadas y sus demas atributos como null basado en la ID de Asignacion relacionada.
		/// </summary>
		/// <param name="IDAsignacion">La ID de la Asignacion relacionada a las ID de ReporteMensual a cargar.</param>
		/// <returns>Una lista de ReporteMensual con solo sus ID inicializadas</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeReportesMensuales = AccesoADatos.EjecutarSelect(QuerysDeReporteMensual.CARGAR_IDS_POR_IDASIGNACION, parametroIDAsignacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDAsignacion);
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

		/// <summary>
		/// Carga al ReporteMensual con la ID dada.
		/// </summary>
		/// <param name="IDDocumento">La ID del ReporteMensual a cargar.</param>
		/// <returns>El ReporteMensual con la ID dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeReporteMensual = AccesoADatos.EjecutarSelect(QuerysDeReporteMensual.CARGAR_REPORTE_MENSUAL_POR_ID, parametroIDDocumento);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDDocumento);
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

		/// <summary>
		/// Convierte una DataTable a una lista de ReporteMensual con solo sus ID inicializadas y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeReportesMensuales">La DataTable que contiene datos de los Encargado.</param>
		/// <returns>La lista de ReporteMensual con solo sus ID inicializadas contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Convierte una DataTable a un ReporteMensual.
		/// </summary>
		/// <param name="tablaDeReportesMensuales">La DataTable que contiene datos del ReporteMensual<./param>
		/// <returns>El ReporteMensual contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Guarda un ReporteMensual en la base de datos.
		/// </summary>
		/// <param name="reporteMensual">El ReporteMensual a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
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
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeReporteMensual.GUARDAR_REPORTE_MENSUAL, parametrosDocumentoDeEntregaUnica);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDAsignacion);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El ReporteMensual: " + reporteMensual.ToString() + " no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

		/// <summary>
		/// Inicializa un arreglo de SqlParameter basado en un ReporteMensual.
		/// </summary>
		/// <param name="reporteMensual">El ReporteMensual para inicializar los parametros.</param>
		/// <returns>Un arreglo de SqlParameter donde cada posición es uno de los atributos del ReporteMensual.</returns>
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

		/// <summary>
		/// Actualiza un ReporteMensual dada su ID.
		/// </summary>
		/// <param name="IDDocumento">La ID del ReporteMensual a actualizar.</param>
		/// <param name="reporteMensual">El ReporteMensual a actualizar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
				filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeReporteMensual.ACTUALIZAR_REPORTE_MENSUAL, parametrosDrProyecto);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, reporteMensual);
			}

			if (filasAfectadas <= 0)
			{
				throw new AccesoADatosException("El Proyecto con IDProyecto: " + IDDocumento + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
			}
		}

		/// <summary>
		/// Obtiene el ultimo ID insertado en la tabla de ReporteMensual en la base de datos.
		/// </summary>
		/// <returns>El ultimo ID insertado en la tabla de ReporteMensual</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiró una excepción. </exception>
		/// <exception cref="InvalidCastException">Tira esta excepción si la base de datos no regresa un valor entero.</exception>
		public int ObtenerUltimoIDInsertado()
        {
			int ultimoIDInsertado = 0;

			try
			{
				ultimoIDInsertado = AccesoADatos.EjecutarOperacionEscalar(QuerysDeReporteMensual.OBTENER_ULTIMO_IDINSERTADO);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}

			return ultimoIDInsertado;
		}
    }
}
