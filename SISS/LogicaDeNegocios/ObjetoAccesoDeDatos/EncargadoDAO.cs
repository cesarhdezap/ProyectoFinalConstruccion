using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class EncargadoDAO : IEncargadoDAO
	{
		public void ActualizarEncargadoPorID(int IDencargado, Encargado encargado)
		{
            SqlParameter[] parametrosDeEncargado = InicializarParametrosDeSql(encargado);

            try
            {
                AccesoADatos.EjecutarInsertInto("UPDATE Encargados SET Nombre = @NombreEncargado, CorreoElectronico = @CorreoElectronicoEncargado, Telefono = @TelefonoEncargado, Puesto = @PuestoEncargado WHERE IDOrganizacion = @IDOrganizacion", parametrosDeEncargado);
            }
            catch (SqlException e)              //Aqui debe ir la ID de la organizacion a la que pertenece de alguna manera
            {                                   //Todavia no se como hacerle 

            }
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
                tablaDeEncargados = AccesoADatos.EjecutarSelect("SELECT * FROM Encargados");
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
                tablaDeEncargados = AccesoADatos.EjecutarSelect("SELECT IDEncargado FROM Encargados WHERE IDOrganizacion = @IDOrganizacion");
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

        public void GuardarEncargado(Encargado encargado)
		{
            SqlParameter[] parametrosDeEncargado = InicializarParametrosDeSql(encargado);

            try
            {
                AccesoADatos.EjecutarInsertInto("INSERT INTO Encargados(Nombre, CorreoElectronico, Telefono, Puesto, IDOrganizacion) VALUES(@NombreEncargado, @CorreoElectronicoEncargado, @TelefonoEncargado, @PuestoEncargado, @IDOrganizacion)", parametrosDeEncargado);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar encargado con id: " + encargado.IDEncargado, e);
            }
        }

        private SqlParameter[] InicializarParametrosDeSql(Encargado encargado)
        {
            SqlParameter[] parametrosDeEncargado = new SqlParameter[6];

            for (int i = 0; i < parametrosDeEncargado.Length; i++)
            {
                parametrosDeEncargado[i] = new SqlParameter();
            }

            parametrosDeEncargado[0].ParameterName = "@NombreEncargado";
            parametrosDeEncargado[0].Value = encargado.Nombre;
            parametrosDeEncargado[1].ParameterName = "@CorreoElectronicoEncargado";
            parametrosDeEncargado[1].Value = encargado.CorreoElectronico;
            parametrosDeEncargado[2].ParameterName = "@TelefonoEncargado";
            parametrosDeEncargado[2].Value = encargado.Telefono;
            parametrosDeEncargado[3].ParameterName = "@PuestoEncargado";
            parametrosDeEncargado[3].Value = encargado.Puesto;
            parametrosDeEncargado[4].ParameterName = "@IDEncargado";
            parametrosDeEncargado[4].Value = encargado.IDEncargado;
            parametrosDeEncargado[5].ParameterName = "@IDOrganizacion";
            parametrosDeEncargado[5].Value = encargado.Organizacion.IDOrganizacion;

            return parametrosDeEncargado;
        }
	}
}
