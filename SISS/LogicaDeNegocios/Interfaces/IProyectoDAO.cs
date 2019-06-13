﻿using System.Collections.Generic;
using System.Data;
using static LogicaDeNegocios.Proyecto;

namespace LogicaDeNegocios.Interfaces
{
	interface IProyectoDAO
	{
        void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto);
        List<Proyecto> CargarIDsPorIDEncargado(int IDencargado);
		int ContarAlumnosAsignadosAProyecto(int IDProyecto);
		Proyecto CargarProyectoPorID(int IDproyecto);
		List<Proyecto> CargarIDsPorIDSolicitud(int IDSolicitud);
		List<Proyecto> CargarProyectosPorEstado(EstadoProyecto estado);
        List<Proyecto> CargarProyectosTodos();
		Proyecto CargarIDProyectoPorIDAsignacion(int IDAsignacion);
		void GuardarProyecto(Proyecto proyecto);
    }
}
