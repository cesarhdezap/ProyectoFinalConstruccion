using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IAsignacionDAO
	{
		int GuardarAsignacion(Asignacion asignacion);
		Asignacion CargarAsignacionPorID(int IDasignacion);
		DataTable AsignacionADataTable(Asignacion asignacion);

	}
}
