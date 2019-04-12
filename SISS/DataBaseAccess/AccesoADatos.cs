using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseAccess
{
	public class AccesoADatos
	{
		private static String CadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
		private const int PRIMERA_POSICION_EN_DATATABLE = 0;

		public static DataTable ExecuteSelect(String Consulta, SqlParameter[] Parametros = null)
		{
			using (SqlConnection Conexion = new SqlConnection(CadenaDeConexion))
			{
				SqlCommand Comando = new SqlCommand(Consulta, Conexion);

				if (Parametros != null)
				{
					Comando.Parameters.AddRange(Parametros);
				}
				
				SqlDataAdapter DataAdapter = new SqlDataAdapter(Comando);
				DataTable DataTable = new DataTable();

				try
				{
					Conexion.Open();
					Comando.ExecuteNonQuery();
					DataSet DataSet = new DataSet();
					DataAdapter.Fill(DataSet);
					DataTable = DataSet.Tables[PRIMERA_POSICION_EN_DATATABLE];
				}
				catch (SqlException SqlException)
				{
					//TODO
					Console.Write("Error - Coneccion.ejecutarConsultaSeleccionada - Consulta: " + Consulta + " \nExcepcion: " + SqlException.StackTrace.ToString());
				}		
				finally
				{
					CerrarConexion(Conexion);
				}

				return DataTable;
			}
		}

		public static void CerrarConexion(SqlConnection Conexion)
		{
			if (Conexion != null)
			{
				if (Conexion.State == ConnectionState.Open)
				{
					Conexion.Close();
				}
			}
		}
			
	
		public static void EjecutarInsertarDentro(string Consulta, SqlParameter[] Parametros)
		{
			using (SqlConnection Conexion = new SqlConnection(CadenaDeConexion))
			{
				
				SqlCommand Comando = new SqlCommand(Consulta, Conexion);
				Comando.Parameters.AddRange(Parametros);
				try
				{
					Conexion.Open();
					Comando.ExecuteNonQuery();
				}
				catch (SqlException sqlException)
				{
					//TODO
					Console.Write("Error - Coneccion.ejecutarConsultaSeleccionada - Consulta: " + Consulta + " \nExcepcion: " + sqlException.StackTrace.ToString());
					throw sqlException;
				}
				finally
				{
					CerrarConexion(Conexion);
				}
			}
		}
	}
}
