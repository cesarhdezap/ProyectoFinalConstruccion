using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IOrganizacionDAO
	{
        void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion);
        List<Organizacion> CargarOrganizacionesTodas();
        Organizacion CargarOrganizacionPorID(int IDorganizacion);
        List<Organizacion> ConvertirDataTableAListaDeOrganizaciones(DataTable tablaOrganizacion);
        Organizacion ConvertirDataTableAOrganizacion(DataTable tablaOrganizacion);
        DataTable ConvertirOrganizacionADataTable(Organizacion organizacion);
        int GuardarOrganizacion(Organizacion organizacion);
    }
}
