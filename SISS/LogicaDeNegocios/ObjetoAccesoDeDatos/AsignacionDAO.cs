using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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

            }
        }

		public Asignacion CargarAsignacionPorID(int IDasignacion)
		{
            DataTable tablaDeAsignacion = new DataTable();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@IDasignacion";
            parametros[0].Value = IDasignacion;
            try
            {
                tablaDeAsignacion = AccesoADatos.EjecutarSelect("SELECT * FROM Asignaciones WHERE IDAsignacion = @IDasignacion", parametros);
            } catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            Asignacion asignacion = new Asignacion();

            asignacion = ConvertirDataTableAAsignacion(tablaDeAsignacion);

            return asignacion;

		}
        public List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula)
		{
            DataTable tablaDeAsignaciones = new DataTable();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@matricula";
            parametros[0].Value = matricula;
            try
            {
                tablaDeAsignaciones = AccesoADatos.EjecutarSelect("SELECT IDAsignacion FROM Alumnos WHERE Matricula = @Matricula", parametros);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            List<Asignacion> asignaciones = new List<Asignacion>();

            asignaciones = ConvertirDataTableAListaDeAsignaciones(tablaDeAsignaciones);

            return asignaciones;
        }


        private Asignacion ConvertirDataTableAAsignacion(DataTable DataTableAsignaciones)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            Asignacion asignacion = (Asignacion)(from DataRow fila in DataTableAsignaciones.Rows
                                                 select new Asignacion()
                                                 {
                                                     IDAsignacion = (int)fila["IDAsignacion"],
                                                     EstadoAsignacion = (EstadoAsignacion)fila["estadoAsignacion"],
                                                     FechaDeInicio = (DateTime)fila["fechaDeInicio"],
                                                     FechaDeFinal = (DateTime)fila["fechaDeFinal"],
                                                     Alumno = alumnoDAO.CargarMatriculaPorIDAsignacion((int)fila["IDAsignacion"]),
                                                     Proyecto = proyectoDAO.CargarIDProyectoPorIDAsignacion((int)fila["IDAsignacion"]),
                                                     DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]),
                                                     ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]),
                                                     //TODO Logica para cargar liberacion y solicitud si es que existe
                                                 }
                             );
            return asignacion;
        }

        private List<Asignacion> ConvertirDataTableAListaDeAsignaciones(DataTable dataTableAsignaciones)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            List<Asignacion> listaDeAsignaciones = (from DataRow fila in dataTableAsignaciones.Rows
                                                         select new Asignacion()
                                                         {
                                                             IDAsignacion = (int)fila["IDAsignacion"],
                                                             EstadoAsignacion = (EstadoAsignacion)fila["estadoAsignacion"],
                                                             FechaDeInicio = (DateTime)fila["fechaDeInicio"],
                                                             FechaDeFinal = (DateTime)fila["fechaDeFinal"],
                                                             Alumno = alumnoDAO.CargarMatriculaPorIDAsignacion((int)fila["IDAsignacion"]),
                                                             Proyecto = proyectoDAO.CargarIDProyectoPorIDAsignacion((int)fila["IDAsignacion"]),
                                                             DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]),
                                                             ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]),
                                                             //TODO Logica para cargar liberacion y solicitud si es que existe
                                                         }
                             ).ToList();
            return listaDeAsignaciones;
        }
        public List<Asignacion> CargarIDsPorIDProyecto(int IDProyecto)
        {
            DataTable tablaDeAsignaciones = new DataTable();
            SqlParameter[] parametroIDProyecto = new SqlParameter[1];
            parametroIDProyecto[1] = new SqlParameter();
            parametroIDProyecto[1].ParameterName = "@IDProyecto";
            parametroIDProyecto[1].Value = IDProyecto;

            try
            {
                tablaDeAsignaciones = AccesoADatos.EjecutarSelect("SELECT IDAsignacion FROM Asignaciones WHERE IDProyecto = @IDProyecto", parametroIDProyecto);
            }
            catch (SqlException e)
            {

            }

            List<Asignacion> asignaciones = ConvertirDataTableAListaDeAsignaciones(tablaDeAsignaciones);

            return asignaciones;

        }
        public void GuardarAsignacion(Asignacion asignacion)
		{
            SqlParameter[] parametrosDeAsignacion = InicializarParametrosDeSql(asignacion);

            try
            {
                AccesoADatos.EjecutarInsertInto("INSERT INTO Asignaciones(EstadoAsignacion, FechaDeInicion, MatriculaDeAlumno, IDProyecto, IDSolicitud) VALUES(@EstadoAsignacion, @FechaDeInicio, @MatriculaDeAlumnoAsignacion, @IDProyectoAsignacion, @IDSolicitudAsignacion)", parametrosDeAsignacion);
            }
            catch (SqlException e)
            {

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
            parametrosDeAsignacion[2].Value = asignacion.FechaDeInicio;
            parametrosDeAsignacion[3].ParameterName = "@FechaDeFinalAsignacion";
            parametrosDeAsignacion[3].Value = asignacion.FechaDeFinal;
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
