using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IOrganizacionDAO
    {
        void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion);
        List<Organizacion> CargarOrganizacionesTodas();
		List<Organizacion> CargarIDYNombreDeOrganizaciones();
		Organizacion CargarOrganizacionPorID(int IDorganizacion);
		Organizacion CargarIDPorIDEncargado(int IDEncargado);
		void GuardarOrganizacion(Organizacion organizacion);
    }
}

