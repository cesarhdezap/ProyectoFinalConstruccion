using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DataBaseAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
			DataTable tablaDeAlumnos = new DataTable();
			try
			{
				tablaDeAlumnos = DataAccess.ExecuteSelect("SELECT * FROM Alumno");
			} catch(SqlException sqlException)
			{
				Console.Write(" \nException: " + sqlException.StackTrace.ToString());
			}
			List<Alumno> alumnos = new List<Alumno>();
			alumnos = (from DataRow fila in tablaDeAlumnos.Rows
					   select new Alumno()
					   {
						   matricula = fila["matricula"].ToString(),
						   carrera = fila["carrera"].ToString(),
						   contraseña = fila["contraseña"].ToString(),
						   estadoAlumno = (EestadoAlumno)fila["estadoAlumno"],
						   asignaciones = 

					   }).ToList();
			return alumnos;
		}

		public Alumno CargarAlumnoPorMatricula(string matricula)
		{
			throw new NotImplementedException();
		}

		public List<Alumno> CargarAlumnosPorEstado(EestadoAlumno estadoAlumno)
		{
			throw new NotImplementedException();
		}

		public void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno)
		{
			throw new NotImplementedException();
		}

		public DataTable ConvertirAlumnoADataTable(Alumno alumno)
		{
			throw new NotImplementedException();
		}

		
	}
}
