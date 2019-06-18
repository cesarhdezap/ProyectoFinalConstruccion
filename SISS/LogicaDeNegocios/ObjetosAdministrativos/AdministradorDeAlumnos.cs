using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    /// <summary>
    /// Clase para administrar Alumnos en una lista.
    /// Contiene un método para cargar los alumnos por carrera y estado.
    /// </summary>
	public class AdministradorDeAlumnos
	{
		public List<Alumno> Alumnos { get; set; }

        /// <summary>
        /// Carga los alumnos a la lista de <see cref="Alumnos"/> del objeto por carrera y estado.
        /// </summary>
        /// <param name="carrera">Carrera del Alumno.</param>
        /// <param name="estadoAlumno">Estado del Alumno.</param>
		public void CargarAlumnosPorCarreraYEstado(string carrera, EstadoAlumno estadoAlumno)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumnos = alumnoDAO.CargarAlumnosPorCarrera(carrera);

			List<Alumno> alumnosFiltrados = new List<Alumno>();
			alumnosFiltrados = Alumnos.FindAll(alumno => alumno.EstadoAlumno == estadoAlumno);
            Alumnos = null;
            Alumnos = alumnosFiltrados;
		}
	}
}
