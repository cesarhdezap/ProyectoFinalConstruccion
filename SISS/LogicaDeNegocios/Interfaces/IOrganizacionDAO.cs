using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
    interface IOrganizacionDAO
    {
        void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion);
        List<Organizacion> CargarOrganizacionesTodas();
        Organizacion CargarOrganizacionPorID(int IDorganizacion);
        void GuardarOrganizacion(Organizacion organizacion);
        int ObtenerUltimoIDInsertado();
    }
}

