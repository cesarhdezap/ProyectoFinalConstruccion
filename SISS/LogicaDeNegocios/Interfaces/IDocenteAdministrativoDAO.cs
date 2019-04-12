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
		void GuardarDocenteAdministrativo(DocenteAcademico docenteAcademico);
		DocenteAcademico CargarDocenteAdministrativoPorIDPersonal(int IDpersonal);
		List<DocenteAcademico> CargarDocentesAdministrativosPorEstado(bool isActivo);
		List<DocenteAcademico> CargarDocentesAdministrativosPorRol(ERol rol);
		void ActualizarDocenteAdministrativoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico);
		DataTable ConvertirDocenteAdministrativoADataTable(DocenteAcademico docenteAcademico);
        
	}
}
