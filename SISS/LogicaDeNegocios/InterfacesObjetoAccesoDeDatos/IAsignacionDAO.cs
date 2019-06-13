using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IAsignacionDAO
	{
		void ActualizarAsignacionPorID(int IDasignacion, Asignacion asignacion);
        Asignacion CargarAsignacionPorID(int IDasignacion);
        Asignacion CargarIDPorMatriculaDeAlumno(string matricula);
        void GuardarAsignacion(Asignacion asignacion);
        List<Asignacion> CargarIDsPorIDProyecto(int IDProyecto);
    }
}
