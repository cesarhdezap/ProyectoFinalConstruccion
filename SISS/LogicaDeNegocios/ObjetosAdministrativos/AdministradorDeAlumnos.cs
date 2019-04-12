using System;
using System.Collections.Generic;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
