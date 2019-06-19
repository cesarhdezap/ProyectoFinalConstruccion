namespace AccesoABaseDeDatos
{
    public enum CodigoDeErrorDeSqlException
    {
		ConexionAServidorFallida = -1,
		TiempoDeEsperaExpirado = -2,
		ServidorNoRespondio = 2,
		ColumnaInvalida = 207,
		ObjetoInvalido = 208,
		ServidorNoEncontrado = 53,
		ConexionRemotaFallida = 1326,
		LoginFallido = 18456,
        InsercionFallidaPorLlavePrimariaDuplicada = 2627,
        ServicioSQLPausado = 17142
    }
}
