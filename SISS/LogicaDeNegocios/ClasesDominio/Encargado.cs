﻿using System;
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
            proyecto.encargado = this;
            this.Proyectos.Add(proyecto);
		}
	}
}
