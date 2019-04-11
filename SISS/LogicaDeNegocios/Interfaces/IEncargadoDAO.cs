using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IEncargadoDAO
	{
		int GuardarEncargado(Encargado encargado);
		Encargado CargarEncargadoPorID(int IDencargado);
		List<Encargado> CargarEncargadosTodos();
		DataTable EncargadoADataTable(Encargado encargado);

	}
}
