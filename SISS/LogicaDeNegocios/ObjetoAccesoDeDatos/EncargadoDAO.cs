using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class EncargadoDAO : Interfaces.IEncargadoDAO
	{
		public void ActualizarEncargadoPorID(int IDencargado, Encargado encargado)
		{
			throw new NotImplementedException();
		}

		public Encargado CargarEncargadoPorID(int IDencargado)
		{
			throw new NotImplementedException();
		}

		public List<Encargado> CargarEncargadosTodos()
		{
			throw new NotImplementedException();
		}

		public List<Encargado> CargarIDsPorIDOrganizacion(int IDorganizacion)
		{
			throw new NotImplementedException();
		}

		public DataTable ConvertirEncargadoADataTable(Encargado encargado)
		{
			throw new NotImplementedException();
		}

		public int GuardarEncargado(Encargado encargado)
		{
			throw new NotImplementedException();
		}
	}
}
