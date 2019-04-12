using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrativos
{
	class AdministradorDeDocentesAcademicos
	{
		public List<DocenteAcademico> DocentesAcademicos { get; set; }

		public void CrearDocenteAcademico(DocenteAcademico docenteAcademico)
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			docenteAcademicoDAO.GuardarDocenteAcademico(docenteAcademico);
			this.DocentesAcademicos.Add(docenteAcademico);
		}

		public void CargarDocentesPorRol(ERol rol)
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			this.DocentesAcademicos = docenteAcademicoDAO.CargarDocentesAcademicosPorRol(rol);
		}
	}
}
