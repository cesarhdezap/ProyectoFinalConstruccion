using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class OrganizacionDAO : Interfaces.IOrganizacioDAO
	{
		public void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion)
		{
			throw new NotImplementedException();
		}

		public List<Organizacion> CargarOrganizacionesTodas()
		{
			throw new NotImplementedException();
		}

		public Organizacion CargarOrganizacionPorID(int IDOrganizacion)
		{
			throw new NotImplementedException();
		}

		public int GuardarOrganizacion(Organizacion organizacion)
		{
			throw new NotImplementedException();
		}

		public DataTable OrganizacionADataTable(Organizacion organizacion)
		{
			throw new NotImplementedException();
		}
	}
}
