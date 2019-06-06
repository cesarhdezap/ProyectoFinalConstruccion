using System.Collections.Generic;
using System.Data;
using static LogicaDeNegocios.Proyecto;

namespace LogicaDeNegocios.Interfaces
{
	interface IProyectoDAO
	{
        void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto);
        List<Proyecto> CargarIDsPorIDEncargado(int IDencargado);
        Proyecto CargarProyectoPorID(int IDproyecto);
        List<Proyecto> CargarProyectosPorEstado(EstadoProyecto estado);
        List<Proyecto> CargarProyectosTodos();
        void GuardarProyecto(Proyecto proyecto);
        int ObtenerUltimoIDInsertado();
    }
}
