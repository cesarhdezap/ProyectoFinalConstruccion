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

		public Encargado CargarEncargadoPorID(int IDEncargado)
		{
			DataTable tablaDeEncargado = new DataTable();
            SqlParameter[] parametroIDEncargado = new SqlParameter[1];
            parametroIDEncargado[0] = new SqlParameter();
            parametroIDEncargado[0].ParameterName = "@IDEncargado";
            parametroIDEncargado[0].Value = IDEncargado;

            try
            {
                tablaDeEncargado = AccesoADatos.EjecutarSelect("SELECT * FROM Encargados WHERE IDEncargado = @IDEncargado", parametroIDEncargado);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            Encargado encargado = ConvertirDataTableAEncargado(tablaDeEncargado);

            return encargado;
        }

		public List<Encargado> CargarEncargadosTodos()
		{
			DataTable tablaDeEncargados = new DataTable();

            try
            {    
                tablaDeEncargados = AccesoADatos.EjecutarSelect("SELECT * FROM Encargado");
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }
            
			List<Encargado> ListaEncargados = new List<Encargado>();
			ListaEncargados = ConvertirDataTableAListaDeEncargados(tablaDeEncargados);

			return ListaEncargados;
		}

		public List<Encargado> CargarIDsPorIDOrganizacion(int IDOrganizacion)
		{
            DataTable tablaDeEncargados = new DataTable();
            SqlParameter[] parametroIDOrganicacion = new SqlParameter[1];
            parametroIDOrganicacion[0] = new SqlParameter();
            parametroIDOrganicacion[0].ParameterName = "@IDOrganizacion";
            parametroIDOrganicacion[0].Value = IDOrganizacion;
            try
            {
                tablaDeEncargados = AccesoADatos.EjecutarSelect("SELECT IDEncargado FROM Alumno WHERE IDOrganizacion = @IDOrganizacion");
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }
            List<Encargado> listaDeEncargados = new List<Encargado>();

            listaDeEncargados = ConvertirDataTableAListaDeEncargados(tablaDeEncargados);

            return listaDeEncargados;
        }

        private Encargado ConvertirDataTableAEncargado (DataTable tablaDeEncargado)
		{
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Encargado encargado = (Encargado)(from DataRow fila in tablaDeEncargado.Rows
                                              select new Encargado()
                                              {
                                                  IDEncargado = (int)fila["IDEncargado"],
                                                  Nombre = fila["Nombre"].ToString(),
                                                  Puesto = fila["Puesto"].ToString(),
                                                  CorreoElectronico = fila["CorreoElectronico"].ToString(),
                                                  Telefono = fila["Telefono"].ToString(),
                                                  Proyectos = proyectoDAO.CargarIDsPorIDEncargado((int)fila["IDEncargado"])
                                              }
                                             );
            return encargado;
		}

        private List<Encargado> ConvertirDataTableAListaDeEncargados (DataTable tablaDeEncargados)
		{
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            List<Encargado> encargados = (from DataRow fila in tablaDeEncargados.Rows
                                              select new Encargado()
                                              {
                                                  IDEncargado = (int)fila["IDEncargado"],
                                                  Nombre = fila["Nombre"].ToString(),
                                                  Puesto = fila["Puesto"].ToString(),
                                                  CorreoElectronico = fila["CorreoElectronico"].ToString(),
                                                  Telefono = fila["Telefono"].ToString(),
                                                  Proyectos = proyectoDAO.CargarIDsPorIDEncargado((int)fila["IDEncargado"])
                                              }
                                             ).ToList();
            return encargados;
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
