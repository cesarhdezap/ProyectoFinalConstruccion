using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using AccesoABaseDeDatos;
using System.Data.SqlClient;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos Organizacion en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos Organizacion.
	/// </summary>
	public class OrganizacionDAO : IOrganizacionDAO
	{

		/// <summary>
		/// Actualiza una Organizacion dada su ID.
		/// </summary>
		/// <param name="IDOrganizacion">La ID de la Organizacion a actualizar.</param>
		/// <param name="organizacion">La Organizacion a actualizar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeOrganizacion.ACTUALIZAR_ORGANIZACION_POR_ID, parametrosDeOrganizacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, organizacion);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("La Organizacion con IDOrganizacion: " + IDOrganizacion + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
            }
        }

		/// <summary>
		/// Carga a todas las Organizacion en la base de datos.
		/// </summary>
		/// <returns>Una lista con todas las Organizacion.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<Organizacion> CargarOrganizacionesTodas()
        {
            DataTable tablaDeOrganizaciones = new DataTable();

            try
            {
                tablaDeOrganizaciones = AccesoADatos.EjecutarSelect(QuerysDeOrganizacion.CARGAR_ORGANIZACIONES_TODAS);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
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

		/// <summary>
		/// Carga una lista de Organizacion con solo sus ID y nombre inicializados y sus demas atributos como null.
		/// </summary>
		/// <returns>Una Organizacion con solo sus ID y nombre inicializados contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		public List<Organizacion> CargarIDYNombreDeOrganizaciones()
        {
            DataTable tablaDeOrganizaciones = new DataTable();

            try
            {
                tablaDeOrganizaciones = AccesoADatos.EjecutarSelect(QuerysDeOrganizacion.CARGAR_ID_Y_NOMBRE_DE_ORGANIZACIONES);
            }

			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
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

		/// <summary>
		/// Carga a la Organizacion con la ID dada.
		/// </summary>
		/// <param name="IDOrganizacion">La ID de la Organizacion a cargar.</param>
		/// <returns>La Organizacion con la ID dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeOrganizacion = AccesoADatos.EjecutarSelect(QuerysDeOrganizacion.CARGAR_ORGANIZACION_POR_ID, parametroIDOrganizacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDOrganizacion);
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

		/// <summary>
		/// Carga una Organizacion con solo su ID inicializada y sus demas atributos en como null basado en la ID de Encargado dada.
		/// </summary>
		/// <param name="IDEncargado">La ID del Encargado relacionada a la ID a cargar.</param>
		/// <returns>Una Organizacion con solo su ID inicializada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeOrganizacion = AccesoADatos.EjecutarSelect(QuerysDeOrganizacion.CARGAR_ID_POR_IDENCARGADO, parametroIDEncargado);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDEncargado);
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

		/// <summary>
		/// Convierte una DataTable a una lista de Organizacion.
		/// </summary>
		/// <param name="tablaDeOrganizaciones">La DataTable que contiene datos de las Organizacion.</param>
		/// <returns>La lista de Organizacion contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<Organizacion> ConvertirDataTableAListaDeOrganizaciones(DataTable tablaDeOrganizaciones)
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            List<Organizacion> listaDeOrganizaciones = new List<Organizacion>();

            foreach (DataRow fila in tablaDeOrganizaciones.Rows)
			{
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

		/// <summary>
		/// Convierte una DataTable a una Organizacion.
		/// </summary>
		/// <param name="tablaDeOrganizacion">La DataTable que contiene datos de la Organizacion<./param>
		/// <returns>La Organizacion contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Convierte una DataTable a una Organizacion con solo su ID inicializada y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeOrganizacion">La DataTable que contiene datos de la Organizacion.</param>
		/// <returns>La Organizacion con solo su ID inicializada contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception
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

		/// <summary>
		/// Convierte una DataTable a una lista de Organizacion con solo sus ID y nombre inicializados y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeOrganizaciones">La DataTable que contiene datos de las Organizacion.</param>
		/// <returns>La lista de Organizacion con solo sus ID y nombre inicializados contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Guarda una Organizacion en la base de datos.
		/// </summary>
		/// <param name="organizacion">La Organizacion a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void GuardarOrganizacion(Organizacion organizacion)
        {
            SqlParameter[] parametrosDeOrganizacion = InicializarParametrosDeSql(organizacion);
            int filasAfectadas = 0;

            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeOrganizacion.GUARDAR_ORGANIZACION, parametrosDeOrganizacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, organizacion);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Organizacion: " + organizacion.ToString() + "no fue guardada.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

		/// <summary>
		/// Inicializa un arreglo de SqlParameter basado en una Organizacion.
		/// </summary>
		/// <param name="organizacion">La Organizacion para inicializar los parametros.</param>
		/// <returns>Un arreglo de SqlParameter donde cada posición es uno de los atributos de la Organizacion.</returns>
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