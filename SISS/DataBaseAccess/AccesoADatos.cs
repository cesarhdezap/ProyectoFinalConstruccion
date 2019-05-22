using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace AccesoABaseDeDatos
{
	public static class AccesoADatos
	{
		private const int PRIMERA_POSICION_EN_DATATABLE = 0;
        public static readonly string CadenaDeConexion = ConfigurationManager.ConnectionStrings["myConection"].ConnectionString.ToString();
		

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

				try
				{
					conexion.Open();
					comando.ExecuteNonQuery();
					DataSet dataSet = new DataSet();
					dataAdapter.Fill(dataSet);
					dataTable = dataSet.Tables[PRIMERA_POSICION_EN_DATATABLE];
				}
				catch (SqlException SqlException)
				{
					//TODO
					Console.Write("Error - Coneccion.ejecutarConsultaSeleccionada - Consulta: " + consulta + " \nExcepcion: " + SqlException.StackTrace.ToString());
				}		
				finally
				{
					CerrarConexion(conexion);
				}

				return dataTable;
			}
		}

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
                Console.WriteLine("\n*\n*\n*\n* " + CadenaDeConexion);
                int numeroDeFilasAfectadas = VALOR_DE_ERROR_POR_DEFECTO;
				try
				{
					conexion.Open();
					numeroDeFilasAfectadas = comando.ExecuteNonQuery();
				}
				catch (SqlException sqlException)
				{
					//TODO
					Console.Write("Error - Coneccion.ejecutarConsultaSeleccionada - Consulta: " + consulta + " \nExcepcion: " + sqlException.StackTrace.ToString());
					throw sqlException;
				}
				finally
				{
					CerrarConexion(conexion);
				}
                return numeroDeFilasAfectadas;
            }
		}
	}
}
