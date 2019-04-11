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
        Proyecto CargarProyectoPorID(int IDProyecto);
        List<Proyecto> CargarProyectosTodos(Proyecto proyecto);
        DataTable ProyectoADataTable(Proyecto proyecto);
	}
}
