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
			//TODO
			throw new NotImplementedException();
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

		private DataTable ConvertirAsignacionADataTable(Asignacion asignación)
        {
            //TODO
            throw new NotImplementedException();
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
            //TODO
            throw new NotImplementedException();
        }
        public int GuardarAsignacion(Asignacion asignacion)
		{
			//TODO
			throw new NotImplementedException();
		}
    }
}
