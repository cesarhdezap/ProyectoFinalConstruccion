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
	public class ProyectoDAO : IProyectoDAO
	{
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
