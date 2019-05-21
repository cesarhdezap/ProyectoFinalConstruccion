using System;
using System.Data;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class SolicitudDAO : Interfaces.ISolicitudDAO
	{
		public Solicitud CargarSolicitudPorID(int IDSolicitud)
		{
			//TODO
			throw new NotImplementedException();
		}
        
        private List<Solicitud> ConvertirDataTableAListaDeSolicitudes(DataTable dataTable)
		{
			//TODO
			throw new NotImplementedException();
		}
        
        private Solicitud ConvertirDataTableASolicutud(DataTable dataTable)
		{
			//TODO
			throw new NotImplementedException();
		}
        
        private DataTable ConvertirSolicitudADataTable (Solicitud Solicitud)
		{
			//TODO
			throw new NotImplementedException();
		}
        
        int GuardarSolicitud(Solicitud solicitud)
		{
			//TODO
			throw new NotImplementedException();
		}
        
	}
}
