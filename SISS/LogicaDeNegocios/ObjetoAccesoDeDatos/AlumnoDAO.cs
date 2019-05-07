using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AlumnoDAO : IAlumnoDAO
	{
		public void GuardarAlumno(Alumno alumno)
		{
			//TODO
			throw new NotImplementedException();
		}

		public List<Alumno> CargarAlumnosTodos()
		{
			DataTable TablaDeAlumnos = new DataTable();
			try
			{
				TablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumno");
			}
			catch (SqlException ExcepcionSQL)
			{
				Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
			}

			AsignacionDAO asignacionDAO = new AsignacionDAO();
			List<Alumno> ListaAlumnos = new List<Alumno>(); 

			ListaAlumnos = (from DataRow fila in TablaDeAlumnos.Rows
							select new Alumno()
							{
								Matricula = fila["matricula"].ToString(),
								Carrera = fila["carrera"].ToString(),
								Contraseña = fila["contraseña"].ToString(),
								EstadoAlumno = (EEstadoAlumno)fila["estadoAlumno"],
								Asignaciones = asignacionDAO.CargarIDsPorMatriculaDeAlumno(fila["matricula"].ToString()),
                            }
                           ).ToList();

			return ListaAlumnos;
		}

		public Alumno CargarAlumnoPorMatricula(string matricula)
		{
            DataTable TablaDeAlumnos = new DataTable();
			SqlParameter[] parametroMatricula = new SqlParameter[1];
			parametroMatricula[0] = new SqlParameter();
			parametroMatricula[0].ParameterName = "@matricula";
			parametroMatricula[0].Value = matricula;

			try
            {
                TablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos WHERE matricula = @matricula", parametroMatricula);
            }catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

			AsignacionDAO asignacionDAO = new AsignacionDAO();

            Alumno alumno = (Alumno)(from DataRow fila in TablaDeAlumnos.Rows
                select new Alumno()
                {
                    Matricula = fila["matricula"].ToString(),
                    Nombre = fila["nombre"].ToString(),
                    Carrera = fila["carrera"].ToString(),
                    EstadoAlumno = (EEstadoAlumno)fila["estadoAlumno"],
                    Telefono = fila["telefono"].ToString(),
                    CorreoElectronico = fila["correo Electronico"].ToString(),
                    Asignaciones = asignacionDAO.CargarIDsPorMatriculaDeAlumno(fila["matricula"].ToString())

                }
             );

            return alumno;
		}

		public List<Alumno> CargarAlumnosPorEstado(EEstadoAlumno estadoAlumno)
		{
			DataTable TablaDeAlumnos = new DataTable();
			SqlParameter[] parametroEstadoAlumno = new SqlParameter[1];
			parametroEstadoAlumno[0] = new SqlParameter();
			parametroEstadoAlumno[0].ParameterName = "@estadoAlumno";
			parametroEstadoAlumno[0].Value = estadoAlumno;

            try
            {
                TablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos WHERE estado = @estadoAlumno", parametroEstadoAlumno);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

			AsignacionDAO asignacionDAO = new AsignacionDAO();
			List<Alumno> alumnos = new List<Alumno>();

			alumnos = (from DataRow fila in TablaDeAlumnos.Rows
                             select new Alumno()
                             {
                                 Matricula = fila["matricula"].ToString(),
                                 Carrera = fila["carrera"].ToString(),
                                 Contraseña = fila["contraseña"].ToString(),
                                 EstadoAlumno = (EEstadoAlumno)fila["estadoAlumno"],
                                 Asignaciones = asignacionDAO.CargarIDsPorMatriculaDeAlumno(fila["matricula"].ToString()),

                             }
                           ).ToList();

            return alumnos;
        }

		public void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno)
		{
			//TODO
			throw new NotImplementedException();
		}

		private DataTable ConvertirAlumnoADataTable(Alumno alumno)
		{
			//TODO
			throw new NotImplementedException();
		}

        public List<Alumno> CargarAlumnosPorCorreo(string correo)
        {
			//TODO
			throw new NotImplementedException();
		}
    }
}
