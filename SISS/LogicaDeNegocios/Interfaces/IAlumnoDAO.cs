using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IAlumnoDAO
	{
		void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno);
		string CargarMatriculaPorCorreoElectronico(string correoElectronico);
		List<Alumno> CargarAlumnosPorEstado(EstadoAlumno estadoAlumno);
		List<Alumno> CargarAlumnosPorCarrera(string carrera);
		Alumno CargarAlumnoPorMatricula(string matricula);
        List<Alumno> CargarAlumnosTodos();
        void GuardarAlumno(Alumno alumno);
		Alumno CargarMatriculaPorIDAsignacion(int IDAsignacion);

	}
}
