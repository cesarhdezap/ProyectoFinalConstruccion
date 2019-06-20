namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeLiberacion
	{
		public const string GUARDAR_LIBERACION = "INSERT INTO Liberaciones(Fecha, IDDocumento) VALUES(@Fecha, @IDDocumento)";
		public const string CARGAR_LIBERACION_POR_ID = "SELECT * FROM Liberaciones WHERE IDLiberacion = @Liberacion";
		public const string OBTENER_ULTIMO_ID_INSERTADO = "SELECT IDENT_CURRENT('Liberaciones')";
	}
}
