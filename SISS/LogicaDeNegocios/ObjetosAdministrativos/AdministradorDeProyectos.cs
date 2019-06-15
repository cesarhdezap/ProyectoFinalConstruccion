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
            Proyectos = proyectoDAO.CargarProyectosTodos();
		}

        public void CargarProyectosPorEstado(EstadoProyecto estadoProyecto)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Proyectos = proyectoDAO.CargarProyectosPorEstado(estadoProyecto);
        }

		public List<string> ObtenerNombresDeProyectos()
		{
			List<string> listaDeNombres = new List<string>();
			foreach (Proyecto proyecto in Proyectos)
			{
				listaDeNombres.Add(proyecto.Nombre);
			}
			return listaDeNombres;
		}
    }
}
