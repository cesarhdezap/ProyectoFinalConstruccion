using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IAlumnoDAO
	{
		void GuardarAlumno(Alumno alumno);
		List<Alumno> CargarAlumnosTodos();
		Alumno CargarAlumnoPorMatricula(string matricula);
		List<Alumno> CargarAlumnosPorEstado(EstadoAlumno estadoAlumno);
		void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno);
	}
}
