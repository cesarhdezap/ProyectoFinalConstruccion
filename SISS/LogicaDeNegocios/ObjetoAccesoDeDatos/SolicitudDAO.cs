using System;
using System.Data;
using System.Collections.Generic;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class SolicitudDAO : ISolicitudDAO
	{
		public Solicitud CargarSolicitudPorID(int IDSolicitud)
		{
            if (IDSolicitud <= 0)
            {
                throw new AccesoADatosException("Error al Actualizar Solicitud Por IDSolicitud: " + IDSolicitud + ". IDSolicitud no es valido.");
            }
            DataTable tablaDeMatricula = new DataTable();
            SqlParameter[] parametroIDSolicitud = new SqlParameter[1];
            parametroIDSolicitud[0] = new SqlParameter
            {
                ParameterName = "@IDSolicitud",
                Value = IDSolicitud
            };

            try
            {
                tablaDeMatricula = AccesoADatos.EjecutarSelect("SELECT * FROM Solicitudes WHERE IDSolicitud = @IDSolicitud", parametroIDSolicitud);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Solicitud con IDSolicitud: " + IDSolicitud, e);
            }
            Solicitud solicitud = new Solicitud();
            try
            {
                solicitud = ConvertirDataTableASolicutud(tablaDeMatricula);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Solicitud en cargar Solicitud con IDSolicitud: " + IDSolicitud, e);

            }
            return solicitud;

        }

        
        private Solicitud ConvertirDataTableASolicutud(DataTable tablaDeSolicitud)
		{
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Solicitud solicitud = new Solicitud();
            foreach (DataRow fila in tablaDeSolicitud.Rows)
            {
                solicitud.IDSolicitud = (int)fila["IDSolicitud"];
                solicitud.Fecha = (DateTime)fila["Fecha"];
                solicitud.Proyectos = proyectoDAO.CargarIDProyectosPorIDSolicitud((int)fila["IDSolicitud"]);
            }
            return solicitud;
        }

        private Solicitud ConvertirDataTableASolicutudConSoloID(DataTable tablaDeSolicitud)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Solicitud solicitud = new Solicitud();
            foreach (DataRow fila in tablaDeSolicitud.Rows)
            {
                solicitud.IDSolicitud = (int)fila["IDSolicitud"];
            }
            return solicitud;
        }

        public void GuardarSolicitud(Solicitud solicitud)
        {
            SqlParameter[] parametrosDeSolicitud = InicializarParametrosDeSql(solicitud);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Solicitudes(Fecha) VALUES(@Fecha)", parametrosDeSolicitud);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar solicitud: " + solicitud.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Solicitud: " + solicitud.ToString() + " no fue guardada.");
            }
            foreach (Proyecto proyecto in solicitud.Proyectos)
            {
                parametrosDeSolicitud[1].Value = ObtenerUltimoIDInsertado();
                parametrosDeSolicitud[2].Value = proyecto.IDProyecto;
                try
                {
                   filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Solicitudes-Proyectos(IDSolicitud, IDProyecto) VALUES(@IDSolicitud, @IDProyecto)", parametrosDeSolicitud);
                }
                catch (SqlException e)
                {
                    throw new AccesoADatosException("Error al guardar relacion Proyectos-Solicitud: " + solicitud.ToString() + proyecto.ToString(), e);
                }
                if (filasAfectadas <= 0)
                {
                    throw new AccesoADatosException("Relacion Solicitud - Proyecto: " + solicitud.ToString() + proyecto.ToString() + " no fue guardada.");
                }
            }
        }

        private static SqlParameter[] InicializarParametrosDeSql(Solicitud solicitud)
        {
            SqlParameter[] parametrosDeSolicitud = new SqlParameter[3];
            for (int i = 0; i < parametrosDeSolicitud.Length; i++)
            {
                parametrosDeSolicitud[i] = new SqlParameter();
            }

            parametrosDeSolicitud[0].ParameterName = "@Fecha";
            parametrosDeSolicitud[0].Value = solicitud.Fecha.ToString();
            parametrosDeSolicitud[1].ParameterName = "@IDSolicitud";
            parametrosDeSolicitud[2].ParameterName = "@IDProyecto";

            return parametrosDeSolicitud;
        }

        public Solicitud CargarIDPorIDAsignacion(int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDSolicitud Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }
            DataTable tablaDeSolicitud = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };

            try
            {
                tablaDeSolicitud = AccesoADatos.EjecutarSelect("Query", parametroIDAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDSolicitud con IDAsignacion: " + IDAsignacion, e);
            }
            Solicitud solicitud = new Solicitud();
            try
            {
                solicitud = ConvertirDataTableASolicutudConSoloID(tablaDeSolicitud);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Solicitud en cargar IDSolicitud con IDAsignacion: " + IDAsignacion, e);
            }
            return solicitud;
        }
    }
}
