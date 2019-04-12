using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocenteAcademicoDAO : Interfaces.IDocenteAcademicoDAO
	{
		public void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico)
		{
			throw new NotImplementedException();
		}

		public DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDpersonal)
		{
			throw new NotImplementedException();
		}

		public List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo)
		{
			throw new NotImplementedException();
		}

		public List<DocenteAcademico> CargarDocentesAcademicosPorRol(ERol rol)
		{
			throw new NotImplementedException();
		}

		public DataTable ConvertirDocenteAcademicoADataTable(DocenteAcademico docenteAdministrativo)
		{
			throw new NotImplementedException();
		}

		public void GuardarDocenteAcademico(DocenteAcademico docenteAdministrativo)
		{
			throw new NotImplementedException();
		}
	}
}
