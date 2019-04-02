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
				connection.Open();
				SqlDataAdapter dataAdapter = new SqlDataAdapter();
				SqlCommand command = new SqlCommand(query);
				DataTable dataTable = new DataTable();
				dataTable = null;
				DataSet dataSet = new DataSet();
				command.Connection = connection;
				command.CommandText = query;
				if (parameters != null)
				{
					command.Parameters.AddRange(parameters);
				}	
				command.ExecuteNonQuery();
				dataAdapter.SelectCommand = command;
				dataAdapter.Fill(dataSet);
				dataTable = dataSet.Tables[0];
				return dataTable;
			}
		}

		public static void ExecuteInsertInto(String query, SqlParameter[] parameters)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(query);
				command.Connection = connection;
				command.CommandText = query;
				command.Parameters.AddRange(parameters);
				command.ExecuteNonQuery();
			}
		}
	}
}
