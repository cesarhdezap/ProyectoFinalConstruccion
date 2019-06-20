namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeDocumentoDeEntregaUnica
	{
		public const string CARGAR_DOCUMENTO_DE_ENTREGA_UNICA_POR_ID = "SELECT * FROM DocumentosDeEntregaUnica WHERE IDDocumento = @IDDocumento";
		public const string CARGAR_IDS_POR_IDASIGNACION = "SELECT IDDocumento FROM DocumentosDeEntregaUnica WHERE IDAsignacion = @IDasignacion";
		public const string GUARDAR_DOCUMENTO_DE_ENTREGA_UNICA = "INSERT INTO DocumentosDeEntregaUnica(FechaDeEntrega, TipoDeDocumento, IDPersonal, IDAsignacion) VALUES(@FechaDeEntrega, @TipoDeDocumento, @DocenteAdministrativo, @IDAsignacion)";
		public const string OBTENER_ULTIMO_ID_INSERTADO = "SELECT IDENT_CURRENT('DocumentosDeEntregaUnica')";
	}
}
