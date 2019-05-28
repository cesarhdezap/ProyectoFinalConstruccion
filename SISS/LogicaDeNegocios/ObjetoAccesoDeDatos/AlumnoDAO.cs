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
		
        public void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno)
		{
			//TODO
			throw new NotImplementedException();
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

			List<Alumno> alumnos = new List<Alumno>();

			alumnos = ConvertirDataTableAListaDeAlumnos(TablaDeAlumnos);

            return alumnos;
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

			Alumno alumno = ConvertirDataTableAAlumno(TablaDeAlumnos);

            return alumno;
		}

        public List<Alumno> CargarAlumnosTodos()
		{
			DataTable tablaDeAlumnos = new DataTable();
			try
			{
				tablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumno");
			}
			catch (SqlException ExcepcionSQL)
			{
				Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
			}
			List<Alumno> listaAlumnos = new List<Alumno>();

			listaAlumnos = ConvertirDataTableAListaDeAlumnos(tablaDeAlumnos);

			return listaAlumnos;
		}

        public Alumno CargarMatriculaPorIDAsignacion(int IDAsignacion)
        {
            //TODO
            throw new NotImplementedException();    
        }

        private DataTable ConvertirAlumnoADataTable(Alumno alumno)
		{
			//TODO
			throw new NotImplementedException();
		}

        private Alumno ConvertirDataTableAAlumno(DataTable tablaDeAlumno)
		{
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Alumno alumno = (Alumno)(from DataRow fila in tablaDeAlumno.Rows
                             select new Alumno()
                             {
                                 Matricula = fila["matricula"].ToString(),
                                 Carrera = fila["carrera"].ToString(),
                                 Contraseña = fila["contraseña"].ToString(),
                                 EstadoAlumno = (EstadoAlumno)fila["estadoAlumno"],
                                 Asignaciones = asignacionDAO.CargarIDsPorMatriculaDeAlumno(fila["matricula"].ToString())
                             }
                           );
            return alumno;
        }

		private List<Alumno> ConvertirDataTableAListaDeAlumnos(DataTable tablaDeAlumnos)
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			List<Alumno> listaDeAlumnos = (from DataRow fila in tablaDeAlumnos.Rows
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

        public void GuardarAlumno(Alumno alumno)
        {
            SqlParameter[] parametros = new SqlParameter[7];

            for (int i = 0; i < 7; i++) {
                parametros[i] = new SqlParameter();
            }

            parametros[0].ParameterName = "@NombreAlumno";
            parametros[0].Value = alumno.Nombre;
            parametros[1].ParameterName = "@CorreoElectronicoAlumno";
            parametros[1].Value = alumno.CorreoElectronico;
            parametros[2].ParameterName = "@TelefonoAlumno";
            parametros[2].Value = alumno.Telefono;
            parametros[3].ParameterName = "@MatriculaAlumno";
            parametros[3].Value = alumno.Matricula;
            parametros[4].ParameterName = "@CarreraAlumno";
            parametros[4].Value = alumno.Carrera;
            parametros[5].ParameterName = "@ContraseñaAlumno";
            parametros[5].Value = alumno.Contraseña;
            parametros[6].ParameterName = "@EstadoAlumno";
            parametros[6].Value = alumno.EstadoAlumno.ToString();

            int filas = 0;
            try
            {
                filas = AccesoADatos.EjecutarInsertInto("INSERT INTO Alumnos(Matricula, Nombre, Carrera, Estado, Telefono, CorreoElectronico, Contraseña) VALUES (@MatriculaAlumno, @NombreAlumno, @CarreraAlumno, @EstadoAlumno, @TelefonoAlumno, @CorreoElectronicoAlumno, @ContraseñaAlumno)", parametros);
            }
            catch (SqlException e)
            {
                throw new NotImplementedException("No se ha implementado excepcion personalizara, AlumnoDAO.GuardarAlumno");
            }
            if (filas <= 0)
            {
                throw new MissingFieldException("Error en AlumnoDAO.GuardarAlumno");
            }
        }
    }
}
