using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocenteAcademicoDAO : Interfaces.IDocenteAcademicoDAO
	{
		public void ActualizarDocenteAcademicoPorIDPersonal(int IDpersonal, DocenteAcademico docenteAcademico)
		{
			//TODO
			throw new NotImplementedException();
		}

		public DocenteAcademico CargarDocenteAcademicoPorIDPersonal(int IDpersonal)
		{
			//TODO
			throw new NotImplementedException();
		}

		public List<DocenteAcademico> CargarDocentesAcademicosPorEstado(bool isActivo)
		{
			//TODO
			throw new NotImplementedException();
		}

		public List<DocenteAcademico> CargarDocentesAcademicosPorRol(ERol rol)
		{
			//TODO
			throw new NotImplementedException();
		}

		public DataTable ConvertirDocenteAcademicoADataTable(DocenteAcademico docenteAdministrativo)
		{
			//TODO
			throw new NotImplementedException();
		}

		public void GuardarDocenteAcademico(DocenteAcademico docenteAdministrativo)
		{
			//TODO
			throw new NotImplementedException();
		}
	}
}
