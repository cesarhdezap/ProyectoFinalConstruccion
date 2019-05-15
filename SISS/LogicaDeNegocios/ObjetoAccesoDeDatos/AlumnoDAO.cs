using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

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

			ListaAlumnos = ConvertirDataTableAListaDeAlumnos(TablaDeAlumnos);

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
            }
			catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

			AsignacionDAO asignacionDAO = new AsignacionDAO();

			Alumno alumno = ConvertirDataTableAAlumno(TablaDeAlumnos);

            return alumno;
		}

		public List<Alumno> CargarAlumnosPorEstado(EstadoAlumno estadoAlumno)
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

			alumnos = ConvertirDataTableAListaDeAlumnos(TablaDeAlumnos);

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

		private Alumno ConvertirDataTableAAlumno(DataTable DataTableAlumnos)
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
            Alumno alumno = new Alumno();
            DataRow filaAlumno;
			if (DataTableAlumnos.Rows.Count > 0)
            {
                filaAlumno = DataTableAlumnos.Rows[0];
                alumno.Matricula = filaAlumno.ItemArray[0].ToString();
                alumno.Nombre = filaAlumno.ItemArray[1].ToString();
                alumno.Carrera = filaAlumno.ItemArray[2].ToString();
                //TODO Campos adicionales
            }
            return alumno;
		}

		private List<Alumno> ConvertirDataTableAListaDeAlumnos(DataTable DataTableAlumnos)
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			List<Alumno> listaDeAlumnos = (from DataRow fila in DataTableAlumnos.Rows
                             select new Alumno()
                             {
                                 Matricula = fila["matricula"].ToString(),
                                 Carrera = fila["carrera"].ToString(),
                                 Contraseña = fila["contraseña"].ToString(),
                                 EstadoAlumno = (EstadoAlumno)fila["estadoAlumno"],
                                 Asignaciones = asignacionDAO.CargarIDsPorMatriculaDeAlumno(fila["matricula"].ToString()),

                             }
                           ).ToList();
			return listaDeAlumnos;
		}

        public List<Alumno> CargarAlumnosPorCorreo(string correo)
        {
			//TODO
			throw new NotImplementedException();
		}


    }
}
