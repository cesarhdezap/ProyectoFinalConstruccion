using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DataBaseAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocios.Interfaces;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class AlumnoDAO : Interfaces.IAlumnoDAO
	{
		public void GuardarAlumno(Alumno alumno)
		{
			throw new NotImplementedException();
		}

        

		public List<Alumno> CargarAlumnosTodos()
		{
			DataTable TablaDeAlumnos = new DataTable();
			try
			{
				TablaDeAlumnos = AccesoADatos.ExecuteSelect("SELECT * FROM Alumno");
			}
            catch (SqlException ExcepcionSQL)
			{
				Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
			}

			List<Alumno> ListaAlumnos = new List<Alumno>();
            IAsignacionDAO InstanciaAsignacion = new AsignacionDAO();
            ListaAlumnos = (from DataRow fila in TablaDeAlumnos.Rows
                            select new Alumno()
                            {
                                Matricula = fila["matricula"].ToString(),
                                Carrera = fila["carrera"].ToString(),
                                Contraseña = fila["contraseña"].ToString(),
                                EstadoAlumno = (EEstadoAlumno)fila["estadoAlumno"],
                                Asignaciones = InstanciaAsignacion.CargarAsignacionPorID((int)fila["IDAsignacion"].ToString),

                            }
                           ).ToList();

			return ListaAlumnos;
		}

		public Alumno CargarAlumnoPorMatricula(string matricula)
		{
            DataTable TablaDeAlumnos = new DataTable();

            try
            {
                TablaDeAlumnos = AccesoADatos.ExecuteSelect("SELECT * FROM Alumnos WHERE matricula = @matricula");
            }catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            IAsignacionDAO InstanciaAsignacion = new AsignacionDAO();
            Alumno alumno = (from DataRow fila in TablaDeAlumnos.Rows
                             select new Alumno()
                            {
                                
                                Matricula = fila["matricula"].ToString(),
                                Carrera = fila["carrera"].ToString(),
                                Contraseña = fila["contraseña"].ToString(),
                                EstadoAlumno = (EEstadoAlumno)fila["estadoAlumno"],
                                Asignaciones = InstanciaAsignacion.CargarAsignacionPorID((int)fila["IDAsignacion"]),

                            }
                           ).ToList();

            return alumno;

		}

		public List<Alumno> CargarAlumnosPorEstado(EEstadoAlumno estadoAlumno)
		{
            DataTable TablaDeAlumnos = new DataTable();

            try
            {
                TablaDeAlumnos = AccesoADatos.ExecuteSelect("SELECT * FROM Alumnos WHERE estado = @estado", estadoAlumno.ToString();
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            List<Alumno> alumno = (from DataRow fila in TablaDeAlumnos.Rows
                             select new Alumno()
                             {
                                 Matricula = fila["matricula"].ToString(),
                                 Carrera = fila["carrera"].ToString(),
                                 Contraseña = fila["contraseña"].ToString(),
                                 EstadoAlumno = (EEstadoAlumno)fila["estadoAlumno"],
                                 Asignaciones = AsignacionDAO.CargarAsignacionPorID(fila["IDAsignacion"].ToString),

                             }
                           ).ToList();

            return alumno;
        }

		public void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno)
		{
			throw new NotImplementedException();
		}

		public DataTable ConvertirAlumnoADataTable(Alumno alumno)
		{
			throw new NotImplementedException();
		}

        public List<Alumno> CargarAlumnosPorCorreo(string correo)
        {
            throw new NotImplementedException();
        }
    }
}
