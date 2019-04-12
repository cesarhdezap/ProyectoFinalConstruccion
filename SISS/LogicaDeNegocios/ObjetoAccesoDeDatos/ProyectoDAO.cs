using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class ProyectoDAO : Interfaces.IProyectoDAO
	{
		public void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto)
		{
			throw new NotImplementedException();
		}

		public Proyecto CargarProyectoPorID(int IDProyecto)
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
