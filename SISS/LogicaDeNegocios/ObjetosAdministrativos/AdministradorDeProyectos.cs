using System;
using System.Collections.Generic;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeProyectos
    {
        public List<Proyecto> Proyectos { get; set; }

        public void CargarProyectosTodos()
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            this.Proyectos = proyectoDAO.CargarProyectosTodos();
		}
        public void CargarProyectosPorEstado(EstadoProyecto estadoProyecto)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            this.Proyectos = proyectoDAO.CargarProyectosPorEstado(estadoProyecto);
        }

        public void CrearProyecto (Proyecto proyecto)
        {
			//TODO
			throw new NotImplementedException();
		}
    }
}
