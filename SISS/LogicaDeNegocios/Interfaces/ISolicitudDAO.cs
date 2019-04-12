namespace LogicaDeNegocios.Interfaces
{
	interface ISolicitudDAO
	{
        int GuardarSolicitud(Solicitud solicitud);
        Solicitud CargarSolicitudPorIDAsignacion(int IDasignacion);
	}
}
