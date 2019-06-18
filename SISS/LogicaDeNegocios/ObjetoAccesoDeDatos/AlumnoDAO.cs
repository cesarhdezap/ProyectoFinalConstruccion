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
	/// <summary>
	/// Clase de abstraccion para acceso a objetos <see cref="Alumno"/> en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos <see cref="Alumno"/>.
	/// </summary>
	public class AlumnoDAO : IAlumnoDAO
	{
		/// <summary>
		/// Actualiza un <see cref="Alumno"/> dada su <see cref="Alumno.Matricula"/>.
		/// </summary>
		/// <param name="matricula"><see cref="Alumno.Matricula"/> del <see cref="Alumno"/> a actualizar.</param>
		/// <param name="alumno">El <see cref="Alumno"/> a actualizar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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

		/// <summary>
		/// Carga un <see cref="Alumno"/> con solo <see cref="Alumno.Matricula"/> inicializado dado su correo electrónico.
		/// </summary>
		/// <param name="correoElectronico">El correo electrónico del <see cref="Alumno"/> a cargar.</param>
		/// <returns>Un <see cref="Alumno"/> con solo <see cref="Alumno.Matricula"/> inicializado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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

		/// <summary>
		/// Carga una <see cref="List{Alumno}"/> de todos los <see cref="Alumno"/> que tengan el <see cref="EstadoAlumno"/> dado.
		/// </summary>
		/// <param name="estadoDeAlumno">El <see cref="EstadoAlumno"/> de los <see cref="Alumno"/> a cargar.</param>
		/// <returns>Una <see cref="List{Alumno}"/> de todos los <see cref="Alumno"/> con el <see cref="EstadoAlumno"/> dado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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

		/// <summary>
		/// Carga una <see cref="List{Alumno}"/> de todos los <see cref="Alumno"/> que tengan la <see cref="Alumno.Carrera"/> dada.
		/// </summary>
		/// <param name="carrera"><see cref="Alumno.Carrera"/> de los <see cref="Alumno"/> a cargar.</param>
		/// <returns>Una <see cref="List{Alumno}"/> de todos los <see cref="Alumno"/> con la <see cref="Alumno.Carrera"/> dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeAlumnos = AccesoADatos.EjecutarSelect(QuerysDeAlumno.CARGAR_ALUMNOS_POR_CARRERA, parametroCarreraAlumno);
            }
            catch (SqlException e)
            {
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, carrera);
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

		/// <summary>
		/// Carga al <see cref="Alumno"/> con la <see cref="Alumno.Matricula"/> dada.
		/// </summary>
		/// <param name="matricula"><see cref="Alumno.Matricula"/> del <see cref="Alumno"/> a cargar.</param>
		/// <returns>El <see cref="Alumno"/> con la <see cref="Alumno.Matricula"/> dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeAlumno = AccesoADatos.EjecutarSelect(QuerysDeAlumno.CARGAR_ALUMNO_POR_MATRICULA, parametroMatricula);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, matricula);
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

		/// <summary>
		/// Carga a todos los <see cref="Alumno"/> en la base de datos.
		/// </summary>
		/// <returns>Una <see cref="List{Alumno}"/> con todos los <see cref="Alumno"/>.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<Alumno> CargarAlumnosTodos()
		{
			DataTable tablaDeAlumnos = new DataTable();
			try
			{
				tablaDeAlumnos = AccesoADatos.EjecutarSelect(QuerysDeAlumno.CARGAR_ALUMNOS_TODOS);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
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

		/// <summary>
		/// Carga un <see cref="Alumno"/> con solo <see cref="Alumno.Matricula"/> inicializado y sus demas atributos como null basado en <see cref="Asignacion.IDAsignacion"/>
		/// </summary>
		/// <param name="IDAsignacion"><see cref="Asignacion.IDAsignacion"/> de la <see cref="Asignacion"/> relacionada a la matrícula a cargar.</param>
		/// <returns>Un <see cref="Alumno"/> con solo <see cref="Alumno.Matricula"/> inicializado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeAlumno = AccesoADatos.EjecutarSelect(QuerysDeAlumno.CARGAR_MATRICULA_POR_IDASIGNACION, parametroIDAsignacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDAsignacion);
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

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a un <see cref="Alumno"/>.
		/// </summary>
		/// <param name="tablaDeAlumno">La <see cref="DataTable"/> que contiene datos del <see cref="Alumno"/><./param>
		/// <returns>El <see cref="Alumno"/> contenido en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a un <see cref="Alumno"/> con solo <see cref="Alumno.Matricula"/> inicializado y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeAlumno">La <see cref="DataTable"/> que contiene datos del <see cref="Alumno"/>.</param>
		/// <returns>El <see cref="Alumno"/> con solo su <see cref="Alumno.Matricula"/> inicializada contenido en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{Alumno}"/>.
		/// </summary>
		/// <param name="tablaDeAlumnos">La <see cref="DataTable"/> que contiene datos de los <see cref="Alumno"/>.</param>
		/// <returns>La <see cref="List{Alumno}"/> de <see cref="Alumno"/> contenido en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Guarda un <see cref="DataTable"/> en la base de datos.
		/// </summary>
		/// <param name="alumno">El <see cref="DataTable"/> a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void GuardarAlumno(Alumno alumno)
        {
            SqlParameter[] parametrosDeAlumno = InicializarParametrosDeSql(alumno);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeAlumno.GUARDAR_ALUMNO, parametrosDeAlumno);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, alumno);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Alumno: " + alumno.ToString() + "no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

		/// <summary>
		/// Inicializa un arreglo de <see cref="SqlParameter"/> basado en un <see cref="DataTable"/>.
		/// </summary>
		/// <param name="alumno">El <see cref="Alumno"/> para inicializar los parametros.</param>
		/// <returns>Un arreglo de <see cref="SqlParameter"/> donde cada posición es uno de los atributos del <see cref="Alumno"/>.</returns>
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
