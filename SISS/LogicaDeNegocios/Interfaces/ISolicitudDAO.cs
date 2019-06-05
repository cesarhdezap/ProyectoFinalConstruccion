using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces

{
	interface ISolicitudDAO
	{
        Solicitud CargarSolicitudPorID(int IDSolicitud);
        void GuardarSolicitud(Solicitud solicitud);
        Solicitud CargarIDPorIDAsignacion(int IDAsignacion);
        int ObtenerUltimoIDInsertado();
    }
}
