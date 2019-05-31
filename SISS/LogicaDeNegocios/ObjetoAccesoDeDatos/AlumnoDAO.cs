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
            SqlParameter[] parametrosDeAlumno = InicializarParametrosDeSql(alumno);
            try
            {
                AccesoADatos.EjecutarInsertInto("UPDATE Alumnos SET Nombre = @NombreAlumno, Estado = @EstadoAlumno, Telefono = @TelefonoAlumno, CorreoElectronico = @CorreoElectronicoAlumno WHERE Matricula = @MatriculaAlumno", parametrosDeAlumno);
            } catch (SqlException e)
            {
                //Definir tipo de excepcion 
                //throw new ??????(e);
            }
        }

        public List<Alumno> CargarAlumnosPorEstado(EstadoAlumno estadoAlumno)
		{
			DataTable tablaDeAlumnos = new DataTable();
			SqlParameter[] parametroEstadoAlumno = new SqlParameter[1];
			parametroEstadoAlumno[0] = new SqlParameter();
			parametroEstadoAlumno[0].ParameterName = "@EstadoAlumno";
			parametroEstadoAlumno[0].Value = estadoAlumno;

            try
            {
                tablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos WHERE Estado = @EstadoAlumno", parametroEstadoAlumno);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

			List<Alumno> alumnos = new List<Alumno>();

			alumnos = ConvertirDataTableAListaDeAlumnos(tablaDeAlumnos);

            return alumnos;
        }

        public Alumno CargarAlumnoPorMatricula(string matricula)
		{
            DataTable tablaDeAlumnos = new DataTable();
			SqlParameter[] parametroMatricula = new SqlParameter[1];
			parametroMatricula[0] = new SqlParameter();
			parametroMatricula[0].ParameterName = "@matricula";
			parametroMatricula[0].Value = matricula;

			try
            {
                tablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos WHERE Matricula = @matricula", parametroMatricula);
            }
			catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

			Alumno alumno = ConvertirDataTableAAlumno(tablaDeAlumnos);

            return alumno;
		}

        public List<Alumno> CargarAlumnosTodos()
		{
			DataTable tablaDeAlumnos = new DataTable();
			try
			{
				tablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos");
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
            DataTable tablaDeAlumno = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter();
            parametroIDAsignacion[0].ParameterName = "@IDAsignacion";
            parametroIDAsignacion[0].Value = IDAsignacion;
                
            try
            {
                tablaDeAlumno = AccesoADatos.EjecutarSelect("SELECT Matricula FROM Asignaciones WHERE IDAsignacion = @IDAsignacion", parametroIDAsignacion);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            Alumno alumno = ConvertirDataTableAAlumno(tablaDeAlumno);

            return alumno;
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
            SqlParameter[] parametrosDeAlumno = InicializarParametrosDeSql(alumno);

            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Alumnos(Matricula, Nombre, Carrera, Estado, Telefono, CorreoElectronico, Contraseña) VALUES (@MatriculaAlumno, @NombreAlumno, @CarreraAlumno, @EstadoAlumno, @TelefonoAlumno, @CorreoElectronicoAlumno, @ContraseñaAlumno)", parametrosDeAlumno);
            }
            catch (SqlException)
            {
                throw new NotImplementedException("No se ha implementado excepcion personalizara, AlumnoDAO.GuardarAlumno");
            }
            if (filasAfectadas <= 0)
            {
                throw new MissingFieldException("Error en AlumnoDAO.GuardarAlumno");
            }
        }
         
        private SqlParameter[] InicializarParametrosDeSql(Alumno alumno)
        { 
            SqlParameter[] parametrosDeAlumno = new SqlParameter[7];

            for (int i = 0; i < parametrosDeAlumno.Length; i++)
            {
                parametrosDeAlumno[i] = new SqlParameter();
            }

            parametrosDeAlumno[0].ParameterName = "@NombreAlumno";
            parametrosDeAlumno[0].Value = alumno.Nombre;
            parametrosDeAlumno[1].ParameterName = "@CorreoElectronicoAlumno";
            parametrosDeAlumno[1].Value = alumno.CorreoElectronico;
            parametrosDeAlumno[2].ParameterName = "@TelefonoAlumno";
            parametrosDeAlumno[2].Value = alumno.Telefono;
            parametrosDeAlumno[3].ParameterName = "@MatriculaAlumno";
            parametrosDeAlumno[3].Value = alumno.Matricula;
            parametrosDeAlumno[4].ParameterName = "@CarreraAlumno";
            parametrosDeAlumno[4].Value = alumno.Carrera;
            parametrosDeAlumno[5].ParameterName = "@ContraseñaAlumno";
            parametrosDeAlumno[5].Value = alumno.Contraseña;
            parametrosDeAlumno[6].ParameterName = "@EstadoAlumno";
            parametrosDeAlumno[6].Value = alumno.EstadoAlumno.ToString();

            return parametrosDeAlumno;
        }
    }
}
