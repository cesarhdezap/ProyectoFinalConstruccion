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

		public static DataTable ExecuteSelect(String query, SqlParameter[] parameters = null)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
	
				if (parameters != null)
				{
					command.Parameters.AddRange(parameters);
				}
				connection.Open();
				SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
				try
				{
					command.ExecuteNonQuery();
				}
				catch (SqlException e)
				{
					Console.Write("Error - Connection.executeSelectQuery - Query: " + query + " \nException: " + e.StackTrace.ToString());
					return null;
				}
				finally
				{
					CloseConnection(connection);
				}
				DataSet dataSet = new DataSet();
				dataAdapter.Fill(dataSet);
				DataTable dataTable = new DataTable();
				dataTable = dataSet.Tables[0];
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
			
	
		public static bool ExecuteInsertInto(String query, SqlParameter[] parameters)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddRange(parameters);
				try
				{
					command.ExecuteNonQuery();
				}
				catch (SqlException e)
				{
					Console.Write("Error - Connection.executeSelectQuery - Query: " + query + " \nException: " + e.StackTrace.ToString());
					return false;
				}
				finally
				{
					CloseConnection(connection);
				}
				return true;
			}
		}
	}
}
