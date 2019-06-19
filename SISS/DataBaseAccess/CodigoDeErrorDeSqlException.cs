namespace AccesoABaseDeDatos
{
    public enum CodigoDeErrorDeSqlException
    {
		ConexionAServidorFallida = -1,
		TiempoDeEsperaExpirado = -2,
		ServidorNoRespondio = 2,
        /// <summary>
        /// Número de error de SQL cuando se llama una columna que no existe.
        /// </summary>
		ColumnaInvalida = 207,
        /// <summary>
        /// Número de error de SQL cuando no se encuentra la tabla.
        /// </summary>
		ObjetoInvalido = 208,
		ServidorNoEncontrado = 53,
		ConexionRemotaFallida = 1326,
		LoginFallido = 18456,
        InsercionFallidaPorLlavePrimariaDuplicada = 2627,
        ServicioSQLPausado = 17142
    }
}
