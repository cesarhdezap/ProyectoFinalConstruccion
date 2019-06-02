using System;
using System.Data;
using System.Collections.Generic;
using LogicaDeNegocios.Interfaces;
using System.Data.SqlClient;
using AccesoABaseDeDatos;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class SolicitudDAO : ISolicitudDAO
	{
		public Solicitud CargarSolicitudPorID(int IDSolicitud)
		{
            DataTable tablaDeMatricula = new DataTable();
            SqlParameter[] parametroIDSolicitud = new SqlParameter[1];
            parametroIDSolicitud[0] = new SqlParameter();
            parametroIDSolicitud[0].ParameterName = "@IDSolicitud";
            parametroIDSolicitud[0].Value = IDSolicitud;

            try
            {
                tablaDeMatricula = AccesoADatos.EjecutarSelect("SELECT * FROM Solicitudes WHERE IDSolicitud = @IDSolicitud", parametroIDSolicitud);
            }
            catch (SqlException e)
            {
                Console.Write(" \nExcepcion: " + e.StackTrace.ToString());
            }

            Solicitud solicitud = ConvertirDataTableASolicutud(tablaDeMatricula);

            return solicitud;

        }

        
        private Solicitud ConvertirDataTableASolicutud(DataTable tablaDeSolicitud)
		{
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Solicitud solicitud = (Solicitud)(from DataRow fila in tablaDeSolicitud.Rows
                                              select new Solicitud()
                                              {
                                                  IDSolicitud = (int)fila["IDSolicitud"],
                                                  Fecha = (DateTime)fila["Fecha"],
                                                  Proyectos = proyectoDAO.CargarIDProyectosPorIDSolicitud((int)fila["IDSolicitud"])
                                              }
                           );
            return solicitud;
        }
        
        public void GuardarSolicitud(Solicitud solicitud)
        {
            SqlParameter[] parametrosDeSolicitud = new SqlParameter[3];

            for (int i = 0; i < parametrosDeSolicitud.Length; i++)
            {
                parametrosDeSolicitud[i] = new SqlParameter();
            }

            parametrosDeSolicitud[0].ParameterName = "@Fecha";
            parametrosDeSolicitud[0].Value = solicitud.Fecha.ToString();
            parametrosDeSolicitud[1].ParameterName = "@IDSolicitud";
            //Como obtengo el valor de ID autoincremental del ultimo ID insertado?
            //parametrosDeSolicitud[1].Value = ???????;
            parametrosDeSolicitud[2].ParameterName = "@IDProyecto";
            try
            {
                AccesoADatos.EjecutarInsertInto("INSERT INTO Solicitudes(Fecha) VALUES(@Fecha)", parametrosDeSolicitud);
            } catch (SqlException e)
            {
                //Definir tipo de excepcion 
                //throw new ??????(e);
            }
            foreach (Proyecto proyecto in solicitud.Proyectos)
            {
                parametrosDeSolicitud[2].Value = proyecto.IDProyecto;
                AccesoADatos.EjecutarInsertInto("INSERT INTO SolicitudProyecto(IDSolicitud, IDProyecto) VALUES(@IDSolicitud, @IDProyecto)", parametrosDeSolicitud);
            }
        }

        public Solicitud CargarIDPorIDAsignacion(int IDAsignacion)
        {
            throw new NotImplementedException();
        }
    }
}
