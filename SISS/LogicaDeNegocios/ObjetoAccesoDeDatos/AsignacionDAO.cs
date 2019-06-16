using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using System.Linq;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AsignacionDAO : IAsignacionDAO
	{
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

		private Asignacion ConvertirDataTableAAsignacion(DataTable tablaDeAsignaciones)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			ProyectoDAO proyectoDAO = new ProyectoDAO();
			ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
			DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
			Asignacion asignacion = new Asignacion();

			foreach (DataRow fila in tablaDeAsignaciones.Rows) {
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

		private Asignacion ConvertirDataTableAAsignacionConSoloID(DataTable tablaDeAsignaciones)
		{
			Asignacion asignacion = new Asignacion();

			foreach (DataRow fila in tablaDeAsignaciones.Rows)
			{
				asignacion.IDAsignacion = (int)fila["IDAsignacion"];
			}
			return asignacion;
		}

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
