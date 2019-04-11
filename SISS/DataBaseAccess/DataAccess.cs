using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseAccess
{
	public class DatabaseAccess
	{
		private static String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
		private const int FIRST_DATA_TABLE_POSITION = 0;

		public static DataTable ExecuteSelect(String query, SqlParameter[] parameters = null)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);

				if (parameters != null)
				{
					command.Parameters.AddRange(parameters);
				}
				
				SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
				DataTable dataTable = new DataTable();

				try
				{
					connection.Open();
					command.ExecuteNonQuery();
					DataSet dataSet = new DataSet();
					dataAdapter.Fill(dataSet);
					dataTable = dataSet.Tables[FIRST_DATA_TABLE_POSITION];
				}
				catch (SqlException sqlException)
				{
					//TODO
					Console.Write("Error - Connection.executeSelectQuery - Query: " + query + " \nException: " + sqlException.StackTrace.ToString());
					throw sqlException;
				}		
				finally
				{
					CloseConnection(connection);
				}

				return dataTable;
			}
		}

		public static void CloseConnection(SqlConnection connection)
		{
			if (connection != null)
			{
				if (connection.State == ConnectionState.Open)
				{
					connection.Close();
				}
			}
		}
			
	
		public static void ExecuteInsertInto(String query, SqlParameter[] parameters)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddRange(parameters);
				try
				{
					connection.Open();
					command.ExecuteNonQuery();
				}
				catch (SqlException sqlException)
				{
					//TOD
					Console.Write("Error - Connection.executeSelectQuery - Query: " + query + " \nException: " + sqlException.StackTrace.ToString());
					throw sqlException;
				}
				finally
				{
					CloseConnection(connection);
				}
			}
		}
	}
}
