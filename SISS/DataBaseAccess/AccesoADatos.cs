using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AccesoABaseDeDatos
{
    /// <summary>
    /// Clase para acceso a la base de datos.
    /// Contiene metodos para insertar y seleccionar tablas de la base de datos.
    /// </summary>
    public static class AccesoADatos
	{
		private const int PRIMERA_POSICION_EN_DATATABLE = 0;
        public static readonly string CadenaDeConexion = ConfigurationManager.ConnectionStrings["myConection"].ConnectionString.ToString();

        /// <summary>
        /// Ejecuta una consulta de selección en la base de datos.
        /// </summary>
        /// <param name="consulta">Una cadena con una consulta de SQL.</param>
        /// <param name="parametros">Parametros utilizados en la consulta.</param>
        /// <returns>Una DataTable con la tabla recuperada de la base de datos.</returns>
        /// <exception cref="IOException">Tira esta excepción cuando no puede establecer conexión con la base de datos.</exception>
        /// <exception cref="SqlException">Tira esta excepción con un código de error según la causa de la falla.</exception>
		public static DataTable EjecutarSelect(string consulta, SqlParameter[] parametros = null)
		{
			using (SqlConnection conexion = new SqlConnection(CadenaDeConexion))
			{
				SqlCommand comando = new SqlCommand(consulta, conexion);

				if (parametros != null)
				{
					comando.Parameters.AddRange(parametros);
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
				DataTable dataTable = new DataTable();
				int filasAfectadas = 0;
				conexion.Open();
				filasAfectadas = comando.ExecuteNonQuery();
				dataAdapter.Fill(dataTable);
				CerrarConexion(conexion);
				return dataTable;
			}
        }

        /// <summary>
        /// Cierra la conexión con la base de datos.
        /// </summary>
        /// <param name="conexion">Una conexión de SQL.</param>
        /// <exception cref="SqlException">Tira esta excepción con un código de error según la causa de la falla.</exception>
		public static void CerrarConexion(SqlConnection conexion)
		{
			if (conexion != null)
			{
				if (conexion.State == ConnectionState.Open)
				{
					conexion.Close();
				}
			}
		}

        /// <summary>
        /// Ejecuta una consulta de inserción en la base de datos.
        /// </summary>
        /// <param name="consulta">Una consulta de SQL.</param>
        /// <param name="parametros">Parametros utilizados en la consulta.</param>
        /// <returns>Un entero con las filas afectadas por la consulta.</returns>
        /// <exception cref="SqlException">Tira esta excepción con un código de error según la causa de la falla.</exception>
        public static int EjecutarInsertInto(string consulta, SqlParameter[] parametros = null)
		{
			using (SqlConnection conexion = new SqlConnection(CadenaDeConexion))
			{
				const int VALOR_DE_ERROR_POR_DEFECTO = -1;
				SqlCommand comando = new SqlCommand(consulta, conexion);

				if (parametros != null)
				{
					comando.Parameters.AddRange(parametros);
				}

				int numeroDeFilasAfectadas = VALOR_DE_ERROR_POR_DEFECTO;
				conexion.Open();
				numeroDeFilasAfectadas = comando.ExecuteNonQuery();
				return numeroDeFilasAfectadas;
			}
		}

        /// <summary>
        /// Ejecuta una consulta de solo un campo en la base de datos.
        /// </summary>
        /// <param name="consulta">Una consulta de SQL</param>
        /// <param name="parametros">Parametros utilizados en la consulta</param>
        /// <returns>Un numero del campo seleccionado</returns>
        /// <exception cref="SqlException">Tira esta excepción con un código de error según la causa de la falla.</exception>
        public static int EjecutarOperacionEscalar(string consulta, SqlParameter[] parametros = null)
        {
			using (SqlConnection conexion = new SqlConnection(CadenaDeConexion))
			{
				SqlCommand comando = new SqlCommand(consulta, conexion);

				if (parametros != null)
				{
					comando.Parameters.AddRange(parametros);
				}

				int resultadoEscalar = 0;
				conexion.Open();
				resultadoEscalar = Convert.ToInt32(comando.ExecuteScalar());
				return resultadoEscalar;
			}
        }
    }
}
