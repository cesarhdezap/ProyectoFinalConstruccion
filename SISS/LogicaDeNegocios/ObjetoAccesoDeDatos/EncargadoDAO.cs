using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos Encargado en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos Encargado.
	/// </summary>
	public class EncargadoDAO : IEncargadoDAO
	{
		/// <summary>
		/// Actualiza un Encargado dada su ID.
		/// </summary>
		/// <param name="IDEncargado">La ID del Encargado a actualizar.</param>
		/// <param name="encargado">El Encargado a actualizar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public void ActualizarEncargadoPorID(int IDEncargado, Encargado encargado)
		{
            if (IDEncargado <= 0)
            {
                throw new AccesoADatosException("Error al actualizar Encargado Por IDEncargado: " + IDEncargado + ". IDEncargado no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            SqlParameter[] parametrosDeEncargado = InicializarParametrosDeSql(encargado);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeEncargado.ACTUALIZAR_ENCARGADO_POR_ID, parametrosDeEncargado);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, encargado);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El encargado con matricula: " + IDEncargado + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
            }
        }

		/// <summary>
		/// Carga al Encargado con la ID dada.
		/// </summary>
		/// <param name="IDEncargado">La ID del Encargado a cargar.</param>
		/// <returns>El Encargado con la ID dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Encargado CargarEncargadoPorID(int IDEncargado)
		{
            if (IDEncargado <= 0)
            {
                throw new AccesoADatosException("Error al Cargar Encargado Por IDEncargado: " + IDEncargado + ". IDEncargado no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeEncargado = new DataTable();
            SqlParameter[] parametroIDEncargado = new SqlParameter[1];
            parametroIDEncargado[0] = new SqlParameter
            {
                ParameterName = "@IDEncargado",
                Value = IDEncargado
            };

            try
            {
                tablaDeEncargado = AccesoADatos.EjecutarSelect(QuerysDeEncargado.CARGAR_ENCARGADO_POR_ID, parametroIDEncargado);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDEncargado);
			}
			Encargado encargado = new Encargado();
            try
            {
                encargado = ConvertirDataTableAEncargado(tablaDeEncargado);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Encargado en cargar Encargado con IDEncargado: " + IDEncargado, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return encargado;
        }

		/// <summary>
		/// Carga a todos los Encargado en la base de datos.
		/// </summary>
		/// <returns>Una lista con todos los Encargado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<Encargado> CargarEncargadosTodos()
		{
			DataTable tablaDeEncargados = new DataTable();
            try
            {    
                tablaDeEncargados = AccesoADatos.EjecutarSelect(QuerysDeEncargado.CARGAR_ENCARGADOS_TODOS);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}
			List<Encargado> ListaEncargados = new List<Encargado>();
            try
            {
                ListaEncargados = ConvertirDataTableAListaDeEncargados(tablaDeEncargados);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Encargados en cargar todos los Encargados", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return ListaEncargados;
		}

		/// <summary>
		/// Carga una lista de Encargado con solo sus ID incializadas y sus demas atributos como null basado en la ID de Organizacion relacionada.
		/// </summary>
		/// <param name="IDOrganizacion">La ID de la Organizacion relacionada a las ID de Encargado a cargar.</param>
		/// <returns>Una lista de Encargado con solo sus ID inicializadas</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si la ID es invalida o si el cliente de SQL tiro una excepción.</exception>
		public List<Encargado> CargarIDsPorIDOrganizacion(int IDOrganizacion)
		{
            if (IDOrganizacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDsEncargado Por IDOrganizacion: " + IDOrganizacion + ". IDOrganizacion no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeEncargados = new DataTable();
            SqlParameter[] parametroIDOrganicacion = new SqlParameter[1];
            parametroIDOrganicacion[0] = new SqlParameter
            {
                ParameterName = "@IDOrganizacion",
                Value = IDOrganizacion
            };
            try
            {
                tablaDeEncargados = AccesoADatos.EjecutarSelect(QuerysDeEncargado.CARGAR_IDS_POR_IDORGANIZACION, parametroIDOrganicacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDOrganizacion);
			}
			List<Encargado> listaDeEncargados = new List<Encargado>();
            try
            {
                listaDeEncargados = ConvertirDataTableAListaDeEncargadosConSoloID(tablaDeEncargados);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Encargado en cargar IDsEncargado con IDOrganizacion: " + IDOrganizacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return listaDeEncargados;
        }

		/// <summary>
		/// Carga un Encargado con solo su ID inicializada y sus demas atributos en como null basado en la ID de Proyecto dada.
		/// </summary>
		/// <param name="IDProyecto">La ID del Proyecto relacionada a la ID a cargar.</param>
		/// <returns>Un Encargado con solo su ID inicializada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public Encargado CargarIDPorIDProyecto(int IDProyecto)
        {
            if (IDProyecto <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDEncargado Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeEncargado = new DataTable();
            SqlParameter[] parametroIDProyecto = new SqlParameter[1];
            parametroIDProyecto[0] = new SqlParameter
            {
                ParameterName = "@IDProyecto",
                Value = IDProyecto
            };
            try
            {
                tablaDeEncargado = AccesoADatos.EjecutarSelect(QuerysDeEncargado.CARGAR_ID_POR_IDROYECTO, parametroIDProyecto);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDProyecto);
			}
			Encargado encargado = new Encargado();
            try
            {
                encargado = ConvertirDataTableAEncargadoConSoloID(tablaDeEncargado);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Encargado en cargar IDEncargado con IDProyecto: " + IDProyecto, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return encargado;
        }

		/// <summary>
		/// Convierte una DataTable a un Encargado.
		/// </summary>
		/// <param name="tablaDeEncargado">La DataTable que contiene datos del Encargado<./param>
		/// <returns>El Encargado contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private Encargado ConvertirDataTableAEncargado(DataTable tablaDeEncargado)
		{
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Encargado encargado = new Encargado();
            foreach (DataRow fila in tablaDeEncargado.Rows)
            {
                encargado.IDEncargado = (int)fila["IDEncargado"];
                encargado.Nombre = fila["Nombre"].ToString();
                encargado.Puesto = fila["Puesto"].ToString();
                encargado.CorreoElectronico = fila["CorreoElectronico"].ToString();
                encargado.Telefono = fila["Telefono"].ToString();
                encargado.Proyectos = proyectoDAO.CargarIDsPorIDEncargado((int)fila["IDEncargado"]);
            }
            return encargado;
		}

		/// <summary>
		/// Convierte una DataTable a un Encargado con solo su ID inicializada y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeEncargado">La DataTable que contiene datos del Encargado.</param>
		/// <returns>El Encargado con solo su ID inicializada contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private Encargado ConvertirDataTableAEncargadoConSoloID(DataTable tablaDeEncargado)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Encargado encargado = new Encargado();
            foreach (DataRow fila in tablaDeEncargado.Rows)
            {
                encargado.IDEncargado = (int)fila["IDEncargado"];
            }
            return encargado;
        }

		/// <summary>
		/// Convierte una DataTable a una lista de Encargado.
		/// </summary>
		/// <param name="tablaDeEncargados">La DataTable que contiene datos de los Encargado.</param>
		/// <returns>La lista de Encargado contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<Encargado> ConvertirDataTableAListaDeEncargados(DataTable tablaDeEncargados)
		{
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            List<Encargado> encargados = new List<Encargado>();
            foreach (DataRow fila in tablaDeEncargados.Rows)
            {
                Encargado encargado = new Encargado
                {
                    IDEncargado = (int)fila["IDEncargado"],
                    Nombre = fila["Nombre"].ToString(),
                    Puesto = fila["Puesto"].ToString(),
                    CorreoElectronico = fila["CorreoElectronico"].ToString(),
                    Telefono = fila["Telefono"].ToString(),
                    Proyectos = proyectoDAO.CargarIDsPorIDEncargado((int)fila["IDEncargado"])
                };
                encargados.Add(encargado);
            }
            return encargados;
        }

		/// <summary>
		/// Convierte una DataTable a una lista de Encargado con solo sus ID, nombre y organizacion inicializados y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeEncargados">La DataTable que contiene datos de los Encargado.</param>
		/// <returns>La lista de Encargado con solo sus ID, nombre y organizacion inicializados contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<Encargado> ConvertirDataTableAListaDeEncargadosConIDNombreYOrganizacion (DataTable tablaDeEncargados)
        {
            List<Encargado> encargados = new List<Encargado>();
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();

            foreach (DataRow fila in tablaDeEncargados.Rows)
            {
                Encargado encargado = new Encargado
                {
                    IDEncargado = (int)fila["IDEncargado"],
                    Nombre = fila["Nombre"].ToString(),
                };
                encargado.Organizacion = organizacionDAO.CargarIDPorIDEncargado(encargado.IDEncargado);
                encargados.Add(encargado);
            }
            return encargados;
        }

		/// <summary>
		/// Convierte una DataTable a una lista de Encargado con solo sus ID inicializadas y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeEncargados">La DataTable que contiene datos de los Encargado.</param>
		/// <returns>La lista de Encargado con solo sus ID inicializadas contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<Encargado> ConvertirDataTableAListaDeEncargadosConSoloID(DataTable tablaDeEncargados)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            List<Encargado> encargados = new List<Encargado>();
            foreach (DataRow fila in tablaDeEncargados.Rows)
            {
                Encargado encargado = new Encargado
                {
                    IDEncargado = (int)fila["IDEncargado"],
                };
                encargados.Add(encargado);
            }
            return encargados;
        }

		/// <summary>
		/// Guarda un Encargado en la base de datos.
		/// </summary>
		/// <param name="encargado">El Encargado a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void GuardarEncargado(Encargado encargado)
		{
            SqlParameter[] parametrosDeEncargado = InicializarParametrosDeSql(encargado);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeEncargado.GUARDAR_ENCARGADO, parametrosDeEncargado);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, encargado);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Encargado: " + encargado.ToString() + "no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

		/// <summary>
		/// Carga una lista de Encargado con solo sus ID, nombre y organizacion inicializados y sus demas atributos como null.
		/// </summary>
		/// <returns>Un Encargado con solo sus ID, nombre y organizacion inicializados contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		public List<Encargado> CargarEncargadosConIDNombreYOrganizacion()
        {
            DataTable tablaDeEncargados = new DataTable();

            try
            {
                tablaDeEncargados = AccesoADatos.EjecutarSelect(QuerysDeEncargado.CARGAR_ENARGADOS_CON_ID_NOMBRE_Y_ORGANIZACION);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}

			List<Encargado> ListaEncargados = new List<Encargado>();
            try
            {
                ListaEncargados = ConvertirDataTableAListaDeEncargadosConIDNombreYOrganizacion(tablaDeEncargados);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Encargados en cargar encargados con id y nombre", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return ListaEncargados;
        }

		/// <summary>
		/// Inicializa un arreglo de SqlParameter basado en un Encargado.
		/// </summary>
		/// <param name="encargado">El Encargado para inicializar los parametros.</param>
		/// <returns>Un arreglo de SqlParameter donde cada posición es uno de los atributos del Encargado.</returns>
		private SqlParameter[] InicializarParametrosDeSql(Encargado encargado)
        {
            SqlParameter[] parametrosDeEncargado = new SqlParameter[5];
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
            parametrosDeEncargado[4].ParameterName = "@IDOrganizacion";
            parametrosDeEncargado[4].Value = encargado.Organizacion.IDOrganizacion;
            return parametrosDeEncargado;
        }
    }
}
