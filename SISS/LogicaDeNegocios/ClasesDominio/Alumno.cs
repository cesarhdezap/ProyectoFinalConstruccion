using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios
{
    /// <summary>
    /// Clase Alumno.
    /// Contiene todos los métodos para realizar operaciones con la base de datos.
    /// <para>Hereda de <see cref="Persona"/>.</para>
    /// </summary>
	public class Alumno : Persona
	{
		private const int MAXIMO_DE_ASIGNACIONES = 2;
		public string Matricula { get; set; }
		public string Carrera { get; set; }
		public string Contraseña { get; set; }
		public EstadoAlumno EstadoAlumno { get; set; }
		public Asignacion Asignacion { get; set; }	

        /// <summary>
        /// Inicializa el Alumno con la Matricula vacía.
        /// </summary>
        public Alumno ()
        {
            Matricula = string.Empty;
        }

        /// <summary>
        /// Guarda el <see cref="Alumno"/> en la base de datos.
        /// </summary>
        /// <exception cref="Excepciones.AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
        public void Guardar()
        {
			string contraseñaEncriptada = Servicios.ServiciosDeAutenticacion.EncriptarContraseña(Contraseña);
			Contraseña = contraseñaEncriptada;
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            alumnoDAO.GuardarAlumno(this);
        }

        /// <summary>
        /// Carga la <see cref="Asignacion"/> del Alumno por su Matricula.
        /// </summary>
        /// <exception cref="Excepciones.AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public Asignacion CargarAsignacion()
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			Asignacion asignacion = new Asignacion();
			asignacion = asignacionDAO.CargarIDPorMatriculaDeAlumno(Matricula);
			asignacion = asignacionDAO.CargarAsignacionPorID(asignacion.IDAsignacion);
			asignacion.Alumno = this;
			return asignacion;
		}

        /// <summary>
        /// Valida si los atributos del <see cref="Alumno"/> son correctos para la inserción
        /// a base de datos.
        /// </summary>
        /// <returns>Si los atributos del Alumno son válidos.</returns>
        public bool Validar()
        {
            bool resultadoDeValidacion = false;

            if (ValidarContraseña(Contraseña) 
                && ValidarCorreoElectronico(CorreoElectronico) 
                && ValidarMatricula(Matricula) 
                && ValidarNombre(Nombre) 
                && ValidarTelefono(Telefono)
				&& ValidarDisponibilidadDeCorreo(CorreoElectronico)
				&& ValidarDisponibilidadDeMatricula(Matricula))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }
        
        /// <summary>
        /// Cambia el <see cref="EstadoAlumno"/> a <see cref="EstadoAlumno.DadoDeBaja"/>
        /// y Actualiza el <see cref="Alumno"/> en la base de datos.
        /// </summary>
		public void DarDeBaja()
		{
			EstadoAlumno = EstadoAlumno.DadoDeBaja;
            ActualizarRegistroDeAlumno();
        }

        /// <summary>
        /// Cambia el <see cref="EstadoAlumno"/> a <see cref="EstadoAlumno.Aceptado"/>
        /// y Actualiza el <see cref="Alumno"/> en la base de datos.
        /// </summary>
		public void Aceptar()
		{
			EstadoAlumno = EstadoAlumno.Aceptado;
            ActualizarRegistroDeAlumno();
        }

        /// <summary>
        /// Cambia el <see cref="EstadoAlumno"/> a <see cref="EstadoAlumno.Rechazado"/>
        /// y Actualiza el <see cref="Alumno"/> en la base de datos.
        /// </summary>
		public void Rechazar()
		{
			EstadoAlumno = EstadoAlumno.Rechazado;
            ActualizarRegistroDeAlumno();
        }

        /// <summary>
        /// Cambia el <see cref="EstadoAlumno"/> a <see cref="EstadoAlumno.EsperandoAsignacion"/>
        /// y Actualiza el <see cref="Alumno"/> en la base de datos.
        /// </summary>
        public void Solicitar()
        {
            EstadoAlumno = EstadoAlumno.EsperandoAsignacion;
            ActualizarRegistroDeAlumno();
        }

        /// <summary>
        /// Convierte los atributos del <see cref="Alumno"/>
        /// a una cadena con espacios para debugging.
        /// </summary>
        /// <returns>Cadena con los datos de los atributos.</returns>
        public override string ToString()
        {  
            string alumno = System.Environment.NewLine +
                            "Matricula: " + Matricula + System.Environment.NewLine +
                            "Nombre: " + Nombre + System.Environment.NewLine +
                            "Correo Electronico: " + CorreoElectronico + System.Environment.NewLine +
                            "Telefono: " + Telefono + System.Environment.NewLine +
                            "Estado: " + EstadoAlumno.ToString() + System.Environment.NewLine +
                            "Carrera" + Carrera;
            return alumno;
        }

        /// <summary>
        /// Actualiza los datos del Alumno en la base de datos con los
        /// atributos de la clase.
        /// </summary>
        /// <exception cref="Excepciones.AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
        private void ActualizarRegistroDeAlumno()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            alumnoDAO.ActualizarAlumnoPorMatricula(Matricula, this);
        }

        /// <summary>
        /// Cambia el <see cref="EstadoAlumno"/> a <see cref="EstadoAlumno.Asignado"/>
        /// y Actualiza el <see cref="Alumno"/> en la base de datos.
        /// </summary>
		public void Asignar()
		{
			EstadoAlumno = EstadoAlumno.Asignado;
			ActualizarRegistroDeAlumno();
		}
	}

    /// <summary>
    /// Enumerador con los estados por los que pasa el Alumno.
    /// </summary>
	public enum EstadoAlumno
	{
		EsperandoAceptacion, 
        Aceptado,
        Rechazado,
        EsperandoAsignacion,
        Asignado,
		Liberado,
		DadoDeBaja,
		
	}
}
