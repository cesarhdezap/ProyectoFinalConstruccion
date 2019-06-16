using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocenteAcademicoDAO
	{
		void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico);
		DocenteAcademico CargarIDCoordinadorPorCarrera(string carrera);
		string CargarIDPorCorreoYRol(string correoElectronico, Rol rol);
		DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDpersonal);
		DocenteAcademico CargarIDPorIDDocumento(int IDDocumento);
		List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo);
		List<DocenteAcademico> CargarDocentesAcademicosPorRol(Rol rol);
        void GuardarDocenteAcademico(DocenteAcademico docenteAcademico);
    }
}
