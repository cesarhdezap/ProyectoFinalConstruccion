using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocenteAcademicoDAO
	{
		void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico);  
        DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDpersonal);
        List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo);
		List<DocenteAcademico> CargarDocentesAcademicosPorRol(Rol rol);
        void GuardarDocenteAcademico(DocenteAcademico docenteAcademico);
        int ObtenerUltimoIDInsertado();
    }
}
