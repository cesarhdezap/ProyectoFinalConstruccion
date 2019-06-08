using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeAlumnos
	{
		public List<Alumno> Alumnos { get; set; }

		public void CrearAlumno(Alumno alumno)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			alumnoDAO.GuardarAlumno(alumno);
			this.Alumnos.Add(alumno);
		}

		public void CargarAlumnosTodos()
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			this.Alumnos = alumnoDAO.CargarAlumnosTodos();
		}

		public void CargarAlumnosPorEstado(EstadoAlumno estadoAlumno)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			this.Alumnos = alumnoDAO.CargarAlumnosPorEstado(estadoAlumno);
		}
        public void CargarAlumnosPorCarrera(string carrera)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            this.Alumnos = alumnoDAO.CargarAlumnosPorCarrera(carrera);
        }
	}
}
