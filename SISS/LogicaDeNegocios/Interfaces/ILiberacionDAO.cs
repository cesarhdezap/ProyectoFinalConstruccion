using System.Collections.Generic;
using System.Data;
namespace LogicaDeNegocios.Interfaces
{
	interface ILiberacionDAO
	{
        Liberacion CargarLiberacionPorIDAsignacion(int IDasignacion);
        Liberacion ConvertirDataTableALiberacion (DataTable tablaLiberacion);
        List<Liberacion> ConvertirDataTableAListaDeLiberaciones(DataTable listaLiberacion);
        DataTable ConvertirLiberacionADataTable(Liberacion liberacion);
        int GuardarLiberacion(Liberacion liberacion);
	}
}
