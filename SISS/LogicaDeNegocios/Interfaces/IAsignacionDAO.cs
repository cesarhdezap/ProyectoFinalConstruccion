using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IAsignacionDAO
	{
		int GuardarAsignacion(Asignacion asignacion);
		Asignacion CargarAsignacionPorID(int IDasignacion);
		List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula);
		void ActualizarAsignacionPorID(int IDasignacion, Asignacion asignacion);
	}
}
