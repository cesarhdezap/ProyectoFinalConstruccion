using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces

{
	interface ISolicitudDAO
	{
        Solicitud CargarSolicitudPorID(int IDSolicitud);
        List<Solicitud> ConvertirDataTableAListaDeSolicitudes(DataTable dataTable);
        Solicitud ConvertirDataTableASolicutud(DataTable dataTable);
        DataTable ConvertirSolicitudADataTable (Solicitud Solicitud);
        int GuardarSolicitud(Solicitud solicitud);
	}
}
