namespace AccesoABaseDeDatos
{
    public enum CodigoDeErrorDeSqlException
    {
		/// <summary>
		/// Número de error de SQL generico cuando no logra conectarse al servidor.
		/// </summary>
		ConexionAServidorFallida = -1,
		/// <summary>
		/// Número de error de SQL cuando se agota el tiempo de espera para conectarse al servidor.
		/// </summary>
		TiempoDeEsperaExpirado = -2,
		/// <summary>
		/// Número de error de SQL cuando el servidor no respondio a la petición.
		/// </summary>
		ServidorNoRespondio = 2,
        /// <summary>
        /// Número de error de SQL cuando se llama una columna que no existe.
        /// </summary>
		ColumnaInvalida = 207,
        /// <summary>
        /// Número de error de SQL cuando no se encuentra la tabla.
        /// </summary>
		ObjetoInvalido = 208,
		/// <summary>
		/// Número de error de SQL cuando no se encontro al servidor especificado.
		/// </summary>
		ServidorNoEncontrado = 53,
		/// <summary>
		/// Número de error de SQL cuando la conexión remota al servidor falló.
		/// </summary>
		ConexionRemotaFallida = 1326,
		/// <summary>
		/// Número de error de SQL cuando el login utilizado para entrar al servidor es invalido.
		/// </summary>
		LoginFallido = 18456,
		/// <summary>
		/// Número de error de SQL cuando se intena insertar un llave primaria ya existente.
		/// </summary>
		InsercionFallidaPorLlavePrimariaDuplicada = 2627,
		/// <summary>
		/// Número de error de SQL cuando el servicio de SQL fue pausado por algún motivo.
		/// </summary>
		ServicioSQLPausado = 17142
    }
}
