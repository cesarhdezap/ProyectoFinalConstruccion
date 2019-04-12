using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Interfaces
{
	interface IOrganizacioDAO
	{
        int GuardarOrganizacion(Organizacion organizacion);
        Organizacion CargarOrganizacionPorID(int IDOrganizacion);
        List<Organizacion> CargarOrganizacionesTodos();
        DataTable ConvertirOrganizacionADataTable(Organizacion organizacion);
	}
}
