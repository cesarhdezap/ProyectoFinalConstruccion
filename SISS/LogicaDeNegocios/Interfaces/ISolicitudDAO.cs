using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces

{
	interface ISolicitudDAO
	{
        Solicitud CargarSolicitudPorID(int IDSolicitud);
        int GuardarSolicitud(Solicitud solicitud);
	}
}
