using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Encargado : Persona
	{
		public int IDEncargado { get; set; }
		public string Puesto { get; set; }
		public List<Proyecto> Proyectos { get; set; }
        public Organizacion Organizacion { get; set; }

		public void AñadirProyecto(Proyecto proyecto)
		{
            proyecto.Encargado = this;
            this.Proyectos.Add(proyecto);
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            proyectoDAO.GuardarProyecto(proyecto);
        }
	}
}
