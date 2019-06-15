using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Querys;


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
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeAlumno.ACTUALIZAR_ALUMNO, parametrosDeAlumno);
            } 
            catch (SqlException e)
            {
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, alumno);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El alumno con matricula: " + matricula + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
            }
        }

        public string CargarMatriculaPorCorreoElectronico(string correoElectronico)
        {
            
            DataTable tablaDeMatricula = new DataTable();
            SqlParameter[] parametroCorreo = new SqlParameter[1];
            parametroCorreo[0] = new SqlParameter
            {
                ParameterName = "@CorreoElectronico",
                Value = correoElectronico
            };

            try
            {
                tablaDeMatricula = AccesoADatos.EjecutarSelect(QuerysDeAlumno.CARGAR_MATRICULA_ALUMNO, parametroCorreo);
            }
            catch (SqlException e)
            {
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, correoElectronico);
			}
            string matricula;
            try
            {
                matricula = ConvertirDataTableAAlumnoConSoloMatricula(tablaDeMatricula).Matricula;
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Alumno en cargar Matricula por CorreoElectronico: " + correoElectronico, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
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
                tablaDeAlumnos = AccesoADatos.EjecutarSelect(QuerysDeAlumno.CARGAR_MATRICULA_ALUMNO, parametroEstadoAlumno);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, estadoDeAlumno);
			}

			List<Alumno> alumnos = new List<Alumno>();
            try
            {
                alumnos = ConvertirDataTableAListaDeAlumnos(tablaDeAlumnos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a alumno en cargar alumnos con estado: " + estadoDeAlumno.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return alumnos;
        }

        public List<Alumno> CargarAlumnosPorCarrera(string carrera)

        {
            DataTable tablaDeAlumnos = new DataTable();
            SqlParameter[] parametroCarreraAlumno = new SqlParameter[1];
            parametroCarreraAlumno[0] = new SqlParameter
            {
                ParameterName = "@Carrera",
                Value = carrera
            };

            try
            {
                tablaDeAlumnos = AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos WHERE Carrera = @Carrera", parametroCarreraAlumno);
            }
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado)
            {
                throw new AccesoADatosException("Error al cargar alumnos con Carrera: " + carrera, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar alumnos con Carrera: " + carrera, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
            }

            List<Alumno> listaDeAlumnos = new List<Alumno>();
            try
            {
                listaDeAlumnos = ConvertirDataTableAListaDeAlumnos(tablaDeAlumnos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a alumno en cargar alumnos con estado: " + carrera, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return listaDeAlumnos;
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
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado)
            {
                throw new AccesoADatosException("Error al cargar Alumno con matricula: " + matricula, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Alumno con matricula: " + matricula, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }

            Alumno alumno = new Alumno();
            try
            {
                alumno = ConvertirDataTableAAlumno(tablaDeAlumno);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Alumno en cargar alumno con matricula: " + matricula, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
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
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado)
            {
                throw new AccesoADatosException("Error al cargar todos los Alumnos", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
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
                throw new AccesoADatosException("Error al convertir datatable a lista de Alumnos en cargar todos los Alumnos", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return listaDeAlumnos;
		}

        public Alumno CargarMatriculaPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar matricula Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
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
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado)
            {
                throw new AccesoADatosException("Error al cargar matricula con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar matricula con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
            }

            Alumno alumno = new Alumno();
            try
            {
                alumno = ConvertirDataTableAAlumnoConSoloMatricula(tablaDeAlumno);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Alumno en cargar alumno con IDAsignacion: " + IDAsignacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
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
                alumno.Asignacion = asignacionDAO.CargarIDPorMatriculaDeAlumno(fila["Matricula"].ToString());
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
                    Asignacion = asignacionDAO.CargarIDPorMatriculaDeAlumno(fila["Matricula"].ToString())
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
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada)
            {
                throw new AccesoADatosException("No se puede guardar el alumno: " + alumno.ToString() + " porque la matricula: " + alumno.Matricula + " ya existe." , e, TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada);
            }
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ServidorNoEncontrado)
            {
                throw new AccesoADatosException("No se puede guardar el alumno: " + alumno.ToString() + " porque no se pudo establecer conexion al servidor.", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
            catch (SqlException e)
            { 
                throw new AccesoADatosException(e.Number.ToString() + "Error al guardar Alumno:" + alumno.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Alumno: " + alumno.ToString() + "no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
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
    }
}
