﻿using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IEncargadoDAO
	{
		void ActualizarEncargadoPorID(int IDencargado, Encargado encargado);
        Encargado CargarEncargadoPorID(int IDencargado);
		List<Encargado> CargarEncargadosTodos();
		List<Encargado> CargarIDsPorIDOrganizacion(int IDorganizacion);
		Encargado CargarIDPorIDProyecto(int IDProyecto);
		void GuardarEncargado(Encargado encargado);
		List<Encargado> CargarEncargadosConIDNombreYOrganizacion();
	}
}
