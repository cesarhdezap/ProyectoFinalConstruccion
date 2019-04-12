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
		List<Organizacion> CargarOrganizacionesTodas();
        Organizacion CargarOrganizacionPorID(int IDorganizacion);
		void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion);
        DataTable OrganizacionADataTable(Organizacion organizacion);
	}
}
