﻿using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeAlumnos
	{
		public List<Alumno> Alumnos { get; set; }

		public bool ValidarExistencia(Alumno alumno)
		{
            CargarAlumnosTodos();
            bool resultadoDeCreacion = false;
            resultadoDeCreacion = !Alumnos.Exists(e => e.CorreoElectronico == alumno.CorreoElectronico);
            return resultadoDeCreacion;
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
			foreach (Alumno alumno in this.Alumnos)
			{
				if (alumno.EstadoAlumno == estadoAlumno)
				{
					alumnosFiltrados.Add(alumno);
				}
			}
			this.Alumnos = null;
			this.Alumnos = alumnosFiltrados;
		}
	}
}
