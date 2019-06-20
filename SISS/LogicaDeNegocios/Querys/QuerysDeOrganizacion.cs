namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeOrganizacion
	{
		public const string	ACTUALIZAR_ORGANIZACION_POR_ID = "UPDATE Organizaciones SET CorreoElectronico = @CorreoElectronicoOrganizacion, Direccion = @DireccionOrganizacion, Telefono = @TelefonoOrganizacion, Nombre = @NombreOrganizacion WHERE IDOrganizacion = @IDOrganizacion";
		public const string CARGAR_ORGANIZACIONES_TODAS = "SELECT * FROM Organizaciones";
		public const string CARGAR_ID_Y_NOMBRE_DE_ORGANIZACIONES = "SELECT IDOrganizacion, Nombre FROM Organizaciones";
		public const string CARGAR_ORGANIZACION_POR_ID = "SELECT * FROM Organizaciones WHERE IDOrganizacion = @IDOrganizacion";
		public const string CARGAR_ID_POR_IDENCARGADO = "SELECT IDOrganizacion FROM Encargados WHERE IDEncargado = @IDEncargado";
		public const string GUARDAR_ORGANIZACION = "INSERT INTO Organizaciones(Nombre, CorreoElectronico, Telefono, Direccion) VALUES(@NombreOrganizacion, @CorreoElectronicoOrganizacion, @TelefonoOrganizacion, @DireccionOrganizacion)";
	}
}
