using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IEncargadoDAO
	{
		int GuardarEncargado(Encargado encargado);
		List<Encargado> CargarEncargadosTodos();
		List<Encargado> CargarIDsPorIDOrganizacion(int IDorganizacion);
		Encargado CargarEncargadoPorID(int IDencargado);
		void ActualizarEncargadoPorID(int IDencargado, Encargado encargado);
	}
}
