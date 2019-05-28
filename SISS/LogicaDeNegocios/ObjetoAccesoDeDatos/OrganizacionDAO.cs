using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Interfaces;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class OrganizacionDAO : Interfaces.IOrganizacionDAO
	{
        public void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        public List<Organizacion> CargarOrganizacionesTodas()
        {
            //TODO
			throw new NotImplementedException();
        }

        public Organizacion CargarOrganizacionPorID(int IDorganizacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        private List<Organizacion> ConvertirDataTableAListaDeOrganizaciones(DataTable tablaOrganizacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        private Organizacion ConvertirDataTableAOrganizacion(DataTable tablaOrganizacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        private DataTable ConvertirOrganizacionADataTable(Organizacion organizacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        public int GuardarOrganizacion(Organizacion organizacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        List<Organizacion> IOrganizacionDAO.ConvertirDataTableAListaDeOrganizaciones(DataTable tablaOrganizacion)
        {
            throw new System.NotImplementedException();
        }

        Organizacion IOrganizacionDAO.ConvertirDataTableAOrganizacion(DataTable tablaOrganizacion)
        {
            throw new System.NotImplementedException();
        }

        DataTable IOrganizacionDAO.ConvertirOrganizacionADataTable(Organizacion organizacion)
        {
            throw new System.NotImplementedException();
        }
    }
}