using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstracción para acceso a objetos <see cref="DocenteAcademico"/> en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos <see cref="DocenteAcademico"/>.
	/// </summary>
	public class DocenteAcademicoDAO : IDocenteAcademicoDAO
    {
		/// <summary>
		/// Actualiza un <see cref="DocenteAcademico"/> dado <see cref="DocenteAcademico.IDPersonal"/>.
		/// </summary>
		/// <param name="IDPersonal"><see cref="DocenteAcademico.IDPersonal"/> del <see cref="DocenteAcademico"/> a actualizar.</param>
		/// <param name="docenteAcademico">El <see cref="DocenteAcademico"/> a actualizar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public void ActualizarDocenteAcademicoPorIDPersonal(int IDPersonal, DocenteAcademico docenteAcademico)
        {
            if (IDPersonal <= 0)
            {
                throw new AccesoADatosException("Error al Actualizar DocenteAcademico Por IDpersonal: " + IDPersonal + ". IDpersonal no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }

            SqlParameter[] parametrosDeDocenteAcademico = InicializarParametrosDeSql(docenteAcademico);
			int filasAfectadas = 0;

            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeDocenteAcademico.ACTUALIZAR_DOCENTE_ACADEMICO_POR_IDPERSONAL, parametrosDeDocenteAcademico);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, docenteAcademico);
			}

			if (filasAfectadas <= 0)
			{
				throw new AccesoADatosException("El DocenteAcademico con ID: " + IDPersonal + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
			}
		}

		/// <summary>
		/// Carga un <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializado y sus demas atributos como null con <see cref="DocenteAcademico.Rol"/> = <see cref="Rol.Coordinador"/> dado <see cref="DocenteAcademico.Carrera"/>.
		/// </summary>
		/// <param name="carrera"><see cref="DocenteAcademico.Carrera"/> del <see cref="DocenteAcademico"/> a cargar.</param>
		/// <returns>Un <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public DocenteAcademico CargarIDCoordinadorPorCarrera(string carrera)
        {
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroCarrera = new SqlParameter[1];

            parametroCarrera[0] = new SqlParameter
            {
                ParameterName = "@Carrera",
                Value = carrera
            };

            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect(QuerysDeDocenteAcademico.CARGAR_ID_COORDINADOR_POR_CARRERA, parametroCarrera);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, carrera);
			}

			DocenteAcademico docenteAcademico = new DocenteAcademico();

            try
            {
                docenteAcademico = ConvertirDataTableADocenteAcademicoConSoloID(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocenteAcademico en cargar IDPersonal con Carrera: " + carrera, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return docenteAcademico;
        }

		/// <summary>
		/// Carga un <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializado y sus demas atributos como null dado su correo electrónico y <see cref="DocenteAcademico.Rol"/>.
		/// </summary>
		/// <param name="correoElectronico">El correo electrónico del <see cref="DocenteAcademico"/> a cargar.</param>
		/// <param name="rol"><see cref="DocenteAcademico.Rol"/> del <see cref="DocenteAcademico"/> a cargar.</param>
		/// <returns>Un <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public string CargarIDPorCorreoYRol(string correoElectronico, Rol rol)
        {
            DataTable tablaDeID = new DataTable();
            SqlParameter[] parametros = new SqlParameter[2];

            parametros[0] = new SqlParameter
            {
                ParameterName = "@CorreoElectronico",
                Value = correoElectronico
            };

            parametros[1] = new SqlParameter
            {
                ParameterName = "@Rol",
                Value = (int)rol
            };

            try
            {
                tablaDeID = AccesoADatos.EjecutarSelect(QuerysDeDocenteAcademico.CARGAR_ID_POR_CORREO_Y_ROL, parametros);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, correoElectronico);
			}

			string IDUsuario = string.Empty;

            try
            {
                IDUsuario = ConvertirDataTableADocenteAcademicoConSoloID(tablaDeID).IDPersonal.ToString();
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocenteAcademico en cargar ID por CorreoElectronico: " + correoElectronico, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return IDUsuario;
        }

		/// <summary>
		/// Carga un <see cref="DocenteAcademico"/> dado <see cref="DocenteAcademico.IDPersonal"/>.
		/// </summary>
		/// <param name="IDPersonal"><see cref="DocenteAcademico.IDPersonal"/> del <see cref="DocenteAcademico"/> a cargar.</param>
		/// <returns>Un <see cref="DocenteAcademico"/> con la <see cref="DocenteAcademico.IDPersonal"/> dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDPersonal)
        {
            if (IDPersonal <= 0)
            {
                throw new AccesoADatosException("Error al cargar DocenteAcademico Por IDpersonal: " + IDPersonal + ". IDpersonal no es valido.");
            }

            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroIDPersonal = new SqlParameter[1];

            parametroIDPersonal[0] = new SqlParameter
            {
                ParameterName = "@IDPersonal",
                Value = IDPersonal
            };

            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect(QuerysDeDocenteAcademico.CARGAR_DOCENTE_ACADEMICO_POR_IDPERSONAL, parametroIDPersonal);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDPersonal);
			}

			DocenteAcademico docenteAcademico = new DocenteAcademico();

            try
            {
                docenteAcademico = ConvertirDataTableADocenteAcademico(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocenteAcademico en cargar DocenteAcademico por IDPersonal: " + IDPersonal, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return docenteAcademico;
        }

		/// <summary>
		/// Carga un <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializada y sus demas atributos como null dado <see cref="ReporteMensual.IDDocumento"/> de un <see cref="ReporteMensual"/> relacionado.
		/// </summary>
		/// <param name="IDDocumento"><see cref="ReporteMensual.IDDocumento"/> del <see cref="ReporteMensual"/> relacionado al <see cref="DocenteAcademico"/> a cargar.</param>
		/// <returns>Un <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public DocenteAcademico CargarIDPorIDReporteMensual(int IDDocumento)
        {
            if (IDDocumento <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDPersonal Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }

            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroIDDocumento = new SqlParameter[1];

            parametroIDDocumento[0] = new SqlParameter
            {
                ParameterName = "@IDDocumento",
                Value = IDDocumento
            };

			try
			{
				tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect(QuerysDeDocenteAcademico.CARGAR_ID_POR_IDDOCUMENTO, parametroIDDocumento);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDDocumento);
			}

			DocenteAcademico docenteAcademico = new DocenteAcademico();

            try
            {
                docenteAcademico = ConvertirDataTableADocenteAcademicoConSoloID(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocenteAcademico en cargar IDPersonal con IDDcumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return docenteAcademico;
        }

		/// <summary>
		/// Carga una <see cref="List{T}"/> de todos los <see cref="DocenteAcademico"/> que tengan el <see cref="DocenteAcademico.EsActivo"/> dado.
		/// </summary>
		/// <param name="esActivo"><see cref="DocenteAcademico.EsActivo"/> de los <see cref="DocenteAcademico"/> a cargar.</param>
		/// <returns>Una <see cref="List{T}"/> de todos los <see cref="DocenteAcademico"/> con el <see cref="DocenteAcademico.EsActivo"/> dado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool esActivo)
        {
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroEsActivo = new SqlParameter[1];

            parametroEsActivo[0] = new SqlParameter
            {
                ParameterName = "@EsActivo",
                Value = esActivo
            };

            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect(QuerysDeDocenteAcademico.CARGAR_DOCENTES_ACADEMICOS_POR_ESTADO, parametroEsActivo);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, esActivo);
			}

			List<DocenteAcademico> listaDeDocentesAcademicos = new List<DocenteAcademico>();

            try
            {
                listaDeDocentesAcademicos = ConvertirDataTableAListaDeDocentesAcademicos(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir data table a lista de DocentesAcademicos en cargar DocentesAcademicos por estado isActivo: " + esActivo.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return listaDeDocentesAcademicos;
        }

		/// <summary>
		/// Carga una <see cref="List{T}"/> de todos los <see cref="DocenteAcademico"/> que tengan el <see cref="DocenteAcademico.Rol"/> dado.
		/// </summary>
		/// <param name="rol"><see cref="DocenteAcademico.Rol"/> de los <see cref="DocenteAcademico"/> a cargar.</param>
		/// <returns>Una <see cref="List{T}"/> de todos los <see cref="DocenteAcademico"/> con el <see cref="DocenteAcademico.Rol"/> dado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<DocenteAcademico> CargarDocentesAcademicosPorRol(Rol rol)
        {
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroRol = new SqlParameter[1];

            parametroRol[0] = new SqlParameter
            {
                ParameterName = "@Rol",
                Value = rol
            };

            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect(QuerysDeDocenteAcademico.CARGAR_DOCENTES_ACADADEMICOS_POR_ROL, parametroRol);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, rol);
			}

			List<DocenteAcademico> listaDeDocentesAcademicos = new List<DocenteAcademico>();

            try
            {
                listaDeDocentesAcademicos = ConvertirDataTableAListaDeDocentesAcademicos(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de DocentesAcademicos en cargar DocentesAcademicos por rol: " + rol.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return listaDeDocentesAcademicos;
        }

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a un <see cref="DocenteAcademico"/>.
		/// </summary>
		/// <param name="tablaDocenteAcademico">La <see cref="DataTable"/> que contiene datos del <see cref="DocenteAcademico"/><./param>
		/// <returns>El <see cref="DocenteAcademico"/> contenido en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private DocenteAcademico ConvertirDataTableADocenteAcademico(DataTable tablaDocenteAcademico)
        {
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            DocenteAcademico docenteAcademico = new DocenteAcademico();

            foreach (DataRow fila in tablaDocenteAcademico.Rows)
            {
                docenteAcademico.IDPersonal = (int)fila["IDPersonal"];
                docenteAcademico.Nombre = fila["Nombre"].ToString();
                docenteAcademico.CorreoElectronico = fila["CorreoElectronico"].ToString();
                docenteAcademico.Telefono = fila["Telefono"].ToString();
                docenteAcademico.Contraseña = fila["Contraseña"].ToString();
                docenteAcademico.Carrera = fila["Carrera"].ToString();
                docenteAcademico.Cubiculo = (int)fila["Cubiculo"];
                docenteAcademico.EsActivo = (bool)fila["EsActivo"];
                docenteAcademico.Rol = (Rol)fila["Rol"];

				if (docenteAcademico.Rol == Rol.TecnicoAcademico)
				{
					docenteAcademico.Coordinador = new DocenteAcademico() { IDPersonal = (int)fila["IDCoordinador"] };
				}
				else
				{
					docenteAcademico.Coordinador = null;
				}
			}

            return docenteAcademico;
        }

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a un <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializado y sus demas atributos como null.
		/// </summary>
		/// <param name="tablaDocenteAcademico">La <see cref="DataTable"/> que contiene datos del <see cref="DocenteAcademico"/>.</param>
		/// <returns>El <see cref="DocenteAcademico"/> con solo <see cref="DocenteAcademico.IDPersonal"/> inicializado contenido en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private DocenteAcademico ConvertirDataTableADocenteAcademicoConSoloID(DataTable tablaDocenteAcademico)
        {
            DocenteAcademico docenteAcademico = new DocenteAcademico();

            foreach (DataRow fila in tablaDocenteAcademico.Rows)
            {
                docenteAcademico.IDPersonal = (int)fila["IDPersonal"];
            }

            return docenteAcademico;
        }

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{T}"/> de <see cref="DocenteAcademico"/>.
		/// </summary>
		/// <param name="tablaDocenteAcademico">La <see cref="DataTable"/> que contiene datos de los <see cref="DocenteAcademico"/>.</param>
		/// <returns>La <see cref="List{T}"/> de <see cref="DocenteAcademico"/> contenido en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<DocenteAcademico> ConvertirDataTableAListaDeDocentesAcademicos(DataTable tablaDocenteAcademico)
        {

            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            List<DocenteAcademico> listaDeDocentesAcademicos = new List<DocenteAcademico>();

            foreach (DataRow fila in tablaDocenteAcademico.Rows)
            {
                DocenteAcademico docenteAcademico = new DocenteAcademico
                {
                    IDPersonal = (int)fila["IDPersonal"],
                    Nombre = fila["Nombre"].ToString(),
                    CorreoElectronico = fila["CorreoElectronico"].ToString(),
                    Telefono = fila["Telefono"].ToString(),
                    Contraseña = fila["Contraseña"].ToString(),
                    Carrera = fila["Carrera"].ToString(),
                    Cubiculo = (int)fila["Cubiculo"],
                    EsActivo = (bool)fila["EsActivo"],
                    Rol = (Rol)fila["Rol"]
                };

				if (docenteAcademico.Rol == Rol.TecnicoAcademico)
				{
					docenteAcademico.Coordinador = new DocenteAcademico() { IDPersonal = (int)fila["IDCoordinador"] };
				}
				else
				{
					docenteAcademico.Coordinador = null;
				}

                listaDeDocentesAcademicos.Add(docenteAcademico);
            }

            return listaDeDocentesAcademicos;
        }

		/// <summary>
		/// Guarda un <see cref="DocenteAcademico"/> en la base de datos.
		/// </summary>
		/// <param name="docenteAcademico">El <see cref="DocenteAcademico"/> a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void GuardarDocenteAcademico(DocenteAcademico docenteAcademico)
        {
            SqlParameter[] parametrosDeDocenteAcademico = InicializarParametrosDeSql(docenteAcademico);
            int filasAfectadas = 0;

			try
			{
				filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeDocenteAcademico.GUARDAR_DOCENTE_ACADEMICO, parametrosDeDocenteAcademico);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, docenteAcademico);
			}

			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("DocenteAcademico: " + docenteAcademico.ToString() + "no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
        }

		/// <summary>
		/// Inicializa un arreglo de <see cref="SqlParameter"/> basado en un <see cref="DocenteAcademico"/>.
		/// </summary>
		/// <param name="docenteAcademico">El <see cref="DocenteAcademico"/> para inicializar los parametros.</param>
		/// <returns>Un arreglo de <see cref="SqlParameter"/> donde cada posición es uno de los atributos del <see cref="DocenteAcademico"/>.</returns>
		private SqlParameter[] InicializarParametrosDeSql(DocenteAcademico docenteAcademico)
        {
            SqlParameter[] parametrosDeDocenteAcademico = new SqlParameter[9];

            for (int i = 0; i < parametrosDeDocenteAcademico.Length; i++)
            {
                parametrosDeDocenteAcademico[i] = new SqlParameter();
            }

            parametrosDeDocenteAcademico[0].ParameterName = "@NombreDocenteAcademico";
            parametrosDeDocenteAcademico[0].Value = docenteAcademico.Nombre;
            parametrosDeDocenteAcademico[1].ParameterName = "@CorreoElectronicoDocenteAcademico";
            parametrosDeDocenteAcademico[1].Value = docenteAcademico.CorreoElectronico;
            parametrosDeDocenteAcademico[2].ParameterName = "@TelefonoDocenteAcademico";
            parametrosDeDocenteAcademico[2].Value = docenteAcademico.Telefono;
            parametrosDeDocenteAcademico[3].ParameterName = "@CarreraDocenteAcademico";
            parametrosDeDocenteAcademico[3].Value = docenteAcademico.Carrera;
            parametrosDeDocenteAcademico[4].ParameterName = "@EsActivoDocenteAcademico";
            parametrosDeDocenteAcademico[4].Value = docenteAcademico.EsActivo;
            parametrosDeDocenteAcademico[5].ParameterName = "@CubiculoDocenteAcademico";
            parametrosDeDocenteAcademico[5].Value = docenteAcademico.Cubiculo;
            parametrosDeDocenteAcademico[6].ParameterName = "@RolDocenteAcademico";
            parametrosDeDocenteAcademico[6].Value = (int)docenteAcademico.Rol;
            parametrosDeDocenteAcademico[7].ParameterName = "@ContraseñaDocenteAcademico";
            parametrosDeDocenteAcademico[7].Value = docenteAcademico.Contraseña;

            if (docenteAcademico.Rol.ToString() == "TecnicoAcademico")
            {
                parametrosDeDocenteAcademico[8].ParameterName = "@IDCoordinadorDocenteAcademico";
                parametrosDeDocenteAcademico[8].Value = docenteAcademico.Coordinador.IDPersonal;
            }
            else
            {
                parametrosDeDocenteAcademico[8].ParameterName = "@IDCoordinadorDocenteAcademico";
                parametrosDeDocenteAcademico[8].Value = 0;
            }

            return parametrosDeDocenteAcademico;
        }
    }
}
