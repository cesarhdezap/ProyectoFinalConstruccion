using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Interfaces
{
	interface ISolicitudDAO
	{
        int GuardarSolicitud(Solicitud solicitud);
        Solicitud CargarSolicitudPorIDAsignacion(int IDAsignacion);
        DataTable SolicitudADataTable(Solicitud solicitud);
	}
}
