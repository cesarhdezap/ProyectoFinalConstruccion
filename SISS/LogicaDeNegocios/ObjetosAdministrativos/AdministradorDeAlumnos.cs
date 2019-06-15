using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeAlumnos
	{
		public List<Alumno> Alumnos { get; set; }


		public void CargarAlumnosTodos()
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumnos = alumnoDAO.CargarAlumnosTodos();
		}

		public void CargarAlumnosPorEstado(EstadoAlumno estadoAlumno)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumnos = alumnoDAO.CargarAlumnosPorEstado(estadoAlumno);
		}
        public void CargarAlumnosPorCarrera(string carrera)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumnos = alumnoDAO.CargarAlumnosPorCarrera(carrera);
        }
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
