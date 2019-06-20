namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeSolicitud
	{
		public const string CARGAR_SOLICITUD_POR_ID = "SELECT * FROM Solicitudes WHERE IDSolicitud = @IDSolicitud";
		public const string GUARDAR_SOLICITUD = "INSERT INTO Solicitudes(Fecha, Matricula) VALUES(@Fecha, @Matricula)";
		public const string GUARDAR_RELACION_SOLICITUD_PROYECTO = "INSERT INTO SolicitudesProyectos(IDSolicitud, IDProyecto) VALUES(@IDSolicitud, @IDProyecto)";
		public const string CARGAR_ID_POR_MATRICULA = "SELECT IDSolicitud FROM Solicitudes WHERE Matricula = @MatriculaAlumno";
		public const string OBTENER_ULTIMO_ID_INSERTADO = "SELECT IDENT_CURRENT('Solicitudes')";
	}
}

