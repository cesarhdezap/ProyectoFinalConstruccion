using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AlumnoDAO : IAlumnoDAO
	{
		
        public void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno)
		{
            SqlParameter[] parametrosDeAlumno = InicializarParametrosDeSql(alumno);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("UPDATE Alumnos SET Nombre = @NombreAlumno, Estado = @EstadoAlumno, Telefono = @TelefonoAlumno, CorreoElectronico = @CorreoElectronicoAlumno WHERE Matricula = @MatriculaAlumno", parametrosDeAlumno);
            } 
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al actualizar Alumno: " + alumno.ToString() + "Con matricula: " + matricula, e);
            }

            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El alumno con matricula: " + matricula + " no existe.");
            }
        }

        public string CargarMatriculaPorCorreo(string correoElectronico)
        {
            string matricula = string.Empty;
            DataTable tablaDeMatricula = new DataTable();
            SqlParameter[] parametroCorreo = new SqlParameter[1];
            parametroCorreo[0] = new SqlParameter
            {
                ParameterName = "@CorreoElectronico",
                Value = correoElectronico
            };

            try
            {
                tablaDeMatricula = AccesoADatos.EjecutarSelect("SELECT Matricula FROM Alumnos WHERE CorreoElectronico = @CorreoElectronico", parametroCorreo);
            }
            catch (SqlException e)
            {
                Console.WriteLine("No se encontro la matricula del alumno con correo: {0}", correoElectronico);
            }
            return matricula;
        }

        public List<Alumno> CargarAlumnosPorEstado(EstadoAlumno estadoDeAlumno)

		{
			DataTable tablaDeAlumnos = new DataTable();
			SqlParameter[] parametroEstadoAlumno = new SqlParameter[1];
            parametroEstadoAlumno[0] = new SqlParameter
            {
                ParameterName = "@EstadoAlumno",
                Value = (int)estadoDeAlumno
            };

            try
            {
                tablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos WHERE Estado = @EstadoAlumno", parametroEstadoAlumno);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar alumnos con estado: " + estadoDeAlumno.ToString(), e);
            }
			List<Alumno> alumnos = new List<Alumno>();
            try
            {
                alumnos = ConvertirDataTableAListaDeAlumnos(tablaDeAlumnos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a alumno en cargar alumnos con estado: " + estadoDeAlumno.ToString(), e);
            }
            return alumnos;
        }

        public Alumno CargarAlumnoPorMatricula(string matricula)
		{
            DataTable tablaDeAlumno = new DataTable();
			SqlParameter[] parametroMatricula = new SqlParameter[1];
            parametroMatricula[0] = new SqlParameter
            {
                ParameterName = "@Matricula",
                Value = matricula
            };

			try
            {
                tablaDeAlumno = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos WHERE Matricula = @Matricula", parametroMatricula);
            }
			catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Alumno con matricula: " + matricula, e);
            }
            Alumno alumno = new Alumno();
            try
            {
                alumno = ConvertirDataTableAAlumno(tablaDeAlumno);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Alumno en cargar alumno con matricula: " + matricula, e);
            }

            return alumno;
		}

        public List<Alumno> CargarAlumnosTodos()
		{
			DataTable tablaDeAlumnos = new DataTable();
			try
			{
				tablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos");
			}
			catch (SqlException e)
			{
                throw new AccesoADatosException("Error al cargar todos los Alumnos", e);
            }
            List<Alumno> listaDeAlumnos = new List<Alumno>();
            try
            {
                listaDeAlumnos = ConvertirDataTableAListaDeAlumnos(tablaDeAlumnos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Alumnos en cargar todos los Alumnos", e);
            }
            return listaDeAlumnos;
		}

        public Alumno CargarMatriculaPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar matricula Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }
            DataTable tablaDeAlumno = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };

            try
            {
                tablaDeAlumno = AccesoADatos.EjecutarSelect("SELECT Matricula FROM Asignaciones WHERE IDAsignacion = @IDAsignacion", parametroIDAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar matricula con IDAsignacion: " + IDAsignacion, e);
            }
            Alumno alumno = new Alumno();
            try
            {
                alumno = ConvertirDataTableAAlumnoConSoloMatricula(tablaDeAlumno);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Alumno en cargar alumno con IDAsignacion: " + IDAsignacion, e);
            }
            return alumno;
        }

        private Alumno ConvertirDataTableAAlumno(DataTable tablaDeAlumno)
		{
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Alumno alumno = new Alumno();
            foreach (DataRow fila in tablaDeAlumno.Rows)
            {
                alumno.Nombre = fila["Nombre"].ToString();
                alumno.CorreoElectronico = fila["CorreoElectronico"].ToString();
                alumno.Telefono = fila["Telefono"].ToString();
                alumno.Matricula = fila["Matricula"].ToString();
                alumno.Carrera = fila["Carrera"].ToString();
                alumno.Contraseña = fila["Contraseña"].ToString();
                alumno.EstadoAlumno = (EstadoAlumno)fila["Estado"];
                alumno.Asignaciones = asignacionDAO.CargarIDsPorMatriculaDeAlumno(fila["Matricula"].ToString());
            }
            return alumno;
        }

        private Alumno ConvertirDataTableAAlumnoConSoloMatricula(DataTable tablaDeAlumno)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Alumno alumno = new Alumno();
            foreach (DataRow fila in tablaDeAlumno.Rows)
            {
                alumno.Matricula = fila["Matricula"].ToString();
            }
            return alumno;
        }

        private List<Alumno> ConvertirDataTableAListaDeAlumnos(DataTable tablaDeAlumnos)
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
            List<Alumno> listaDeAlumnos = new List<Alumno>();
            foreach (DataRow fila in tablaDeAlumnos.Rows)
            {
                Alumno alumno = new Alumno
                {
                    Nombre = fila["Nombre"].ToString(),
                    CorreoElectronico = fila["CorreoElectronico"].ToString(),
                    Telefono = fila["Telefono"].ToString(),
                    Matricula = fila["Matricula"].ToString(),
                    Carrera = fila["Carrera"].ToString(),
                    Contraseña = fila["Contraseña"].ToString(),
                    EstadoAlumno = (EstadoAlumno)fila["Estado"],
                    Asignaciones = asignacionDAO.CargarIDsPorMatriculaDeAlumno(fila["Matricula"].ToString())
                };
                listaDeAlumnos.Add(alumno);
            }
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
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar Alumno:" + alumno.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Alumno: " + alumno.ToString() + "no fue guardado.");
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
            parametrosDeAlumno[6].Value = (int)alumno.EstadoAlumno;

            return parametrosDeAlumno;
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}
