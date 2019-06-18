using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using System.Linq;
using System.Reflection;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos Proyecto en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos Proyecto.
	/// </summary>
	public class ProyectoDAO : IProyectoDAO
	{

		/// <summary>
		/// Actualiza un Proyecto dada su ID.
		/// </summary>
		/// <param name="IDProyecto">La ID del Proyecto a actualizar.</param>
		/// <param name="proyecto">El Proyecto a actualizar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public void ActualizarProyectoPorID(int IDProyecto, Proyecto proyecto)
		{
            if (IDProyecto <= 0)
            {
                throw new AccesoADatosException("Error al Actualizar Proyecto Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            SqlParameter[] parametrosDrProyecto = InicializarParametrosDeSql(proyecto);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeProyecto.ACTUALIZAR_PROYECTO_POR_ID, parametrosDrProyecto);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, proyecto);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El Proyecto con IDProyecto: " + IDProyecto + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
            }
        }

		/// <summary>
		/// Carga una lista de Proyecto con solo sus ID incializadas y sus demas atributos como null basado en la ID de Encargado relacionada.
		/// </summary>
		/// <param name="IDEncargado">La ID del Encargado relacionado a las ID de Proyecto a cargar.</param>
		/// <returns>Una lista de Proyecto con solo sus ID inicializadas</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
		public List<Proyecto> CargarIDsPorIDEncargado(int IDEncargado)
		{
            if (IDEncargado <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDsProyecto Por IDEncargado: " + IDEncargado + ". IDEncargado no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeProyectos = new DataTable();
            SqlParameter[] parametroIDEncargado = new SqlParameter[1];
            parametroIDEncargado[0] = new SqlParameter
            {
                ParameterName = "@IDEncargado",
                Value = IDEncargado
            };
            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect(QuerysDeProyecto.CARGAR_IDS_POR_IDENCARGADO, parametroIDEncargado);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDEncargado);
			}
			List<Proyecto> listaDeProyectos = new List<Proyecto>();
            try
            {
                listaDeProyectos = ConvertirDataTableAListaDeProyectosConSoloID(tablaDeProyectos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto en cargar IDsProyecto con IDEncargado: " + IDEncargado, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return listaDeProyectos;
        }

		/// <summary>
		/// Cuenta la cantidad de Alumno relacionados por una Asignacion a un Proyecto.
		/// </summary>
		/// <param name="IDProyecto">La ID del Proyecto del que contar los Alumno asignados.</param>
		/// <returns>La cantidad de Alumnos relacionados al Proyecto con la ID dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
		public int ContarAlumnosAsignadosAProyecto(int IDProyecto)
		{
			if (IDProyecto <= 0)
			{
				throw new AccesoADatosException("Error al contar Alumnos asignados a Proyecto Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
			}
			SqlParameter[] parametroIDProyecto = new SqlParameter[1];
			parametroIDProyecto[0] = new SqlParameter
			{
				ParameterName = "@IDProyecto",
				Value = IDProyecto
			};
			int cuenta = 0;
			try
			{
				cuenta = AccesoADatos.EjecutarOperacionEscalar(QuerysDeProyecto.CONTAR_ALUMNOS_ASIGNACIONS_A_PROYECTO, parametroIDProyecto);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDProyecto);
			}
			return cuenta;
		}

		/// <summary>
		/// Carga al Proyecto con la ID dada.
		/// </summary>
		/// <param name="IDProyecto">La ID del Proyecto a cargar.</param>
		/// <returns>El Proyecto con la ID dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Proyecto CargarProyectoPorID(int IDProyecto)
		{
            if (IDProyecto <= 0)
            {
                throw new AccesoADatosException("Error al cargar Proyecto Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeProyecto = new DataTable();
            SqlParameter[] parametroIDProyecto = new SqlParameter[1];
            parametroIDProyecto[0] = new SqlParameter
            {
                ParameterName = "@IDProyecto",
                Value = IDProyecto
            };

            try
            {
                tablaDeProyecto = AccesoADatos.EjecutarSelect(QuerysDeProyecto.CARGAR_PROYECTO_POR_ID, parametroIDProyecto);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDProyecto);
			}
			Proyecto proyecto = new Proyecto();
            try
            {
                proyecto = ConvertirDataTableAProyecto(tablaDeProyecto);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto en cargar Proyecto con IDProyecto: " + IDProyecto, e,TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return proyecto;
        }

		/// <summary>
		/// Carga una lista de Proyecto con solo sus ID incializadas y sus demas atributos como null basado en la ID de Solicitud relacionada.
		/// </summary>
		/// <param name="IDSolicitud">La ID de la Solicitud relacionado a las ID de Proyecto a cargar.</param>
		/// <returns>Una lista de Proyecto con solo sus ID inicializadas</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
		public List<Proyecto> CargarIDsPorIDSolicitud(int IDSolicitud)
        {
			if (IDSolicitud <= 0)
			{
				throw new AccesoADatosException("Error al cargar IDsProyecto Por IDSolicitud: " + IDSolicitud + ". IDSolicitud no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
			}
			DataTable tablaDeProyectos = new DataTable();
			SqlParameter[] parametroIDSolicitud = new SqlParameter[1];
			parametroIDSolicitud[0] = new SqlParameter
			{
				ParameterName = "@IDSolicitud",
				Value = IDSolicitud
			};
			try
			{
				tablaDeProyectos = AccesoADatos.EjecutarSelect(QuerysDeProyecto.CARGAR_IDS_POR_IDSOLICITUD, parametroIDSolicitud);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDSolicitud);
			}
			List<Proyecto> proyectos = new List<Proyecto>();
			try
			{
				proyectos = ConvertirDataTableAListaDeProyectosConSoloID(tablaDeProyectos);
			}
			catch (FormatException e)
			{
				throw new AccesoADatosException("Error al convertir datatable a Proyectos en cargar IDsProyectos con IDSoliciutd: " + IDSolicitud, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
			}
			return proyectos;
		}

		/// <summary>
		/// Carga una lista de todos los Proyecto que tengan el estado dado.
		/// </summary>
		/// <param name="estadoDeProyecto">El estado de los Proyecto a cargar.</param>
		/// <returns>Una lista de todos los Proyecto con el estado dado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<Proyecto> CargarProyectosPorEstado(EstadoProyecto estadoDeProyecto)
		{
            DataTable tablaDeProyectos = new DataTable();
            SqlParameter[] parametroEstadoDeProyecto = new SqlParameter[1];
            parametroEstadoDeProyecto[0] = new SqlParameter
            {
                ParameterName = "@EstadoDeProyecto",
                Value = (int)estadoDeProyecto
            };
            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect(QuerysDeProyecto.CARGAR_PROYECTOS_POR_ESTADO, parametroEstadoDeProyecto);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, estadoDeProyecto);
			}

			List<Proyecto> proyectos = new List<Proyecto>();
            try
            {
                proyectos = ConvertirDataTableAListaDeProyectos(tablaDeProyectos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto en cargar Proyectos con estado: " + estadoDeProyecto.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return proyectos;
        }

		/// <summary>
		/// Carga a todos los Proyecto en la base de datos.
		/// </summary>
		/// <returns>Una lista con todos los Proyecto.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<Proyecto> CargarProyectosTodos()
        {
            DataTable tablaDeProyectos = new DataTable();
            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect(QuerysDeProyecto.CARGAR_PROYECTOS_TODOS);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}
			List<Proyecto> proyectos = new List<Proyecto>();
            try
            {
                proyectos = ConvertirDataTableAListaDeProyectos(tablaDeProyectos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Proyectos en cargar todos los Proyectos", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return proyectos;
        }

		/// <summary>
		/// Carga un Proyecto con solo su ID inicializada y sus demas atributos en como null basado en la ID de Asignación dada.
		/// </summary>
		/// <param name="IDAsignacion">La ID de la Asignación relacionada a la ID a cargar.</param>
		/// <returns>Un Proyecto con solo su ID inicializada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Proyecto CargarIDPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar Proyecto Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeProyecto = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };
            try
            {
                tablaDeProyecto = AccesoADatos.EjecutarSelect(QuerysDeProyecto.CARGAR_ID_POR_IDASIGNACION, parametroIDAsignacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDAsignacion);
			}
			Proyecto proyecto = new Proyecto();
            try
            {
                proyecto = ConvertirDataTableAProyectoConSoloID(tablaDeProyecto);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto cargar Proyecto con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return proyecto;
        }

		/// <summary>
		/// Convierte una DataTable a una lista de Proyecto.
		/// </summary>
		/// <param name="tablaDeProyectos">La DataTable que contiene datos de los Proyecto.</param>
		/// <returns>La lista de Proyecto contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<Proyecto> ConvertirDataTableAListaDeProyectos (DataTable tablaDeProyectos)
		{
            AsignacionDAO asignacionDAO = new AsignacionDAO();
			EncargadoDAO encargadoDAO = new EncargadoDAO();
            List<Proyecto> listaDeProyectos = new List<Proyecto>();
            foreach (DataRow fila in tablaDeProyectos.Rows)
            {
                Proyecto proyecto = new Proyecto()
                {
                    IDProyecto = (int)fila["IDProyecto"],
                    Nombre = fila["Nombre"].ToString(),
                    DescripcionGeneral = fila["DescripcionGeneral"].ToString(),
                    ObjetivoGeneral = fila["ObjetivoGeneral"].ToString(),
                    Cupo = (int)fila["Cupo"],
                    Asignaciones = asignacionDAO.CargarIDsPorIDProyecto((int)fila["IDProyecto"]),
					Estado = (EstadoProyecto)fila["Estado"],
					Encargado = encargadoDAO.CargarIDPorIDProyecto((int)fila["IDProyecto"])
                };
                listaDeProyectos.Add(proyecto);
            }
            return listaDeProyectos;
        }
		
		/// <summary>
		/// Convierte una DataTable a una lista de Proyecto con solo sus ID inicializadas y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeProyectos">La DataTable que contiene datos de los Proyecto.</param>
		/// <returns>La lista de Proyecto con solo sus ID inicializadas contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<Proyecto> ConvertirDataTableAListaDeProyectosConSoloID(DataTable tablaDeProyectos)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            List<Proyecto> listaDeProyectos = new List<Proyecto>();
            foreach (DataRow fila in tablaDeProyectos.Rows)
            {
                Proyecto proyecto = new Proyecto()
                {
                    IDProyecto = (int)fila["IDProyecto"],
                };
                listaDeProyectos.Add(proyecto);
            }
            return listaDeProyectos;
        }

		/// <summary>
		/// Convierte una DataTable a un Proyecto.
		/// </summary>
		/// <param name="tablaDeProyecto">La DataTable que contiene datos del Proyecto<./param>
		/// <returns>El Proyecto contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private Proyecto ConvertirDataTableAProyecto (DataTable tablaDeProyecto)
		{
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Proyecto proyecto = new Proyecto();
            foreach (DataRow fila in tablaDeProyecto.Rows)
            {
                proyecto.IDProyecto = (int)fila["IDProyecto"];
                proyecto.Nombre = fila["Nombre"].ToString();
                proyecto.DescripcionGeneral = fila["DescripcionGeneral"].ToString();
                proyecto.ObjetivoGeneral = fila["ObjetivoGeneral"].ToString();
                proyecto.Cupo = (int)fila["Cupo"];
                proyecto.Asignaciones = asignacionDAO.CargarIDsPorIDProyecto((int)fila["IDProyecto"]);
				proyecto.Estado = (EstadoProyecto)fila["Estado"];

			}
            return proyecto;
		}

		/// <summary>
		/// Convierte una DataTable a un Proyecto con solo su ID inicializada y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeProyecto">La DataTable que contiene datos del Proyecto.</param>
		/// <returns>El Proyecto con solo su ID inicializada contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private Proyecto ConvertirDataTableAProyectoConSoloID(DataTable tablaDeProyecto)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Proyecto proyecto = new Proyecto();
            foreach (DataRow fila in tablaDeProyecto.Rows)
            {
                proyecto.IDProyecto = (int)fila["IDProyecto"];
            }
            return proyecto;
        }

		/// <summary>
		/// Guarda un Proyecto en la base de datos.
		/// </summary>
		/// <param name="proyecto">El Encargado a Proyecto.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void GuardarProyecto(Proyecto proyecto)
        {
            SqlParameter[] parametrosDeProyecto = InicializarParametrosDeSql(proyecto);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeProyecto.GUARDAR_PROYECTO, parametrosDeProyecto);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, proyecto);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Asignacion: " + proyecto.ToString() + " no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

		/// <summary>
		/// Inicializa un arreglo de SqlParameter basado en un Proyecto.
		/// </summary>
		/// <param name="proyecto">El Proyecto para inicializar los parametros.</param>
		/// <returns>Un arreglo de SqlParameter donde cada posición es uno de los atributos del Proyecto.</returns>
		private SqlParameter[] InicializarParametrosDeSql(Proyecto proyecto)
        {
            SqlParameter[] parametrosDeProyecto = new SqlParameter[7];

            for (int i = 0; i < parametrosDeProyecto.Length; i++)
            {
                parametrosDeProyecto[i] = new SqlParameter();
            }
            parametrosDeProyecto[0].ParameterName = "@IDProyecto";
            parametrosDeProyecto[0].Value = proyecto.IDProyecto;
            parametrosDeProyecto[1].ParameterName = "@NombreProyecto";
            parametrosDeProyecto[1].Value = proyecto.Nombre;
            parametrosDeProyecto[2].ParameterName = "@EstadoProyecto";
            parametrosDeProyecto[2].Value = (int)proyecto.Estado;
            parametrosDeProyecto[3].ParameterName = "@DescripcionGeneralProyecto";
            parametrosDeProyecto[3].Value = proyecto.DescripcionGeneral;
            parametrosDeProyecto[4].ParameterName = "@ObjetivoGeneralProyecto";
            parametrosDeProyecto[4].Value = proyecto.ObjetivoGeneral;
            parametrosDeProyecto[5].ParameterName = "@CupoProyecto";
            parametrosDeProyecto[5].Value = proyecto.Cupo;
            parametrosDeProyecto[6].ParameterName = "@IDEncargado";
            parametrosDeProyecto[6].Value = proyecto.Encargado.IDEncargado;

            return parametrosDeProyecto;
        }
    }
}
