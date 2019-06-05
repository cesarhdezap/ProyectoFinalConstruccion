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
	class DocenteAcademicoDAO : IDocenteAcademicoDAO
	{
		public void ActualizarDocenteAcademicoPorIDPersonal(int IDPersonal, DocenteAcademico docenteAcademico)
		{
            if (IDPersonal <= 0)
            {
                throw new AccesoADatosException("Error al Actualizar DocenteAcademico Por IDpersonal: " + IDPersonal + ". IDpersonal no es valido.");
            }
            SqlParameter[] parametrosDeDocenteAcademico = InicializarParametrosDeSql(docenteAcademico);
            try
            {
                AccesoADatos.EjecutarInsertInto("UPDATE DocentesAcademicos SET (Nombre = @NombreDocenteAcademico, CorreoElectronico = @CorreoElectronicoDocenteAcademico, Telefono = @TelefonoDocenteAcademico, EsActivo = @EsActivoDocenteAcademico)", parametrosDeDocenteAcademico);
            } catch (SqlException e)
            {
                throw new AccesoADatosException("Error al actualizar DocenteAcademico: " + docenteAcademico.ToString() + "Con ID: " + IDPersonal, e);
            }
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
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar DocenteAcademico por IDPersonal: " + IDPersonal, e);
            }

            DocenteAcademico docenteAcademico = new DocenteAcademico();
            try
            {
                docenteAcademico = ConvertirDataTableADocenteAcademico(tablaDeDocenteAcademico);
            }
            catch(FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocenteAcademico en cargar DocenteAcademico por IDPersonal: " + IDPersonal, e);
            }
            return docenteAcademico;
        }

        internal DocenteAcademico CargarIDPorIDDocumento(int IDDocumento)
        {
            if(IDDocumento <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDPersonal Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.");
            }
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroIDDocumento = new SqlParameter[1];
            parametroIDDocumento[0] = new SqlParameter
            {
                ParameterName = "@IDDocuemento",
                Value = IDDocumento
            };
            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("Query?", parametroIDDocumento);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDPersonal con IDDocumento: " + IDDocumento, e);
            }
            DocenteAcademico docenteAcademico = new DocenteAcademico();
            try
            {
                docenteAcademico = ConvertirDataTableADocenteAcademicoConSoloID(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a DocenteAcademico en cargar IDPersonal con IDDcumento: " + IDDocumento, e);
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
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar DocentesAcademicos por estado isActivo: " + isActivo.ToString(), e);
            }

            List<DocenteAcademico> listaDeDocentesAcademicos = new List<DocenteAcademico>();

            try
            {
                listaDeDocentesAcademicos = ConvertirDataTableAListaDeDocentesAcademicos(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir data table a lista de DocentesAcademicos en cargar DocentesAcademicos por estado isActivo: " + isActivo.ToString(), e);
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
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar DocentesAcademicos por rol: " + rol.ToString(), e);
            }

            List<DocenteAcademico> listaDeDocentesAcademicos = new List<DocenteAcademico>();

            try
            {
                listaDeDocentesAcademicos = ConvertirDataTableAListaDeDocentesAcademicos(tablaDeDocenteAcademico);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de DocentesAcademicos en cargar DocentesAcademicos por rol: " + rol.ToString(), e);
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
                docenteAcademico.Coordinador = new DocenteAcademico() { IDPersonal = (int)fila["IDCoordinador"] };
                docenteAcademico.Rol = (Rol)fila["Rol"];
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
                    Coordinador = new DocenteAcademico() { IDPersonal = (int)fila["IDCoordinador"] },
                    Rol = (Rol)fila["Rol"]
                };
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
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO DocentesAcademicos(Nombre, CorreoElectronico, Telefono, Carrera, EsActivo, Cubiculo, Rol, Contraseña) VALUES (@NombreDocenteAcademico, @CorreoElectronicoDocenteAcademico, @TelefonoDocenteAcademico, @CarreraDocenteAcademico, @oEsActivoDocenteAcademic, @CubiculoDocenteAcademico, @RolDocenteAcademico, @ContraseñaDocenteAcademico, @IDCoordinadorDocenteAcademico)", parametrosDeDocenteAcademico);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar DocenteAcademico:" + docenteAcademico.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("DocenteAcademico: " + docenteAcademico.ToString() + "no fue guardado.");
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
                parametrosDeDocenteAcademico[8].Value = null;
            }

            return parametrosDeDocenteAcademico;
        }
	}
}
