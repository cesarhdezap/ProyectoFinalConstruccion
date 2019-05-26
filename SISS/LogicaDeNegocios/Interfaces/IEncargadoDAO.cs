using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IEncargadoDAO
	{
		void ActualizarEncargadoPorID(int IDencargado, Encargado encargado);
        Encargado CargarEncargadoPorID(int IDencargado);
		List<Encargado> CargarEncargadosTodos();
		List<Encargado> CargarIDsPorIDOrganizacion(int IDorganizacion);
        int GuardarEncargado(Encargado encargado);
	}
}
