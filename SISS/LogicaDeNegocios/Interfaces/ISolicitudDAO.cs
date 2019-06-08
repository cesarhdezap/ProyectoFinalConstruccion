using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces

{
	interface ISolicitudDAO
	{
        void ActualizarSolicitudPorID(int IDSolicitud, Solicitud solicitud);
        Solicitud CargarSolicitudPorID(int IDSolicitud);
        void GuardarSolicitud(Solicitud solicitud, Alumno alumno);
        Solicitud CargarIDPorIDAsignacion(int IDAsignacion);
        int ObtenerUltimoIDInsertado();
    }
}
