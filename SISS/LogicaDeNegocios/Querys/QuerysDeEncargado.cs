namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeEncargado
	{
		public const string ACTUALIZAR_ENCARGADO_POR_ID = "UPDATE Encargados SET Nombre = @NombreEncargado, CorreoElectronico = @CorreoElectronicoEncargado, Telefono = @TelefonoEncargado, Puesto = @PuestoEncargado WHERE IDEncargado = @IDEncargado";
		public const string CARGAR_ENCARGADO_POR_ID = "SELECT * FROM Encargados WHERE IDEncargado = @IDEncargado";
		public const string CARGAR_ENCARGADOS_TODOS = "SELECT * FROM Encargados";
		public const string CARGAR_IDS_POR_IDORGANIZACION = "SELECT IDEncargado FROM Encargados WHERE IDOrganizacion = @IDOrganizacion";
		public const string CARGAR_ID_POR_IDROYECTO = "SELECT IDEncargado FROM Proyectos WHERE IDProyecto = @IDProyecto";
		public const string GUARDAR_ENCARGADO = "INSERT INTO Encargados(IDOrganizacion, Nombre, CorreoElectronico, Telefono, Puesto ) VALUES(@IDOrganizacion, @NombreEncargado, @CorreoElectronicoEncargado, @TelefonoEncargado, @PuestoEncargado)";
		public const string CARGAR_ENARGADOS_CON_ID_NOMBRE_Y_ORGANIZACION = "SELECT IDEncargado,Nombre,IDOrganizacion FROM Encargados";
	}
}
