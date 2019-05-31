using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocenteAcademicoDAO : IDocenteAcademicoDAO
	{
		public void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico)
		{
            SqlParameter[] parametrosDeDocenteAcademico = InicializarParametrosDeSql(docenteAcademico);

            try
            {
                AccesoADatos.EjecutarInsertInto("UPDATE DocentesAcademicos SET (Nombre = @NombreDocenteAcademico, CorreoElectronico = @CorreoElectronicoDocenteAcademico, Telefono = @TelefonoDocenteAcademico, EsActivo = @EsActivoDocenteAcademico)", parametrosDeDocenteAcademico);
            } catch (SqlException e)
            {

            }

		}

		public DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDpersonal)
		{
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroIDPersonal = new SqlParameter[1];
            parametroIDPersonal[0] = new SqlParameter();
            parametroIDPersonal[0].ParameterName = "@IDPersonal";
            parametroIDPersonal[0].Value = IDpersonal;
            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT * FROM DocentesAcademicos WHERE IDPersonal = @IDPersonal", parametroIDPersonal);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            DocenteAcademico docenteAcademico = new DocenteAcademico();
            docenteAcademico = ConvertirDataTableADocenteAcademico(tablaDeDocenteAcademico);
            return docenteAcademico;
        }

        public List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo)
        {
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroEsActivo = new SqlParameter[1];
            parametroEsActivo[0] = new SqlParameter();
            parametroEsActivo[0].ParameterName = "@EsActivo";
            parametroEsActivo[0].Value = isActivo;
            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT * FROM DocentesAcademicos WHERE EsActivo = @EsActivo", parametroEsActivo);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            List<DocenteAcademico> docentesAcademicos = new List<DocenteAcademico>();
            docentesAcademicos = ConvertirDataTableAListaDeDocentesAcademicos(tablaDeDocenteAcademico);
            return docentesAcademicos;
        }

		public List<DocenteAcademico> CargarDocentesAcademicosPorRol(Rol rol)
		{
            DataTable tablaDeDocenteAcademico = new DataTable();
            SqlParameter[] parametroRol = new SqlParameter[1];
            parametroRol[0] = new SqlParameter();
            parametroRol[0].ParameterName = "@Rol";
            parametroRol[0].Value = rol;
            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT * FROM DocentesAcademicos WHERE Rol = @Rol", parametroRol);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            List<DocenteAcademico> docentesAcademicos = new List<DocenteAcademico>();
            docentesAcademicos = ConvertirDataTableAListaDeDocentesAcademicos(tablaDeDocenteAcademico);
            return docentesAcademicos;
        }

		private DocenteAcademico ConvertirDataTableADocenteAcademico(DataTable tablaDocenteAcademico)
        {
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            DocenteAcademico docenteAcademico = (DocenteAcademico)(from DataRow fila in tablaDocenteAcademico.Rows
                                                                   select new DocenteAcademico()
                                                                   {
                                                                       IDPersonal = (int)fila["IDPersonal"],
                                                                       Carrera = fila["Carrera"].ToString(),
                                                                       Cubiculo = (int)fila["Cubiculo"],
                                                                       EsActivo = (bool)fila["EsActivo"],
                                                                       Coordinador = new DocenteAcademico() { IDPersonal = (int)fila["IDCoordinador"] },
                                                                       Rol = (Rol)fila["Rol"]
                                                                   }
                                                                  );
            return docenteAcademico;
        }
        
        private List<DocenteAcademico> ConvertirDataTableAListaDeDocentesAcademicos(DataTable tablaDocenteAcademico)
        {

            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            List<DocenteAcademico> docentesAcademicos = (from DataRow fila in tablaDocenteAcademico.Rows
                                                                   select new DocenteAcademico()
                                                                   {
                                                                       IDPersonal = (int)fila["IDPersonal"],
                                                                       Carrera = fila["Carrera"].ToString(),
                                                                       Cubiculo = (int)fila["Cubiculo"],
                                                                       EsActivo = (bool)fila["EsActivo"],
                                                                       Coordinador = new DocenteAcademico() { IDPersonal = (int)fila["IDCoordinador"] },
                                                                       Rol = (Rol)fila["Rol"]
                                                                   }
                                                                  ).ToList();
            return docentesAcademicos;
        }

		public void GuardarDocenteAcademico(DocenteAcademico docenteAcademico)
		{
            SqlParameter[] parametrosDeDocenteAcademico = InicializarParametrosDeSql(docenteAcademico);
            try
            {
                AccesoADatos.EjecutarInsertInto("INSERT INTO DocentesAcademicos(Nombre, CorreoElectronico, Telefono, Carrera, EsActivo, Cubiculo, Rol, Contraseña) VALUES (@NombreDocenteAcademico, @CorreoElectronicoDocenteAcademico, @TelefonoDocenteAcademico, @CarreraDocenteAcademico, @oEsActivoDocenteAcademic, @CubiculoDocenteAcademico, @RolDocenteAcademico, @ContraseñaDocenteAcademico, @IDCoordinadorDocenteAcademico)", parametrosDeDocenteAcademico);
            } catch (SqlException e){

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
            parametrosDeDocenteAcademico[6].Value = docenteAcademico.Rol.ToString();
            parametrosDeDocenteAcademico[7].ParameterName = "@ContraseñaDocenteAcademico";
            parametrosDeDocenteAcademico[7].Value = docenteAcademico.Contraseña;

            if (docenteAcademico.Rol.ToString() == "TecnicoAcademico")
            {
                parametrosDeDocenteAcademico[8].ParameterName = "@IDCoordinadorDocenteAcademico";
                parametrosDeDocenteAcademico[8].Value = docenteAcademico.Coordinador.IDPersonal;
            } else
            {
                parametrosDeDocenteAcademico[8].ParameterName = "@IDCoordinadorDocenteAcademico";
                parametrosDeDocenteAcademico[8].Value = null;
            }

            return parametrosDeDocenteAcademico;
        }
	}
}
