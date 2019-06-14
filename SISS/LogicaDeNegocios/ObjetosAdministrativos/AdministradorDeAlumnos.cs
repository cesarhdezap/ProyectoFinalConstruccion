using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeAlumnos
	{
		public List<Alumno> Alumnos { get; set; }

		public bool ValidarExistencia(Alumno alumno)
		{
            CargarAlumnosTodos();
            bool esxistencia = false;
            bool resultadoDeExistenciaDeMatricula = !Alumnos.Exists(alumnoActual => alumnoActual.Matricula == alumno.Matricula);
            if (resultadoDeExistenciaDeMatricula && ValidarExistenciaDeCorreo(alumno.CorreoElectronico))
            {
                esxistencia = true;
            }

            return esxistencia;
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
		public void CargarAlumnosPorCarreraYEstado(string carrera, EstadoAlumno estadoAlumno)
		{
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			this.Alumnos = alumnoDAO.CargarAlumnosPorCarrera(carrera);
			List<Alumno> alumnosFiltrados = new List<Alumno>();
			alumnosFiltrados = Alumnos.FindAll(alumno => alumno.EstadoAlumno == estadoAlumno);
			this.Alumnos = null;
			this.Alumnos = alumnosFiltrados;
		}
	}
}
