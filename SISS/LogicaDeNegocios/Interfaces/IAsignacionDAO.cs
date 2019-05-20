using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IAsignacionDAO
	{
		void ActualizarAsignacionPorID(int IDasignacion, Asignacion asignacion);
        Asignacion CargarAsignacionPorID(int IDasignacion);
        List<Asignacion> CargarIDsPorMatricula(string matricula);
        List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula);
        DataTable ConvertirAsignacionADataTable(Asignacion asignación);
        Asignacion ConvertirDataTableAAsignacion(DataTable tablaAsignaciones);
        List<Asignacion> ConvertirDataTableAListaDeAsignaciones(DataTable tablaAsignaciones);
        int GuardarAsignacion(Asignacion asignacion);
	}
}
