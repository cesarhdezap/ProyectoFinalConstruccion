using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocenteAcademicoDAO
	{
		void GuardarDocenteAcademico(DocenteAcademico docenteAcademico);
		DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDpersonal);
		List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo);
		List<DocenteAcademico> CargarDocentesAcademicosPorRol(Rol rol);
		void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico);  
	}
}
