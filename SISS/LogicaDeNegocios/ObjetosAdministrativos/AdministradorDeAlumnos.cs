using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrativos
{
	class AdministradorDeAlumnos
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

		public void CargarAlumnosPorEstado(EEstadoAlumno estadoAlumno)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			this.Alumnos = alumnoDAO.CargarAlumnosPorEstado(estadoAlumno);
		}
	}
}
