using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Interfaces
{
	interface ILiberacionDAO
	{
        int GuardarLiberacion(Liberacion liberacion);
        Liberacion CargarLiberacionPorID(int IDLiberacion);
        DataTable LiberacionADataTable(Liberacion liberacion);

	}
}
