namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeReporteMensual
	{
		public const string CARGAR_IDS_POR_IDASIGNACION = "SELECT IDDocumento FROM ReportesMensuales WHERE IDAsignacion = @IDAsignacion";
		public const string CARGAR_REPORTE_MENSUAL_POR_ID = "SELECT * FROM ReportesMensuales WHERE IDDocumento = @IDDocumento";
		public const string GUARDAR_REPORTE_MENSUAL = "INSERT INTO ReportesMensuales(HorasReportadas, FechaDeEntrega, NumeroDeReporte, IDPersonal, Mes, IDAsignacion) VALUES(@HorasReportadas, @FechaDeEntrega, @NumeroDeReporte, @IDPersonal, @Mes, @IDAsignacion)";
		public const string ACTUALIZAR_REPORTE_MENSUAL = "UPDATE ReportesMensuales SET HorasReportadas = @HorasReportadas WHERE IDDocumento = @IDReporte";
		public const string OBTENER_ULTIMO_IDINSERTADO = "SELECT IDENT_CURRENT('ReportesMensuales')";

	}
}
