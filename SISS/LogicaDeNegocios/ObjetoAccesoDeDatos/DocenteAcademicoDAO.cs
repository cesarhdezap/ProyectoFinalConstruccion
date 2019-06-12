using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LogicaDeNegocios.Excepciones;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
    public class DocenteAcademicoDAO : IDocenteAcademicoDAO
    {
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
                filasAfectadas = AccesoADatos.EjecutarInsertInto("UPDATE DocentesAcademicos SET (Nombre = @NombreDocenteAcademico, CorreoElectronico = @CorreoElectronicoDocenteAcademico, Telefono = @TelefonoDocenteAcademico, EsActivo = @EsActivoDocenteAcademico)", parametrosDeDocenteAcademico);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al actualizar DocenteAcademico: " + docenteAcademico.ToString() + "Con ID: " + IDPersonal, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al actualizar DocenteAcademico: " + docenteAcademico.ToString() + "Con ID: " + IDPersonal, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			if (filasAfectadas <= 0)
			{
				throw new AccesoADatosException("El DocenteAcademico con ID: " + IDPersonal + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
			}
		}

        public DocenteAcademico CargarIDPorCarrera(string carrera)
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
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT IDPersonal FROM DocentesAcademicos WHERE Carrera = @Carrera", parametroCarrera);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al cargar IDPersonal con Carrera: " + carrera, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar IDPersonal con Carrera: " + carrera, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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
                tablaDeID = AccesoADatos.EjecutarSelect("SELECT IDPersonal FROM DocentesAcademicos WHERE CorreoElectronico = @CorreoElectronico AND Rol = @Rol", parametros);
            }
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
            {
                throw new AccesoADatosException("Error al cargar ID por CorreoElectronico: " + correoElectronico, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar ID por CorreoElectronico: " + correoElectronico, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT * FROM DocentesAcademicos WHERE IDPersonal = @IDPersonal", parametroIDPersonal);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al cargar DocenteAcademico por IDPersonal: " + IDPersonal, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar DocenteAcademico por IDPersonal: " + IDPersonal, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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

        public DocenteAcademico CargarIDPorIDDocumento(int IDDocumento)
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
				tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT IDDocenteAcademico FROM ReportesMensuales WHERE IDDocumento = @IDDocumento", parametroIDDocumento);
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar IDPersonal con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar IDPersonal con IDDocumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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
	

		public List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo)
        {
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroEsActivo = new SqlParameter[1];
            parametroEsActivo[0] = new SqlParameter
            {
                ParameterName = "@EsActivo",
                Value = isActivo
            };
            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT * FROM DocentesAcademicos WHERE EsActivo = @EsActivo", parametroEsActivo);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al cargar DocentesAcademicos por estado isActivo: " + isActivo.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar DocentesAcademicos por estado isActivo: " + isActivo.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			List<DocenteAcademico> listaDeDocentesAcademicos = new List<DocenteAcademico>();
            try
            {
                listaDeDocentesAcademicos = ConvertirDataTableAListaDeDocentesAcademicos(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir data table a lista de DocentesAcademicos en cargar DocentesAcademicos por estado isActivo: " + isActivo.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
            return listaDeDocentesAcademicos;
        }

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
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT * FROM DocentesAcademicos WHERE Rol = @Rol", parametroRol);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al cargar DocentesAcademicos por rol: " + rol.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al cargar DocentesAcademicos por rol: " + rol.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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

        private DocenteAcademico ConvertirDataTableADocenteAcademicoConSoloID(DataTable tablaDocenteAcademico)
        {
            DocenteAcademico docenteAcademico = new DocenteAcademico();
            foreach (DataRow fila in tablaDocenteAcademico.Rows)
            {
                docenteAcademico.IDPersonal = (int)fila["IDPersonal"];
            }
            return docenteAcademico;
        }

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

        public void GuardarDocenteAcademico(DocenteAcademico docenteAcademico)
        {
            SqlParameter[] parametrosDeDocenteAcademico = InicializarParametrosDeSql(docenteAcademico);
            int filasAfectadas = 0;
			try
			{
				filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO DocentesAcademicos(Nombre, Rol, IDCoordinador, CorreoElectronico, Telefono, Carrera, EsActivo, Cubiculo, Contraseña) VALUES (@NombreDocenteAcademico, @RolDocenteAcademico, @IDCoordinadorDocenteAcademico, @CorreoElectronicoDocenteAcademico, @TelefonoDocenteAcademico, @CarreraDocenteAcademico, @EsActivoDocenteAcademico, @CubiculoDocenteAcademico, @ContraseñaDocenteAcademico)", parametrosDeDocenteAcademico);
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al guardar DocenteAcademico:" + docenteAcademico.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al guardar DocenteAcademico:" + docenteAcademico.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("DocenteAcademico: " + docenteAcademico.ToString() + "no fue guardado.", TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }
        }

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
