using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Interfaces
{
	interface IProyectoDAO
	{
        int GuardarProyecto(Proyecto proyecto);
		List<Proyecto> CargarProyectosTodos();
        Proyecto CargarProyectoPorID(int IDproyecto);
		void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto);
        DataTable ProyectoADataTable(Proyecto proyecto);
	}
}
