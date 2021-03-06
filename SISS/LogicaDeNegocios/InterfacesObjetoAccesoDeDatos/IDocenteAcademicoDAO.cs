﻿using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocenteAcademicoDAO
	{
		void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico);
		DocenteAcademico CargarIDCoordinadorPorCarrera(string carrera);
		string CargarIDPorCorreoYRol(string correoElectronico, Rol rol);
		DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDpersonal);
		DocenteAcademico CargarIDPorIDReporteMensual(int IDDocumento);
		List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo);
		List<DocenteAcademico> CargarDocentesAcademicosPorRol(Rol rol);
        void GuardarDocenteAcademico(DocenteAcademico docenteAcademico);
    }
}
