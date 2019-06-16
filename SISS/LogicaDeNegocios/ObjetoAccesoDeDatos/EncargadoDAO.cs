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
	public class EncargadoDAO : IEncargadoDAO
	{
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
