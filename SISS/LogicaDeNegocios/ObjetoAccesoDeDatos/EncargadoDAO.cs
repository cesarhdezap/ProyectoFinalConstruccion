using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LogicaDeNegocios.ObjetoAccesoDeDatos;


namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class EncargadoDAO : Interfaces.IEncargadoDAO
	{
		public void ActualizarEncargadoPorID(int IDencargado, Encargado encargado)
		{
			//TODO
			throw new NotImplementedException();
		}

		public Encargado CargarEncargadoPorID(int IDencargado)
		{
			//TODO
			throw new NotImplementedException();
		}

		public List<Encargado> CargarEncargadosTodos()
		{
			DataTable TablaDeEncargados = new DataTable();

            try
            {    
                TablaDeEncargados = AccesoADatos.EjecutarSelect("SELECT * FROM Encargado");
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }
            
			List<Encargado> ListaEncargados = new List<Encargado>();
			ListaEncargados = ConvertirDataTableAListaDeEncargados(ListaEncargados);

			return ListaEncargados;
		}

		public List<Encargado> CargarIDsPorIDOrganizacion(int IDorganizacion)
		{
			//TODO
			throw new NotImplementedException();
		}

        private Encargado ConvertirDataTableAEncargado (DataTable dataTable)
		{
			//TODO
			throw new NotImplementedException();
		}

        private List<Encargado> ConvertirDataTableAListaDeEncargados (DataTable dataTable)
		{
			//TODO
			throw new NotImplementedException();
		}

        private DataTable  ConvertirEncargadoADataTable (Encargado encargado)
		{
			//TODO
			throw new NotImplementedException();
		}

        public int GuardarEncargado(Encargado encargado)
		{
			//TODO
			throw new NotImplementedException();
		}
	}
}
