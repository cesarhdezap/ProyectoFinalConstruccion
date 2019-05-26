using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IProyectoDAO
	{
        void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto);
        List<Proyecto> CargarIDsPorIDEncargado(int IDencargado);
        Proyecto CargarProyectoPorID(int IDproyecto);
        List<Proyecto> CargarProyectosPorEstado(EstadoProyecto estado);
        List<Proyecto> CargarProyectosTodos();
        int GuardarProyecto(Proyecto proyecto);
	}
}
