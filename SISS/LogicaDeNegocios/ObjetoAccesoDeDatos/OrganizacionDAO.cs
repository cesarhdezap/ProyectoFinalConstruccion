using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using AccesoABaseDeDatos;
using System.Data.SqlClient;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class OrganizacionDAO : IOrganizacionDAO
	{
        public void ActualizarOrganizacionPorID(int IDOrganizacion, Organizacion organizacion)
        {
            if (IDOrganizacion <= 0)
            {
                throw new AccesoADatosException("Error al Actualizar Organizacion Por IDOrganizacion: " + IDOrganizacion + ". IDOrganizacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            SqlParameter[] parametrosDeOrganizacion = InicializarParametrosDeSql(organizacion);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("UPDATE Organizaciones SET CorreoElectronico = @CorreoElectronicoOrganizacion, Direccion = @DireccionOrganizacion, Telefono = @TelefonoOrganizacion, Nombre = @NombreOrganizacion WHERE IDOrganizacion = @IDOrganizacion", parametrosDeOrganizacion);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al actualizar Organizacion: " + organizacion.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al actualizar Organizacion: " + organizacion.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("La Organizacion con IDOrganizacion: " + IDOrganizacion + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
            }
        }

        public List<Organizacion> CargarOrganizacionesTodas()
        {
            DataTable tablaDeOrganizaciones = new DataTable();
            try
            {
                tablaDeOrganizaciones = AccesoADatos.EjecutarSelect("SELECT * FROM Organizaciones");
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar todas las Organizaciones", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar todas las Organizaciones", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			List<Organizacion> listaDeOrganizaciones = new List<Organizacion>();
            try
            {
                listaDeOrganizaciones = ConvertirDataTableAListaDeOrganizaciones(tablaDeOrganizaciones);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Organizaciones en cargar todas las Organizaciones", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return listaDeOrganizaciones;
        }

        public List<Organizacion> CargarIDYNombreDeOrganizaciones()
        {
            DataTable tablaDeOrganizaciones = new DataTable();
            try
            {
                tablaDeOrganizaciones = AccesoADatos.EjecutarSelect("SELECT IDOrganizacion, Nombre FROM Organizaciones");
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al cargar todas las Organizaciones", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar todas las Organizaciones", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			List<Organizacion> listaDeOrganizaciones = new List<Organizacion>();
            try
            {
                listaDeOrganizaciones = ConvertirDataTableAListaDeOrganizacionesConIDYNombre(tablaDeOrganizaciones);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Organizaciones en cargar todas las Organizaciones", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return listaDeOrganizaciones;
        }

        public Organizacion CargarOrganizacionPorID(int IDOrganizacion)
        {
            DataTable tablaDeOrganizacion = new DataTable();
            SqlParameter[] parametroIDOrganizacion = new SqlParameter[1];
            parametroIDOrganizacion[0] = new SqlParameter
            {
                ParameterName = "@IDOrganizacion",
                Value = IDOrganizacion
            };

            try
            {
                tablaDeOrganizacion = AccesoADatos.EjecutarSelect("SELECT * FROM Organizaciones WHERE IDOrganizacion = @IDOrganizacion", parametroIDOrganizacion);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar Organizacion con IDOrganizacion: " + IDOrganizacion, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar Organizacion con IDOrganizacion: " + IDOrganizacion, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}

			Organizacion organizacion = new Organizacion();
            try
            {
                organizacion = ConvertirDataTableAOrganizacion(tablaDeOrganizacion);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Organizacion en cargar Organizacion con IDOrganizacion: " + IDOrganizacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return organizacion;
        }

        public Organizacion CargarIDPorIDEncargado(int IDEncargado)
        {
            if (IDEncargado <= 0)
            {
                throw new AccesoADatosException("Error al  Cargar IDOrganizacion Por IDEncargado: " + IDEncargado + ". IDEncargado no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeOrganizacion = new DataTable();
            SqlParameter[] parametroIDEncargado = new SqlParameter[1];
            parametroIDEncargado[0] = new SqlParameter
            {
                ParameterName = "@IDEncargado",
                Value = IDEncargado
            };
            try
            {
                tablaDeOrganizacion = AccesoADatos.EjecutarSelect("SELECT IDOrganizacion FROM Encargados WHERE IDEncargado = @IDEncargado", parametroIDEncargado);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al Cargar IDOrganizacion Por IDEncargado: " + IDEncargado, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al Cargar IDOrganizacion Por IDEncargado: " + IDEncargado, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			Organizacion organizacion = new Organizacion();
            try
            {
                organizacion = ConvertirDataTableAOrganizacionConSoloID(tablaDeOrganizacion);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Organizacion en Cargar IDOrganizacion Por IDEncargado: " + IDEncargado, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return organizacion;
        }

        private List<Organizacion> ConvertirDataTableAListaDeOrganizaciones(DataTable tablaDeOrganizaciones)
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            List<Organizacion> listaDeOrganizaciones = new List<Organizacion>();
            foreach (DataRow fila in tablaDeOrganizaciones.Rows) {
                Organizacion organizacion = new Organizacion
                {
                    IDOrganizacion = (int)fila["IDOrganizacion"],
                    CorreoElectronico = fila["CorreoElectronico"].ToString(),
                    Direccion = fila["Direccion"].ToString(),
                    Telefono = fila["Telefono"].ToString(),
                    Nombre = fila["Nombre"].ToString(),
                    Encargados = encargadoDAO.CargarIDsPorIDOrganizacion((int)fila["IDOrganizacion"])
                };
                listaDeOrganizaciones.Add(organizacion);
            }
            return listaDeOrganizaciones;
        }

        private Organizacion ConvertirDataTableAOrganizacion(DataTable tablaDeOrganizacion)
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            Organizacion organizacion = new Organizacion();
            foreach (DataRow fila in tablaDeOrganizacion.Rows)
            {

                organizacion.IDOrganizacion = (int)fila["IDOrganizacion"];
                organizacion.CorreoElectronico = fila["CorreoElectronico"].ToString();
                organizacion.Direccion = fila["Direccion"].ToString();
                organizacion.Telefono = fila["Telefono"].ToString();
                organizacion.Nombre = fila["Nombre"].ToString();
                organizacion.Encargados = encargadoDAO.CargarIDsPorIDOrganizacion((int)fila["IDOrganizacion"]);
            }
            return organizacion;
        }

        private Organizacion ConvertirDataTableAOrganizacionConSoloID(DataTable tablaDeOrganizacion)
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            Organizacion organizacion = new Organizacion();
            foreach (DataRow fila in tablaDeOrganizacion.Rows)
            {

                organizacion.IDOrganizacion = (int)fila["IDOrganizacion"];

            }
            return organizacion;
        }

        private List<Organizacion> ConvertirDataTableAListaDeOrganizacionesConIDYNombre(DataTable tablaDeOrganizaciones)
        {
            List<Organizacion> listaDeOrganizaciones = new List<Organizacion>();
            foreach (DataRow fila in tablaDeOrganizaciones.Rows)
            {
                Organizacion organizacion = new Organizacion
                {
                    IDOrganizacion = (int)fila["IDOrganizacion"],
                    Nombre = fila["Nombre"].ToString(),
                };
                listaDeOrganizaciones.Add(organizacion);
            }

            return listaDeOrganizaciones;
        }

        public void GuardarOrganizacion(Organizacion organizacion)
        {
            SqlParameter[] parametrosDeOrganizacion = InicializarParametrosDeSql(organizacion);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Organizaciones(Nombre, CorreoElectronico, Telefono, Direccion) VALUES(@NombreOrganizacion, @CorreoElectronicoOrganizacion, @TelefonoOrganizacion, @DireccionOrganizacion)", parametrosDeOrganizacion);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al guardar Organizacion:" + organizacion.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al guardar Organizacion:" + organizacion.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Organizacion: " + organizacion.ToString() + "no fue guardada.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }


        private SqlParameter[] InicializarParametrosDeSql(Organizacion organizacion)
        {
            SqlParameter[] parametrosDeOrganizacion = new SqlParameter[4];

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

            return parametrosDeOrganizacion;
        }
    }
}