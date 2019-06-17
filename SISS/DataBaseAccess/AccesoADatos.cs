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
		public static DataTable EjecutarSelect(String consulta, SqlParameter[] parametros = null)
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
                try
                {
                    conexion.Open();
                    filasAfectadas = comando.ExecuteNonQuery();
                    dataAdapter.Fill(dataTable);
                }
                catch(IOException e)
                {
                    throw new IOException("Error al intentar conectarse a la base de datos ",e);
                }
                finally
                {
                    CerrarConexion(conexion);
                }

                if (filasAfectadas == 0)
                {
                    throw new NotImplementedException("Debe implementarse una excepcion personalizada");
                }
                
                return dataTable;
            }
        }

        /// <summary>
        /// Cierra la conexión con la base de datos.
        /// </summary>
        /// <param name="conexion">Una conexión de SQL.</param>
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
        /// <returns></returns>
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
				try
				{
					conexion.Open();
					numeroDeFilasAfectadas = comando.ExecuteNonQuery();
				}
				finally
				{
					CerrarConexion(conexion);
				}
                return numeroDeFilasAfectadas;
            }
		}

        /// <summary>
        /// Ejecuta una consulta de solo un campo en la base de datos.
        /// </summary>
        /// <param name="consulta">Una consulta de SQL</param>
        /// <param name="parametros">Parametros utilizados en la consulta</param>
        /// <returns>Un numero del campo seleccionado</returns>
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
                try
                {
                    conexion.Open();
                    resultadoEscalar = Convert.ToInt32(comando.ExecuteScalar());
                }
                finally
                {
                    CerrarConexion(conexion);
                }
                return resultadoEscalar;
            }
        }
    }
}
