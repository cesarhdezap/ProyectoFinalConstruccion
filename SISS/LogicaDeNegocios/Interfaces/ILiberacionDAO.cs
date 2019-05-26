using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface ILiberacionDAO
	{
        Liberacion CargarLiberacionPorIDAsignacion(int IDasignacion);
        int GuardarLiberacion(Liberacion liberacion);
	}
}
