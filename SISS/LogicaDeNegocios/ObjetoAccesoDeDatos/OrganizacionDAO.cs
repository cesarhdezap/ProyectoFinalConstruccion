using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Interfaces;
using AccesoABaseDeDatos;
using System.Data.SqlClient;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class OrganizacionDAO : IOrganizacionDAO
	{
        public void ActualizarOrganizacionPorID(int IDorganizacion, Organizacion organizacion)
        {
            SqlParameter[] parametrosDeOrganizacion = InicializarParametrosDeSql(organizacion);

            try
            {
                AccesoADatos.EjecutarInsertInto("UPDATE Organizaciones SET CorreoElectronico = @CorreoElectronicoOrganizacion, Direccion = @DireccionOrganizacion, Telefono = @TelefonoOrganizacion, Nombre = @NombreOrganizacion WHERE IDOrganizacion = @IDOrganizacion", parametrosDeOrganizacion);
            }
            catch (SqlException e)
            {

            }
        }

        public List<Organizacion> CargarOrganizacionesTodas()
        {
            DataTable tablaDeOrganizaciones = new DataTable();
            
            try
            {
                tablaDeOrganizaciones = AccesoADatos.EjecutarSelect("SELECT * FROM Organizaciones");
            }
            catch (SqlException e)
            {

            }

            List<Organizacion> listaDeOrganizaciones = ConvertirDataTableAListaDeOrganizaciones(tablaDeOrganizaciones);

            return listaDeOrganizaciones;
        }

        public Organizacion CargarOrganizacionPorID(int IDOrganizacion)
        {
            DataTable tablaDeOrganizacion = new DataTable();
            SqlParameter[] parametroIDOrganizacion = new SqlParameter[1];
            parametroIDOrganizacion[0] = new SqlParameter();
            parametroIDOrganizacion[0].ParameterName = "@IDOrganizacion";
            parametroIDOrganizacion[0].Value = IDOrganizacion;

            try
            {
                tablaDeOrganizacion = AccesoADatos.EjecutarSelect("SELECT * FROM Alumno WHERE IDOrgnizacion = @IDOrganizacion", parametroIDOrganizacion);
            }
            catch (SqlException e)
            {
                
            }

            Organizacion organizacion = ConvertirDataTableAOrganizacion(tablaDeOrganizacion);

            return organizacion;
        }

        private List<Organizacion> ConvertirDataTableAListaDeOrganizaciones(DataTable tablaDeOrganizaciones)
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            List<Organizacion> listaDeOrganizaciones = (from DataRow fila in tablaDeOrganizaciones.Rows
                                                       select new Organizacion()
                                                       {
                                                           IDOrganizacion = (int)fila["IDOrganizacion"],
                                                           CorreoElectronico = fila["CorreoElectronico"].ToString(),
                                                           Direccion = fila["Direccion"].ToString(),
                                                           Telefono = fila["Telefono"].ToString(),
                                                           Nombre = fila["Nombre"].ToString(),
                                                           Encargados = encargadoDAO.CargarIDsPorIDOrganizacion((int)fila["IDOrganizacion"])
                                                       }
                           ).ToList();
            return listaDeOrganizaciones;
        }

        private Organizacion ConvertirDataTableAOrganizacion(DataTable tablaDeOrganizacion)
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            Organizacion organizacion = (Organizacion)(from DataRow fila in tablaDeOrganizacion.Rows
                                     select new Organizacion()
                                     {
                                         IDOrganizacion = (int)fila["IDOrganizacion"],
                                         CorreoElectronico = fila["CorreoElectronico"].ToString(),
                                         Direccion = fila["Direccion"].ToString(),
                                         Telefono = fila["Telefono"].ToString(),
                                         Nombre = fila["Nombre"].ToString(),
                                         Encargados = encargadoDAO.CargarIDsPorIDOrganizacion((int)fila["IDOrganizacion"])
                                     }
                           );
            return organizacion;
        }

        public void GuardarOrganizacion(Organizacion organizacion)
        {
            SqlParameter[] parametrosDeOrganizacion = InicializarParametrosDeSql(organizacion);

            try
            {
                AccesoADatos.EjecutarInsertInto("INSERT INTO Orgnizaciones(CorreoElectronico, Direccion, Telefono, Nombre) VALUES(@CorreoElectronicoOrganizacion, @DireccionOrganizacion, @TelefonoOrganizacion, @NombreOrganizacion)", parametrosDeOrganizacion);
            }
            catch (SqlException e)
            {

            }
        }

        private SqlParameter[] InicializarParametrosDeSql(Organizacion organizacion)
        {
            SqlParameter[] parametrosDeOrganizacion = new SqlParameter[5];

            for (int i = 0; i < parametrosDeOrganizacion.Length; i++)
            {
                parametrosDeOrganizacion[i] = new SqlParameter();
            }
            parametrosDeOrganizacion[0].ParameterName = "@NombreOrganizacion";
            parametrosDeOrganizacion[0].Value = organizacion.Nombre;
            parametrosDeOrganizacion[1].ParameterName = "@CorreoElectronicoOrganizacion";
            parametrosDeOrganizacion[1].Value = organizacion.CorreoElectronico;
            parametrosDeOrganizacion[2].ParameterName = "@DireccionOrganizacion";
            parametrosDeOrganizacion[2].Value = organizacion.Direccion;
            parametrosDeOrganizacion[3].ParameterName = "@TelefonoOrganizacion";
            parametrosDeOrganizacion[3].Value = organizacion.Telefono;
            parametrosDeOrganizacion[4].ParameterName = "@IDOrganizacion";
            parametrosDeOrganizacion[4].Value = organizacion.IDOrganizacion;

            return parametrosDeOrganizacion;
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}