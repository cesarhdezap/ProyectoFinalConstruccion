namespace LogicaDeNegocios.Interfaces
{
	interface ISolicitudDAO
	{
		Solicitud CargarSolicitudPorID(int IDSolicitud);
		void GuardarSolicitud(Solicitud solicitud);
		Solicitud CargarIDPorMatricula(string matriculaAlumno);
        int ObtenerUltimoIDInsertado();
    }
}
