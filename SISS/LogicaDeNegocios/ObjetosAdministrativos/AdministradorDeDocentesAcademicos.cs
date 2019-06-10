using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
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

		public void CargarDocentesPorRol(Rol rol)
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			this.DocentesAcademicos = docenteAcademicoDAO.CargarDocentesAcademicosPorRol(rol);
		}
	}
}
