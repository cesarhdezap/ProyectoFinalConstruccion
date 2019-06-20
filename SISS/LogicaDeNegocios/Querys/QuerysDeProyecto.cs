namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeProyecto
	{
		public const string ACTUALIZAR_PROYECTO_POR_ID = "UPDATE Proyectos SET Estado = @EstadoProyecto WHERE IDProyecto = @IDProyecto";
		public const string CARGAR_IDS_POR_IDENCARGADO = "SELECT IDProyecto FROM Proyectos WHERE IDEncargado = @IDEncargado";
		public const string CONTAR_ALUMNOS_ASIGNACIONS_A_PROYECTO = "SELECT COUNT(*) FROM Asignaciones WHERE IDProyecto = @IDProyecto";
		public const string CARGAR_PROYECTO_POR_ID = "SELECT * FROM Proyectos WHERE IDProyecto = @IDProyecto";
		public const string CARGAR_IDS_POR_IDSOLICITUD = "SELECT IDProyecto FROM SolicitudesProyectos WHERE IDSolicitud = @IDSolicitud";
		public const string CARGAR_PROYECTOS_POR_ESTADO = "SELECT * FROM Proyectos WHERE Estado = @EstadoDeProyecto";
		public const string CARGAR_PROYECTOS_TODOS = "SELECT * FROM Proyectos";
		public const string CARGAR_ID_POR_IDASIGNACION = "SELECT IDProyecto FROM Asignaciones WHERE IDAsignacion = IDAsignacion";
		public const string GUARDAR_PROYECTO = "INSERT INTO Proyectos(Nombre, Estado, DescripcionGeneral, ObjetivoGeneral, Cupo, IDEncargado) VALUES(@NombreProyecto, @EstadoProyecto, @DescripcionGeneralProyecto, @ObjetivoGeneralProyecto, @CupoProyecto, @IDEncargado)"; 
	}
}
