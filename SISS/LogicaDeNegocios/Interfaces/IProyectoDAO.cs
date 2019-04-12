using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IProyectoDAO
	{
        int GuardarProyecto(Proyecto proyecto);
		List<Proyecto> CargarProyectosTodos();
        Proyecto CargarProyectoPorID(int IDproyecto);
		List<Proyecto> CargarIDsPorIDEncargado(int IDencargado);
		void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto);
	}
}
