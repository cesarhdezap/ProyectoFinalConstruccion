using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using System.Linq;
using System.Reflection;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class ProyectoDAO : IProyectoDAO
	{
        public void ActualizarProyectoPorID(int IDProyecto, Proyecto proyecto)
		{
            if (IDProyecto <= 0)
            {
                throw new AccesoADatosException("Error al Actualizar Proyecto Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.");
            }
            SqlParameter[] parametrosDrProyecto = InicializarParametrosDeSql(proyecto);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("UPDATE Proyectos SET Estado = @EstadoProyecto WHERE IDProyecto = @IDProyecto", parametrosDrProyecto);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al actualizar Proyecto: " + proyecto.ToString() + "Con IDProyecto: " + IDProyecto, e);
            }

            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El Proyecto con IDProyecto: " + IDProyecto + " no existe.");
            }
        }

        public List<Proyecto> CargarIDsPorIDEncargado(int IDEncargado){
            if (IDEncargado <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDsProyecto Por IDEncargado: " + IDEncargado + ". IDEncargado no es valido.");
            }
            DataTable tablaDeProyectos = new DataTable();
            SqlParameter[] parametroIDEncargado = new SqlParameter[1];
            parametroIDEncargado[0] = new SqlParameter
            {
                ParameterName = "@IDEncargado",
                Value = IDEncargado
            };
            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect("SELECT IDProyectos FROM Proyectos WHERE IDEncargado = @IDEncargado",parametroIDEncargado);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDsProyecto con IDEncargado: " + IDEncargado, e);
            }
            List<Proyecto> listaDeProyectos = new List<Proyecto>();
            try
            {
                listaDeProyectos = ConvertirDataTableAListaDeProyectosConSoloID(tablaDeProyectos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto en cargar IDsProyecto con IDEncargado: " + IDEncargado, e);
            }
            return listaDeProyectos;
        }

        public Proyecto CargarProyectoPorID(int IDProyecto){
            if (IDProyecto <= 0)
            {
                throw new AccesoADatosException("Error al cargar Proyecto Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.");
            }
            DataTable tablaDeProyecto = new DataTable();
            SqlParameter[] parametroIDProyecto = new SqlParameter[1];
            parametroIDProyecto[0] = new SqlParameter
            {
                ParameterName = "@IDProyecto",
                Value = IDProyecto
            };

            try
            {
                tablaDeProyecto = AccesoADatos.EjecutarSelect("SELECT * FROM Proyectos WHERE IDProyecto = @IDProyecto", parametroIDProyecto);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Proyecto con IDProyecto: " + IDProyecto, e);
            }
            Proyecto proyectos = new Proyecto();
            try
            {
                proyectos = ConvertirDataTableAProyecto(tablaDeProyecto);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto en cargar Proyecto con IDProyecto: " + IDProyecto, e);
            }
            return proyectos;
        }

        internal List<Proyecto> CargarIDProyectosPorIDSolicitud(int v)
        {
            throw new NotImplementedException();
        }

        public List<Proyecto> CargarProyectosPorEstado(Proyecto.EstadoProyecto estadoDeProyecto){
            DataTable tablaDeProyectos = new DataTable();
            SqlParameter[] parametroEstadoDeProyecto = new SqlParameter[1];
            parametroEstadoDeProyecto[0] = new SqlParameter
            {
                ParameterName = "@estadoDeProyecto",
                Value = (int)estadoDeProyecto
            };

            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect("SELECT * FROM Proyectos WHERE EstadoDeProyecto = @estadoDeProyectos", parametroEstadoDeProyecto);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Proyectos con estado: " + estadoDeProyecto.ToString(), e);
            }   

            List<Proyecto> proyectos = new List<Proyecto>();
            try
            {
                proyectos = ConvertirDataTableAListaDeProyectos(tablaDeProyectos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto en cargar Proyectos con estado: " + estadoDeProyecto.ToString(), e);
            }
            return proyectos;
        }

        public List<Proyecto> CargarProyectosTodos()
        {
            DataTable tablaDeProyectos = new DataTable();
            try
            {
                tablaDeProyectos = AccesoADatos.EjecutarSelect("SELECT * FROM Proyectos");
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar todos los Proyectos", e);
            }
            List<Proyecto> proyectos = new List<Proyecto>();
            try
            {
                proyectos = ConvertirDataTableAListaDeProyectos(tablaDeProyectos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Proyectos en cargar todos los Proyectos", e);
            }
            return proyectos;
        }

        public Proyecto CargarIDProyectoPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar Proyecto Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }
            DataTable tablaDeProyecto = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };
            try
            {
                tablaDeProyecto = AccesoADatos.EjecutarSelect("Query?", parametroIDAsignacion);
            } 
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar ID de Proyecto con IDAsignacion: " + IDAsignacion, e);
            }
            Proyecto proyecto = new Proyecto();
            try
            {
                proyecto = ConvertirDataTableAProyectoConSoloID(tablaDeProyecto);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Proyecto cargar Proyecto con IDAsignacion: " + IDAsignacion, e);
            }

            return proyecto;
        }
        private List<Proyecto> ConvertirDataTableAListaDeProyectos (DataTable tablaDeProyectos){
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            List<Proyecto> listaDeProyectos = new List<Proyecto>();
            foreach (DataRow fila in tablaDeProyectos.Rows)
            {
                Proyecto proyecto = new Proyecto()
                {
                    IDProyecto = (int)fila["IDProyecto"],
                    Nombre = fila["Nombre"].ToString(),
                    DescripcionGeneral = fila["DecripcionGeneral"].ToString(),
                    ObjetivoGeneral = fila["ObjetivoGeneral"].ToString(),
                    Cupo = (int)fila["Cupo"],
                    Asignaciones = asignacionDAO.CargarIDsPorIDProyecto((int)fila["IDProyecto"])
                };
                listaDeProyectos.Add(proyecto);
            }
            return listaDeProyectos;
        }

        private List<Proyecto> ConvertirDataTableAListaDeProyectosConSoloID(DataTable tablaDeProyectos)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            List<Proyecto> listaDeProyectos = new List<Proyecto>();
            foreach (DataRow fila in tablaDeProyectos.Rows)
            {
                Proyecto proyecto = new Proyecto()
                {
                    IDProyecto = (int)fila["IDProyecto"],
                };
                listaDeProyectos.Add(proyecto);
            }
            return listaDeProyectos;
        }

        private Proyecto ConvertirDataTableAProyecto (DataTable tablaDeProyecto){
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Proyecto proyecto = new Proyecto();
            foreach (DataRow fila in tablaDeProyecto.Rows)
            {
                proyecto.IDProyecto = (int)fila["IDProyecto"];
                proyecto.Nombre = fila["Nombre"].ToString();
                proyecto.DescripcionGeneral = fila["DecripcionGeneral"].ToString();
                proyecto.ObjetivoGeneral = fila["ObjetivoGeneral"].ToString();
                proyecto.Cupo = (int)fila["Cupo"];
                proyecto.Asignaciones = asignacionDAO.CargarIDsPorIDProyecto((int)fila["IDProyecto"]);
            }
            return proyecto;
		}
        private Proyecto ConvertirDataTableAProyectoConSoloID(DataTable tablaDeProyecto)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Proyecto proyecto = new Proyecto();
            foreach (DataRow fila in tablaDeProyecto.Rows)
            {
                proyecto.IDProyecto = (int)fila["IDProyecto"];
            }
            return proyecto;
        }

        public void GuardarProyecto(Proyecto proyecto)
        {
            SqlParameter[] parametrosDeProyecto = InicializarParametrosDeSql(proyecto);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Proyectos(Nombre, Estado, DescripcionGeneral, ObjetivoGeneral, Cupo, IDEncargado) VALUES(@NombreProyecto, @EstadoProyecto, @DescripcionGeneralProyecto, @ObjetivoGeneralProyecto, @CupoProyecto, @IDEncargado)", parametrosDeProyecto);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar Proyecto:" + proyecto.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Asignacion: " + proyecto.ToString() + " no fue guardado.");
            }
        }

        private SqlParameter[] InicializarParametrosDeSql(Proyecto proyecto)
        {
            SqlParameter[] parametrosDeProyecto = new SqlParameter[7];

            for (int i = 0; i < parametrosDeProyecto.Length; i++)
            {
                parametrosDeProyecto[i] = new SqlParameter();
            }
            parametrosDeProyecto[0].ParameterName = "@IDProyecto";
            parametrosDeProyecto[0].Value = proyecto.IDProyecto;
            parametrosDeProyecto[1].ParameterName = "@NombreProyecto";
            parametrosDeProyecto[1].Value = proyecto.Nombre;
            parametrosDeProyecto[2].ParameterName = "@EstadoProyecto";
            parametrosDeProyecto[2].Value = proyecto.Estado.ToString();
            parametrosDeProyecto[3].ParameterName = "@DescripcionGeneralProyecto";
            parametrosDeProyecto[3].Value = proyecto.DescripcionGeneral;
            parametrosDeProyecto[4].ParameterName = "@ObjetivoGeneralProyecto";
            parametrosDeProyecto[4].Value = proyecto.ObjetivoGeneral;
            parametrosDeProyecto[5].ParameterName = "@CupoProyecto";
            parametrosDeProyecto[5].Value = proyecto.Cupo;
            parametrosDeProyecto[6].ParameterName = "@IDEncargado";
            parametrosDeProyecto[6].Value = proyecto.Encargado.IDEncargado;

            return parametrosDeProyecto;
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}
