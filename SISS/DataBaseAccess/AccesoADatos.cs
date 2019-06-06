using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

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
	}
}
