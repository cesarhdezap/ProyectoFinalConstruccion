using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AsignacionDAO : Interfaces.IAsignacionDAO
	{
		public void ActualizarAsignacionPorID(int IDasignacion, Asignacion asignacion)
		{
            SqlParameter[] parametrosDeAsignacion = InicializarParametrosDeSql(asignacion);

            try
            {
                AccesoADatos.EjecutarInsertInto("UPDATE Asignaciones SET EstadoAsignacion = @EstadoAsignacion, FechaDeFinal = @FechaDeFinalAsignacion, IDLiberacion = @IDLiberacionAsignacion WHERE IDAsignacion = @IDAsignacion ", parametrosDeAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al actualizar asignacion: " + asignacion.ToString(), e);
            }
        }

		public Asignacion CargarAsignacionPorID(int IDAsignacion)
		{
            DataTable tablaDeAsignacion = new DataTable();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };
            try
            {
                tablaDeAsignacion = AccesoADatos.EjecutarSelect("SELECT * FROM Asignaciones WHERE IDAsignacion = @IDAsignacion", parametros);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar asignacion con ID: " + IDAsignacion, e);
            }

            Asignacion asignacion = new Asignacion();
            try
            {
                asignacion = ConvertirDataTableAAsignacion(tablaDeAsignacion);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a asignacion en cargar asignacion con id: " + IDAsignacion, e);
            }
            return asignacion;

		}
        public List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula)
		{
            DataTable tablaDeAsignaciones = new DataTable();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter
            {
                ParameterName = "@matricula",
                Value = matricula
            };
            try
            {
                tablaDeAsignaciones = AccesoADatos.EjecutarSelect("SELECT IDAsignacion FROM Alumnos WHERE Matricula = @Matricula", parametros);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar is de asignacion con matricula: " + matricula, e);
            }

            List<Asignacion> asignaciones = new List<Asignacion>();
            try
            {
                asignaciones = ConvertirDataTableAListaDeAsignaciones(tablaDeAsignaciones);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error convertir datatable a lista de asgianaciones al cargar asignacion con matricula: " + matricula, e);
            }
            return asignaciones;
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
                asignacion.EstadoAsignacion = (EstadoAsignacion)fila["estadoAsignacion"];
                asignacion.FechaDeInicio = DateTime.Parse(fila["fechaDeInicio"].ToString());
                asignacion.FechaDeFinal = DateTime.Parse(fila["fechaDeFinal"].ToString());
                asignacion.Alumno = alumnoDAO.CargarMatriculaPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.Proyecto = proyectoDAO.CargarIDProyectoPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.Liberacion = new Liberacion { IDLiberacion = (int)fila["IDLiberacion"] };
                asignacion.Solicitud = new Solicitud { IDSolicitud =(int)fila["IDLiberacion"]};
            }

            return asignacion;
        }

        private List<Asignacion> ConvertirDataTableAListaDeAsignaciones(DataTable dataTableAsignaciones)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            List<Asignacion> listaDeAsignaciones = new List<Asignacion>();
            foreach (DataRow fila in dataTableAsignaciones.Rows)
            {
                Asignacion asignacion = new Asignacion
                {
                    IDAsignacion = (int)fila["IDAsignacion"],
                    EstadoAsignacion = (EstadoAsignacion)fila["estadoAsignacion"],
                    FechaDeInicio = DateTime.Parse(fila["fechaDeInicio"].ToString()),
                    FechaDeFinal = DateTime.Parse(fila["fechaDeFinal"].ToString()),
                    Alumno = alumnoDAO.CargarMatriculaPorIDAsignacion((int)fila["IDAsignacion"]),
                    Proyecto = proyectoDAO.CargarIDProyectoPorIDAsignacion((int)fila["IDAsignacion"]),
                    DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]),
                    ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]),
                    Liberacion = new Liberacion { IDLiberacion = (int)fila["IDLiberacion"] },
                    Solicitud = new Solicitud { IDSolicitud = (int)fila["IDLiberacion"] },
            };
                listaDeAsignaciones.Add(asignacion);
            }
            return listaDeAsignaciones;
        }
        public List<Asignacion> CargarIDsPorIDProyecto(int IDProyecto)
        {
            DataTable tablaDeAsignaciones = new DataTable();
            SqlParameter[] parametroIDProyecto = new SqlParameter[1];
            parametroIDProyecto[0] = new SqlParameter
            {
                ParameterName = "@IDProyecto",
                Value = IDProyecto
            };

            try
            {
                tablaDeAsignaciones = AccesoADatos.EjecutarSelect("SELECT IDAsignacion FROM Asignaciones WHERE IDProyecto = @IDProyecto", parametroIDProyecto);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDs por ID de proyecto: " + IDProyecto, e);
            }
            List<Asignacion> listaDeAsignaciones = new List<Asignacion>();
            try
            {
                listaDeAsignaciones = ConvertirDataTableAListaDeAsignaciones(tablaDeAsignaciones);
            }
            catch (FormatException e)
            { 
                throw new AccesoADatosException("Error al convertir datatable a lista de asignaciones en cargar IDs por ID de proyecto: " + IDProyecto, e);
            }
            return listaDeAsignaciones;
        }
        public void GuardarAsignacion(Asignacion asignacion)
		{
            SqlParameter[] parametrosDeAsignacion = InicializarParametrosDeSql(asignacion);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Asignaciones(EstadoAsignacion, FechaDeInicion, HorasCubiertas, MatriculaDeAlumno, IDProyecto, IDSolicitud) VALUES(@EstadoAsignacion, @FechaDeInicio, @HorasCubiertas, @MatriculaDeAlumnoAsignacion, @IDProyectoAsignacion, @IDSolicitudAsignacion)", parametrosDeAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar asignacion:" + asignacion.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Asignacion: " + asignacion.ToString() + " no fue guardada.");
            }
			
		}
        
        private SqlParameter[] InicializarParametrosDeSql(Asignacion asignacion)
        {
            SqlParameter[] parametrosDeAsignacion = new SqlParameter[9];

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
            parametrosDeAsignacion[8].ParameterName = "@HorasCubiertas";
            parametrosDeAsignacion[8].Value = asignacion.HorasCubiertas;

            return parametrosDeAsignacion;
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}
