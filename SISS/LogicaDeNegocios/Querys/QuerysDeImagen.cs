namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeImagen
	{
		public const string ACTUALIZAR_IMAGEN_POR_IDDOCUMENTO = "UPDATE Imagenes SET DatosDeImagen = @DatosDeImagen WHERE IDDocumento = @IDDocumento AND TipoDeDocumentoEnImagen = @TipoDeDocumentoEnImagen";
		public const string CARGAR_IMAGEN_POR_IDDOCUMENTO_Y_TIPO_DE_DOCUMENTO_EN_IMAGEN = "SELECT DatosDeImagen FROM Imagenes WHERE IDDocumento = @IDDocumento AND TipoDeDocumentoEnImagen = @TipoDeDocumentoEnimagen";
		public const string GUARDAR_IMAGEN = "INSERT INTO Imagenes(IDDocumento, DatosDeImagen, TipoDeDocumentoEnImagen) VALUES(@IDDocumento, @DatosDeImagen, @TipoDeDocumentoEnImagen)";
	}
}
