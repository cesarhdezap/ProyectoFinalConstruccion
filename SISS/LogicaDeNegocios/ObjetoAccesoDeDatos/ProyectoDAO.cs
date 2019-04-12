using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class ProyectoDAO : Interfaces.IProyectoDAO
	{
		public void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto)
		{
			throw new NotImplementedException();
		}

		public List<Proyecto> CargarIDsPorIDEncargado(int IDencargado)
		{
			throw new NotImplementedException();
		}

		public Proyecto CargarProyectoPorID(int IDproyecto)
		{
			throw new NotImplementedException();
		}

		public List<Proyecto> CargarProyectosTodos()
		{
			throw new NotImplementedException();
		}

		public int GuardarProyecto(Proyecto proyecto)
		{
			throw new NotImplementedException();
		}

		public DataTable ProyectoADataTable(Proyecto proyecto)
		{
			throw new NotImplementedException();
		}
	}
}
