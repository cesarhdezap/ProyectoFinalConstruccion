using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos <see cref="Asignacion"/> en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos <see cref="Asignacion"/>.
	/// </summary>
	public class AsignacionDAO : IAsignacionDAO
	{
		/// <summary>
		/// Actualiza una <see cref="Asignacion"/> dada su <see cref="Asignacion.IDAsignacion"/>.
		/// </summary>
		/// <param name="IDAsignacion"><see cref="Asignacion.IDAsignacion"/> de la <see cref="Asignacion"/> a actualizar.</param>
		/// <param name="asignacion">La <see cref="Asignacion"/> a actuliazar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public void ActualizarAsignacionPorID(int IDAsignacion, Asignacion asignacion)
		{
			if (IDAsignacion <= 0)
			{
				throw new AccesoADatosException("Error al Actualizar Asignacion Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
			}

			SqlParameter[] parametrosDeAsignacion = InicializarParametrosDeSql(asignacion);
			int filasAfectadas = 0;

			try
			{
				filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeAsignacion.ACTUALIZAR_ASIGNACION, parametrosDeAsignacion);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, asignacion);
			}

			if (filasAfectadas <= 0)
			{
				throw new AccesoADatosException("La Asignacion con IDAsignacion: " + IDAsignacion + " no existe.", TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
			}
		}

		/// <summary>
		/// Carga la <see cref="Asignacion"/> con la <see cref="Asignacion.IDAsignacion"/> dada.
		/// </summary>
		/// <param name="IDAsignacion"><see cref="Asignacion.IDAsignacion"/> de la <see cref="Asignacion"/> a cargar.</param>
		/// <returns>La <see cref="Asignacion"/> con la <see cref="Asignacion.IDAsignacion"/> dada</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Asignacion CargarAsignacionPorID(int IDAsignacion)
		{
			if (IDAsignacion <= 0)
			{
				throw new AccesoADatosException("Error al cargar Asignacion Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
			}

			DataTable tablaDeAsignacion = new DataTable();
			SqlParameter[] parametroIDAsignacion = new SqlParameter[1];

			parametroIDAsignacion[0] = new SqlParameter
			{
				ParameterName = "@IDAsignacion",
				Value = IDAsignacion
			};

			try
			{
				tablaDeAsignacion = AccesoADatos.EjecutarSelect(QuerysDeAsignacion.CARGAR_ASIGNACION_POR_ID, parametroIDAsignacion);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDAsignacion);
			}

			Asignacion asignacion = new Asignacion();

			try
			{
				asignacion = ConvertirDataTableAAsignacion(tablaDeAsignacion);
			}
			catch (FormatException e)
			{
				throw new AccesoADatosException("Error al convertir datatable a Asignacion en cargar Asignacion con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
			}

			return asignacion;

		}

		/// <summary>
		/// Carga una <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado y sus demas atributos como null dado <see cref="Alumno.Matricula"/> del <see cref="Alumno"/> relacionado a la <see cref="Asignacion"/>.
		/// </summary>
		/// <param name="matricula"><see cref="Alumno.Matricula"/> del <see cref="Alumno"/> relacionado a la <see cref="Asignacion"/> a cargar.</param>
		/// <returns>Una <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Asignacion CargarIDPorMatriculaDeAlumno(string matricula)
		{
			DataTable tablaDeAsignacion = new DataTable();
			SqlParameter[] parametroMatricula = new SqlParameter[1];

			parametroMatricula[0] = new SqlParameter
			{
				ParameterName = "@Matricula",
				Value = matricula
			};

			try
			{
				tablaDeAsignacion = AccesoADatos.EjecutarSelect(QuerysDeAsignacion.CARGAR_ID_POR_MATRICULA_DE_ALUMNO, parametroMatricula);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, matricula);
			}

			Asignacion asignacion = new Asignacion();

			try
			{
				asignacion = ConvertirDataTableAAsignacionConSoloID(tablaDeAsignacion);
			}
			catch (FormatException e)
			{
				throw new AccesoADatosException("Error al convertir datatable a lista de Asignaciones en cargar Asignacion con matricula: " + matricula, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
			}

			return asignacion;
		}

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="Asignacion"/>.
		/// </summary>
		/// <param name="tablaDeAsignaciones">La <see cref="DataTable"/> que contiene datos de la <see cref="Asignacion"/></param>
		/// <returns>La <see cref="Asignacion"/> contenida en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepcion si hay algún error de casteo en la conversión.</exception>
		private Asignacion ConvertirDataTableAAsignacion(DataTable tablaDeAsignaciones)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			ProyectoDAO proyectoDAO = new ProyectoDAO();
			ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
			DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
			Asignacion asignacion = new Asignacion();

			foreach (DataRow fila in tablaDeAsignaciones.Rows)
			{
				asignacion.IDAsignacion = (int)fila["IDAsignacion"];
				asignacion.EstadoAsignacion = (EstadoAsignacion)fila["Estado"];
				asignacion.FechaDeInicio = DateTime.Parse(fila["FechaDeInicio"].ToString());
				asignacion.Alumno = alumnoDAO.CargarMatriculaPorIDAsignacion((int)fila["IDAsignacion"]);
				asignacion.Proyecto = proyectoDAO.CargarIDPorIDAsignacion((int)fila["IDAsignacion"]);
				asignacion.DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]);
				asignacion.ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]);

				if (fila["FechaDeFinal"].ToString() != string.Empty)
				{
					asignacion.FechaDeFinal = DateTime.Parse(fila["FechaDeFinal"].ToString());
				}

				if (fila["IDLiberacion"].ToString() != string.Empty)
				{
					asignacion.Liberacion = new Liberacion { IDLiberacion = (int)fila["IDLiberacion"] };
				}

				if (fila["IDSolicitud"].ToString() != string.Empty)
				{
					asignacion.Solicitud = new Solicitud { IDSolicitud = (int)fila["IDSolicitud"] };
				}
			}
			return asignacion;
		}

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeAsignaciones">La <see cref="DataTable"/> que contiene datos de la <see cref="Asignacion"/>.</param>
		/// <returns>La <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado contenida en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepcion si hay algún error de casteo en la conversión.</exception>
		private Asignacion ConvertirDataTableAAsignacionConSoloID(DataTable tablaDeAsignaciones)
		{
			Asignacion asignacion = new Asignacion();

			foreach (DataRow fila in tablaDeAsignaciones.Rows)
			{
				asignacion.IDAsignacion = (int)fila["IDAsignacion"];
			}

			return asignacion;
		}

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{Asignacion}"/> de <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeAsignaciones">La <see cref="DataTable"/> que contiene datos de las <see cref="Asignacion"/>.</param>
		/// <returns>Una <see cref="List{Asignacion}"/> de <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado contenida en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepcion si hay algún error de casteo en la conversión.</exception>
		private List<Asignacion> ConvertirDataTableAListaDeAsignacionesConSoloID(DataTable dataTableAsignaciones)
		{
			List<Asignacion> listaDeAsignaciones = new List<Asignacion>();

			foreach (DataRow fila in dataTableAsignaciones.Rows)
			{
				Asignacion asignacion = new Asignacion
				{
					IDAsignacion = (int)fila["IDAsignacion"],
				};
				listaDeAsignaciones.Add(asignacion);
			}

			return listaDeAsignaciones;
		}

		/// <summary>
		/// Carga una <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado y sus demas atributos como null dado <see cref="Proyecto.IDProyecto"/> del <see cref="Proyecto"/> relacionado a la <see cref="Asignacion"/>.
		/// </summary>
		/// <param name="IDProyecto"><see cref="Proyecto.IDProyecto"/> del <see cref="Proyecto"/> relacionado a la <see cref="Asignacion"/> a cargar.</param>
		/// <returns>Una <see cref="Asignacion"/> con solo <see cref="Asignacion.IDAsignacion"/> inicializado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<Asignacion> CargarIDsPorIDProyecto(int IDProyecto)
		{
			if (IDProyecto <= 0)
			{
				throw new AccesoADatosException("Error al cargar IDAsignacion Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
			}

			DataTable tablaDeAsignaciones = new DataTable();
			SqlParameter[] parametroIDProyecto = new SqlParameter[1];

			parametroIDProyecto[0] = new SqlParameter
			{
				ParameterName = "@IDProyecto",
				Value = IDProyecto
			};

			try
			{
				tablaDeAsignaciones = AccesoADatos.EjecutarSelect(QuerysDeAsignacion.CARGAR_IDS_POR_IDPROYECTO, parametroIDProyecto);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDProyecto);
			}

			List<Asignacion> listaDeAsignaciones = new List<Asignacion>();

			try
			{
				listaDeAsignaciones = ConvertirDataTableAListaDeAsignacionesConSoloID(tablaDeAsignaciones);
			}
			catch (FormatException e)
			{
				throw new AccesoADatosException("Error al convertir datatable a lista de Asignaciones en cargar IDsAsignacion con IDProyecto: " + IDProyecto, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
			}

			return listaDeAsignaciones;
		}

		/// <summary>
		/// Guarda una <see cref="Asignacion"/> a la base de datos.
		/// </summary>
		/// <param name="asignacion">La <see cref="Asignacion"/> a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public void GuardarAsignacion(Asignacion asignacion)
		{
			SqlParameter[] parametrosDeAsignacion = InicializarParametrosDeSql(asignacion);
			int filasAfectadas = 0;

			try
			{
				filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeAsignacion.GUARDAR_ASIGNACION, parametrosDeAsignacion);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, asignacion);
			}

			if (filasAfectadas <= 0)
			{
				throw new AccesoADatosException("Asignacion: " + asignacion.ToString() + " no fue guardada.", TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
			}
		}

		/// <summary>
		/// Inicializa un arreglo de <see cref="SqlParameter"/> basado en una <see cref="Asignacion"/>.
		/// </summary>
		/// <param name="asignacion">La <see cref="Asignacion"/> para inicializar los parametros.</param>
		/// <returns>Un arreglo de <see cref="SqlParameter"/> donde cada posición es uno de los atributos de la <see cref="Asignacion"/>.</returns>
		private SqlParameter[] InicializarParametrosDeSql(Asignacion asignacion)
        {
            SqlParameter[] parametrosDeAsignacion = new SqlParameter[8];

            for (int i = 0; i < parametrosDeAsignacion.Length; i++)
            {
                parametrosDeAsignacion[i] = new SqlParameter();
            }

            parametrosDeAsignacion[0].ParameterName = "@IDAsignacion";
            parametrosDeAsignacion[0].Value = asignacion.IDAsignacion;
            parametrosDeAsignacion[1].ParameterName = "@EstadoAsignacion";
            parametrosDeAsignacion[1].Value = asignacion.EstadoAsignacion;
            parametrosDeAsignacion[2].ParameterName = "@FechaDeInicioAsignacion";
            parametrosDeAsignacion[2].Value = asignacion.FechaDeInicio.ToString();
            parametrosDeAsignacion[3].ParameterName = "@FechaDeFinalAsignacion";
            parametrosDeAsignacion[3].Value = asignacion.FechaDeFinal.ToString();
            parametrosDeAsignacion[4].ParameterName = "@MatriculaDeAlumnoAsignacion";
            parametrosDeAsignacion[4].Value = asignacion.Alumno.Matricula;
            parametrosDeAsignacion[5].ParameterName = "@IDProyectoAsignacion";
            parametrosDeAsignacion[5].Value = asignacion.Proyecto.IDProyecto;
            parametrosDeAsignacion[6].ParameterName = "@IDSolicitudAsignacion";
            parametrosDeAsignacion[6].Value = asignacion.Solicitud.IDSolicitud; 
            parametrosDeAsignacion[7].ParameterName = "@IDLiberacionAsignacion";
            parametrosDeAsignacion[7].Value = asignacion.Liberacion.IDLiberacion;

            return parametrosDeAsignacion;
        }
    }
}
