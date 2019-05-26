using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocenteAcademicoDAO : Interfaces.IDocenteAcademicoDAO
	{
		public void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico)
		{
			//TODO
			throw new NotImplementedException();
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
            SqlParameter[] parametroIsActivo = new SqlParameter[1];
            parametroIsActivo[0] = new SqlParameter();
            parametroIsActivo[0].ParameterName = "@isActivo";
            parametroIsActivo[0].Value = isActivo;
            try
            {
                tablaDeDocenteAcademico = AccesoADatos.EjecutarSelect("SELECT * FROM DocentesAcademicos WHERE isActivo = @isActivo", parametroIsActivo);
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
                                                                       //Coordinador = Deberia haber un cargar ID por subordinado?
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
                                                                       //Coordinador = Deberia haber un cargar ID por subordinado?
                                                                       Rol = (Rol)fila["Rol"]
                                                                   }
                                                                  ).ToList();
            return docentesAcademicos;
        }
        
        private DataTable ConvertirDocenteAcademicoADataTable(DocenteAcademico docenteAdministrativo)
		{
			//TODO
			throw new NotImplementedException();
		}

		public void GuardarDocenteAcademico(DocenteAcademico docenteAdministrativo)
		{
			//TODO
			throw new NotImplementedException();
		}
	}
}
