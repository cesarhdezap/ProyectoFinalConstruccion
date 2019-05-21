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
        Encargado ConvertirDataTableAEncargado (DataTable dataTable);
        List<Encargado> ConvertirDataTableAListaDeEncargados (DataTable dataTable);
        DataTable  ConvertirEncargadoADataTable (Encargado encargado);
        int GuardarEncargado(Encargado encargado);
	}
}
