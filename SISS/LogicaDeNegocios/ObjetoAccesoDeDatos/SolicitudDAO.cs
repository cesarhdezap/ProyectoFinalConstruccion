using System;
using System.Data;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class SolicitudDAO : ISolicitudDAO
	{
		/// <summary>
		/// Carga la Solicitud con la ID dada.
		/// </summary>
		/// <param name="IDSolicitud">La ID de la Solicitud a cargar.</param>
		/// <returns>La Solicitud con la ID dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Solicitud CargarSolicitudPorID(int IDSolicitud)
		{
            if (IDSolicitud <= 0)
            {
                throw new AccesoADatosException("Error al Cargar Solicitud Por IDSolicitud: " + IDSolicitud + ". IDSolicitud no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }

            DataTable tablaDeMatricula = new DataTable();
            SqlParameter[] parametroIDSolicitud = new SqlParameter[1];

            parametroIDSolicitud[0] = new SqlParameter
            {
                ParameterName = "@IDSolicitud",
                Value = IDSolicitud
            };

            try
            {
                tablaDeMatricula = AccesoADatos.EjecutarSelect(QuerysDeSolicitud.CARGAR_SOLICITUD_POR_ID, parametroIDSolicitud);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDSolicitud);
			}

			Solicitud solicitud = new Solicitud();

            try
            {
                solicitud = ConvertirDataTableASolicutud(tablaDeMatricula);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Solicitud en cargar Solicitud con IDSolicitud: " + IDSolicitud, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return solicitud;
        }

		/// <summary>
		/// Convierte una DataTabla a una Solicitud.
		/// </summary>
		/// <param name="tablaDeSolicitud">La DataTable que contiene datos de la Solicitud</param>
		/// <returns>La Solicitud contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepcion si hay algún error de casteo en la conversión.</exception>
		private Solicitud ConvertirDataTableASolicutud(DataTable tablaDeSolicitud)
		{
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Solicitud solicitud = new Solicitud();

            foreach (DataRow fila in tablaDeSolicitud.Rows)
            {
                solicitud.IDSolicitud = (int)fila["IDSolicitud"];
                solicitud.Fecha = (DateTime)fila["Fecha"];
                solicitud.Proyectos = proyectoDAO.CargarIDsPorIDSolicitud((int)fila["IDSolicitud"]);
            }

            return solicitud;
        }

		/// <summary>
		/// Convierte una DataTable a una Solicitud con solo su ID inicializada y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeSolicitud">La DataTable que contiene datos de la Solicitud.</param>
		/// <returns>La Solicitud con solo su ID inicializada contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepcion si hay algún error de casteo en la conversión.</exception>
		private Solicitud ConvertirDataTableASolicutudConSoloID(DataTable tablaDeSolicitud)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Solicitud solicitud = new Solicitud();

            foreach (DataRow fila in tablaDeSolicitud.Rows)
            {
                solicitud.IDSolicitud = (int)fila["IDSolicitud"];
            }

            return solicitud;
        }

		/// <summary>
		/// Guarda una Solicitud a la base de datos.
		/// </summary>
		/// <param name="solicitud">La Solicitud a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public void GuardarSolicitud(Solicitud solicitud)
        {
            SqlParameter[] parametrosDeSolicitud = InicializarParametrosDeSql(solicitud);
            int filasAfectadas = 0;

            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeSolicitud.GUARDAR_SOLICITUD, parametrosDeSolicitud);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, solicitud);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Solicitud: " + solicitud.ToString() + " no fue guardada.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }

            foreach (Proyecto proyecto in solicitud.Proyectos)
            {
                parametrosDeSolicitud = InicializarParametrosDeSql(solicitud);
                parametrosDeSolicitud[2].Value = ObtenerUltimoIDInsertado();
                parametrosDeSolicitud[3].Value = proyecto.IDProyecto;

                try
                {
                   filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeSolicitud.GUARDAR_RELACION_SOLICITUD_PROYECTO, parametrosDeSolicitud);
                }
				catch (SqlException e)
				{
					EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, proyecto);
				}

				if (filasAfectadas <= 0)
                {
                    throw new AccesoADatosException("Relacion Solicitud - Proyecto: " + solicitud.ToString() + proyecto.ToString() + " no fue guardada.",TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
                }
            }
        }

		/// <summary>
		/// Inicializa un arreglo de SqlParameter basado en una Solicitud.
		/// </summary>
		/// <param name="solicitud">La Solicitud para inicializar los parametros.</param>
		/// <returns>Un arreglo de SqlParameter donde cada posición es uno de los atributos de la Solicitud.</returns>
		private static SqlParameter[] InicializarParametrosDeSql(Solicitud solicitud)
        {
            SqlParameter[] parametrosDeSolicitud = new SqlParameter[4];

            for (int i = 0; i < parametrosDeSolicitud.Length; i++)
            {
                parametrosDeSolicitud[i] = new SqlParameter();
            }

            parametrosDeSolicitud[0].ParameterName = "@Fecha";
            parametrosDeSolicitud[0].Value = solicitud.Fecha.ToString();
            parametrosDeSolicitud[1].ParameterName = "@Matricula";
            parametrosDeSolicitud[1].Value = solicitud.Alumno.Matricula;
            parametrosDeSolicitud[2].ParameterName = "@IDSolicitud";
            parametrosDeSolicitud[2].Value = 0;
            parametrosDeSolicitud[3].ParameterName = "@IDProyecto";
            parametrosDeSolicitud[3].Value = 0;
			
            return parametrosDeSolicitud;
        }

		/// <summary>
		/// Carga una Solicitud con solo su ID inicializada y sus demas atributos como null dada la matrícula del Alumno relacionado a la Asignacion.
		/// </summary>
		/// <param name="matriculaAlumno">La matrícula del Alumno relacionado a la Solicitud a cargar.</param>
		/// <returns>Una Solicitud con solo su ID inicializada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Solicitud CargarIDPorMatricula(string matriculaAlumno)
		{
			DataTable tablaDeSolicitud = new DataTable();
			SqlParameter[] parametroMatricula = new SqlParameter[1];

			parametroMatricula[0] = new SqlParameter
			{
				ParameterName = "@MatriculaAlumno",
				Value = matriculaAlumno
			};

			try
			{
				tablaDeSolicitud = AccesoADatos.EjecutarSelect(QuerysDeSolicitud.CARGAR_ID_POR_MATRICULA, parametroMatricula);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, matriculaAlumno);
			}

			Solicitud solicitud = new Solicitud();

			if (tablaDeSolicitud.Rows.Count > 0)
			{
				try
				{
					solicitud = ConvertirDataTableASolicutudConSoloID(tablaDeSolicitud);
				}
				catch (SqlException e)
				{
					EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, matriculaAlumno);
				}
			}
			else
			{
				solicitud = null;
			}

			return solicitud;
		}

		/// <summary>
		/// Obtiene el ultimo ID insertado en la tabla de Solicitud en la base de datos.
		/// </summary>
		/// <returns>El ultimo ID insertado en la tabla de Solicitud</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiró una excepción. </exception>
		/// <exception cref="InvalidCastException">Tira esta excepción si la base de datos no regresa un valor entero.</exception>
		public int ObtenerUltimoIDInsertado()
        {
            int ultimoIDInsertado = 0;

            try
            {
                ultimoIDInsertado = AccesoADatos.EjecutarOperacionEscalar(QuerysDeSolicitud.OBTENER_ULTIMO_ID_INSERTADO);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}

			return ultimoIDInsertado;
        }
    }
}
