using System;
using System.Data;
using System.Collections.Generic;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class SolicitudDAO : ISolicitudDAO
	{
        public void ActualizarSolicitudPorID(int IDSolicitud, Solicitud solicitud)
        {
			throw new NotImplementedException();
        }

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
                tablaDeMatricula = AccesoADatos.EjecutarSelect("SELECT * FROM Solicitudes WHERE IDSolicitud = @IDSolicitud", parametroIDSolicitud);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al cargar Solicitud con IDSolicitud: " + IDSolicitud, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar Solicitud con IDSolicitud: " + IDSolicitud, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Solicitudes(Fecha, Matricula) VALUES(@Fecha, @Matricula)", parametrosDeSolicitud);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al guardar solicitud: " + solicitud.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al guardar solicitud: " + solicitud.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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
                   filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO SolicitudesProyectos(IDSolicitud, IDProyecto) VALUES(@IDSolicitud, @IDProyecto)", parametrosDeSolicitud);
                }
				catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
				{
					throw new AccesoADatosException("Error al guardar relacion Proyectos-Solicitud: " + solicitud.ToString() + proyecto.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
				}
				catch (SqlException e)
				{
					throw new AccesoADatosException("Error al guardar relacion Proyectos-Solicitud: " + solicitud.ToString() + proyecto.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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

        public Solicitud CargarIDPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDSolicitud Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeSolicitud = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };
            try
            {
                tablaDeSolicitud = AccesoADatos.EjecutarSelect("Query", parametroIDAsignacion);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar IDSolicitud con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar IDSolicitud con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			Solicitud solicitud = new Solicitud();
            try
            {
                solicitud = ConvertirDataTableASolicutudConSoloID(tablaDeSolicitud);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Solicitud en cargar IDSolicitud con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return solicitud;
        }

		public Solicitud CargarIDPorMatricula(string matriculaAlumno)
		{
			DataTable tablaDeSolicitud;
			SqlParameter[] parametroMatricula = new SqlParameter[1];
			parametroMatricula[0] = new SqlParameter
			{
				ParameterName = "@MatriculaAlumno",
				Value = matriculaAlumno
			};

			try
			{
				tablaDeSolicitud = AccesoADatos.EjecutarSelect("SELECT IDSolicitud FROM Solicitudes WHERE Matricula = @MatriculaAlumno", parametroMatricula);
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar IDSolicitud con Matricula: " + matriculaAlumno, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{ 
				throw new AccesoADatosException("Error al cargar IDSolicitud con Matricula: " + matriculaAlumno, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			Solicitud solicitud = new Solicitud();
			if (tablaDeSolicitud.Rows.Count > 0)
			{
				try
				{
					solicitud = ConvertirDataTableASolicutudConSoloID(tablaDeSolicitud);
				}
				catch (FormatException e)
				{
					throw new AccesoADatosException("Error al convertir datatable a Solicitud en cargar IDSolicitud con Matricula: " + matriculaAlumno, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
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
                ultimoIDInsertado = AccesoADatos.EjecutarOperacionEscalar("SELECT IDENT_CURRENT('Solicitudes')");
            }
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
            {
                throw new AccesoADatosException("Error al obtener Ultimo ID Insertado en SolicitudDAO", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al obtener Ultimo ID Insertado en SolicitudDAO", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			return ultimoIDInsertado;
        }
    }
}
