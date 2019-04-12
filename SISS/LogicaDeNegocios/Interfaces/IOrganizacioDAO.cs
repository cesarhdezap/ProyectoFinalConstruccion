using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IOrganizacioDAO
	{
        int GuardarOrganizacion(Organizacion organizacion);
		List<Organizacion> CargarOrganizacionesTodas();
        Organizacion CargarOrganizacionPorID(int IDorganizacion);
		void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion);
	}
}
