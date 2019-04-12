using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocenteAdministrativoDAO : Interfaces.IDocenteAdministrativoDAO
	{
		public DocenteAcademico CargarDocenteAdministrativoPorIDPersonal(int IDpersonal)
		{
			throw new NotImplementedException();
		}

		public List<DocenteAcademico> CargarDocentesAdministrativosPorEstado(bool isActivo)
		{
			throw new NotImplementedException();
		}

		public List<DocenteAcademico> CargarDocentesAdministrativosPorRol(Erol rol)
		{
			throw new NotImplementedException();
		}

		public DataTable ConvertirDocenteAdministrativoADataTable(DocenteAcademico docenteAdministrativo)
		{
			throw new NotImplementedException();
		}

		public void GuardarDocenteAdministrativo(DocenteAcademico docenteAdministrativo)
		{
			throw new NotImplementedException();
		}
	}
}
