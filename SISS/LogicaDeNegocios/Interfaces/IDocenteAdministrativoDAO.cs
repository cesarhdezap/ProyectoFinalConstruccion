using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocenteAdministrativoDAO
	{
		void GuardarDocenteAdministrativo(DocenteAdministrativo docenteAdministrativo);
		DocenteAdministrativo CargarDocenteAdministrativoPorIDPersonal(int IDpersonal);
		List<DocenteAdministrativo> CargarDocentesAdministrativosPorRol(Erol rol);
		DataTable DocenteAdministrativoADataTable(DocenteAdministrativo docenteAdministrativo);
	}
}
