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
	/// Clase de abstraccion para acceso a objetos <see cref="Encargado"/> en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos <see cref="Encargado"/>.
	/// </summary>
	public class EncargadoDAO : IEncargadoDAO
	{
		/// <summary>
		/// Actualiza un <see cref="Encargado"/> dado su <see cref="Encargado.IDEncargado"/>.
		/// </summary>
		/// <param name="IDEncargado"><see cref="Encargado.IDEncargado"/> del Enc<see cref="Encargado"/>argado a actualizar.</param>
		/// <param name="encargado">El <see cref="Encargado"/> a actualizar.</param>
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
		/// Carga al <see cref="Encargado"/> con la <see cref="Encargado.IDEncargado"/> dada.
		/// </summary>
		/// <param name="IDEncargado"><see cref="Encargado.IDEncargado"/> del <see cref="Encargado"/> a cargar.</param>
		/// <returns>El <see cref="Encargado"/> con <see cref="Encargado.IDEncargado"/> dado.</returns>
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
		/// Carga a todos los <see cref="Encargado"/> en la base de datos.
		/// </summary>
		/// <returns>Una <see cref="List{Encargado}"/> con todos los <see cref="Encargado"/>.</returns>
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
		/// Carga una <see cref="List{Encargado}"/> de <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado y sus demas atributos como null basado en <see cref="Organizacion.IDOrganizacion"/> de la <see cref="Organizacion"/> relacionada.
		/// </summary>
		/// <param name="IDOrganizacion">La <see cref="Organizacion.IDOrganizacion"/> de la <see cref="Organizacion"/> relacionada a <see cref="Encargado.IDEncargado"/> del <see cref="Encargado"/> a cargar.</param>
		/// <returns>Una <see cref="List{Encargado}"/> de <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado</returns>
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
		/// Carga un <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado y sus demas atributos en como null basado en <see cref="Proyecto.IDProyecto"/> de <see cref="Proyecto"/> dada.
		/// </summary>
		/// <param name="IDProyecto"><see cref="Proyecto.IDProyecto"/> del <see cref="Proyecto"/> relacionada a los <see cref="Encargado.IDEncargado"/> a cargar.</param>
		/// <returns>Un <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado.</returns>
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
		/// Convierte una <see cref="DataTable"/> a un <see cref="Encargado"/>.
		/// </summary>
		/// <param name="tablaDeEncargado">La <see cref="DataTable"/> que contiene datos del <see cref="Encargado"/>.</param>
		/// <returns>El <see cref="Encargado"/> contenido en la <see cref="DataTable|"/>.</returns>
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
		/// Convierte una <see cref="DataTable"/> a un <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeEncargado">La <see cref="DataTable"/> que contiene datos del <see cref="Encargado"/>.</param>
		/// <returns>El <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado contenido en la <see cref="DataTable"/>.</returns>
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
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{T}"/> de <see cref="Encargado"/>.
		/// </summary>
		/// <param name="tablaDeEncargados">La <see cref="DataTable"/> que contiene datos de los <see cref="Encargado"/>.</param>
		/// <returns>La <see cref="List{T}"/> de <see cref="Encargado"/> contenido en la <see cref="DataTable"/>.</returns>
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
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{T}"/> de <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/>, nombre y <see cref="Encargado.Organizacion"/> inicializados y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeEncargados">La <see cref="DataTable"/> que contiene datos de los <see cref="Encargado"/>.</param>
		/// <returns>La <see cref="List{T}"/> de <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/>, nombre y <see cref="Encargado.Organizacion"/> inicializados contenido en la <see cref="DataTable"/>.</returns>
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
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{T}"/> de <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDeEncargados">La <see cref="DataTable"/> que contiene datos de los <see cref="Encargado"/>.</param>
		/// <returns>La lista de <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/> inicializado contenido en la <see cref="DataTable"/>.</returns>
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
		/// Guarda un <see cref="Encargado"/> en la base de datos.
		/// </summary>
		/// <param name="encargado">El <see cref="Encargado"/> a guardar.</param>
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
		/// Carga una lista de <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/>, nombre y <see cref="Encargado.Organizacion"/> inicializados y sus demas atributos como null.
		/// </summary>
		/// <returns>Un <see cref="Encargado"/> con solo <see cref="Encargado.IDEncargado"/>, nombre y <see cref="Encargado.IDEncargado"/> inicializados contenido en la <see cref="DataTable"/>.</returns>
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
		/// Inicializa un arreglo de <see cref="SqlParameter"/> basado en un <see cref="Encargado"/>.
		/// </summary>
		/// <param name="encargado">El <see cref="Encargado"/> para inicializar los parametros.</param>
		/// <returns>Un arreglo de <see cref="SqlParameter"/> donde cada posición es uno de los atributos del <see cref="Encargado"/>.</returns>
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
