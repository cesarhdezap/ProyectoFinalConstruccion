using System;
using System.Data;
using System.Collections.Generic;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using System.Linq;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class SolicitudDAO : ISolicitudDAO
	{
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
