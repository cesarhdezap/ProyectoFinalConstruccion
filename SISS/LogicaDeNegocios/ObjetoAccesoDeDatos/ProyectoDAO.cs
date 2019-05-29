using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using System.Linq;
using static LogicaDeNegocios.Proyecto;
using System.Reflection;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class ProyectoDAO : Interfaces.IProyectoDAO
	{
        public void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto)
		{
			//TODO
			throw new NotImplementedException();
		}

        public List<Proyecto> CargarIDsPorIDEncargado(int IDEncargado){
            DataTable tablaDeProyectos = new DataTable();
            SqlParameter[] parametroIDEncargado = new SqlParameter[1];
            parametroIDEncargado[0] = new SqlParameter();
            parametroIDEncargado[0].ParameterName = "@IDEncargado";
            parametroIDEncargado[0].Value = IDEncargado;
            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect("SELECT IDProyectos FROM Proyectos WHERE IDEncargado = @IDEncargado");
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }
            List<Proyecto> listaDeProyectos = new List<Proyecto>();

            listaDeProyectos = ConvertirDataTableAListaDeProyectos(tablaDeProyectos);

            return listaDeProyectos;
        }

        public Proyecto CargarProyectoPorID(int IDProyecto){
            DataTable tablaDeProyecto = new DataTable();
            SqlParameter[] parametroIDProyecto = new SqlParameter[1];
            parametroIDProyecto[0] = new SqlParameter();
            parametroIDProyecto[0].ParameterName = "@IDProyecto";
            parametroIDProyecto[0].Value = IDProyecto;

            try
            {
                tablaDeProyecto = AccesoADatos.EjecutarSelect("SELECT * FROM Proyectos WHERE IDProyecto = @IDProyecto", parametroIDProyecto);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            Proyecto proyectos = new Proyecto();

            proyectos = ConvertirDataTableAProyecto(tablaDeProyecto);

            return proyectos;
        }

        public List<Proyecto> CargarProyectosPorEstado(EstadoProyecto estadoDeProyecto){
            DataTable tablaDeProyectos = new DataTable();
            SqlParameter[] parametroEstadoDeProyecto = new SqlParameter[1];
            parametroEstadoDeProyecto[0] = new SqlParameter();
            parametroEstadoDeProyecto[0].ParameterName = "@estadoDeProyecto";
            parametroEstadoDeProyecto[0].Value = estadoDeProyecto;

            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect("SELECT * FROM Proyectos WHERE EstadoDeProyecto = @estadoDeProyectos", parametroEstadoDeProyecto);
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            List<Proyecto> proyectos = new List<Proyecto>();

            proyectos = ConvertirDataTableAListaDeProyectos(tablaDeProyectos);

            return proyectos;
        }

        public List<Proyecto> CargarProyectosTodos(){
            DataTable tablaDeProyectos = new DataTable();

            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect("SELECT * FROM Proyectos");
            }
            catch (SqlException ExcepcionSQL)
            {
                Console.Write(" \nExcepcion: " + ExcepcionSQL.StackTrace.ToString());
            }

            List<Proyecto> proyectos = new List<Proyecto>();

            proyectos = ConvertirDataTableAListaDeProyectos(tablaDeProyectos);

            return proyectos;
        }

        public Proyecto CargarIDProyectoPorIDAsignacion(int IDAsignacion)
        {
            //TODO
            throw new NotImplementedException();
        }
        private List<Proyecto> ConvertirDataTableAListaDeProyectos (DataTable tablaDeProyectos){
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            List<Proyecto> proyectos = (from DataRow fila in tablaDeProyectos.Rows
                                        select new Proyecto()
                                        {
                                            IDProyecto = (int)fila["IDProyecto"],
                                            Nombre = fila["Nombre"].ToString(),
                                            DescripcionGeneral = fila["DecripcionGeneral"].ToString(),
                                            ObjetivoGeneral = fila["ObjetivoGeneral"].ToString(),
                                            Cupo = (int)fila["Cupo"],
                                            Asignaciones = asignacionDAO.CargarIDsPorIDProyecto((int)fila["IDProyecto"])
                                        }
                                       ).ToList();
            return proyectos;
        }

        private Proyecto ConvertirDataTableAProyecto (DataTable tablaDeProyecto){
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Proyecto proyecto = (Proyecto)(from DataRow fila in tablaDeProyecto.Rows
                                           select new Proyecto()
                                           {
                                               IDProyecto = (int)fila["IDProyecto"],
                                               Nombre = fila["Nombre"].ToString(),
                                               DescripcionGeneral = fila["DecripcionGeneral"].ToString(),
                                               ObjetivoGeneral = fila["ObjetivoGeneral"].ToString(),
                                               Cupo = (int)fila["Cupo"],
                                               Asignaciones = asignacionDAO.CargarIDsPorIDProyecto((int)fila["IDProyecto"])
                                           }
                                          );
            return proyecto;
		}

        private DataTable ConvertirProyectoADataTable (Proyecto proyecto){
            DataTable tablaDeProyecto = new DataTable();
            foreach (PropertyInfo property in proyecto.GetType().GetProperties())
            {
                if (property.PropertyType != typeof(List<>))
                { 
                    tablaDeProyecto.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                }
            }

            DataRow contactRow = tablaDeProyecto.NewRow();

            foreach (PropertyInfo property in proyecto.GetType().GetProperties())
            {
                if (property.PropertyType != typeof(List<>))
                {
                    contactRow[property.Name] = proyecto.GetType().GetProperty(property.Name).GetValue(proyecto, null);
                }
            }
            tablaDeProyecto.Rows.Add(contactRow);
            return tablaDeProyecto;
        }

        public int GuardarProyecto(Proyecto proyecto)
        { 
			//TODO
			throw new NotImplementedException();
		}
    }
}
