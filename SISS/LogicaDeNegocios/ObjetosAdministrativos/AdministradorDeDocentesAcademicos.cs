using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeDocentesAcademicos
	{
		public List<DocenteAcademico> DocentesAcademicos { get; set; }

		public void CargarDocentesPorRol(Rol rol)
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			this.DocentesAcademicos = docenteAcademicoDAO.CargarDocentesAcademicosPorRol(rol);
		}

		public void CargarCoordinadoresTodos()
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			this.DocentesAcademicos = docenteAcademicoDAO.CargarDocentesAcademicosPorRol(Rol.Coordinador);
		}

	}
}
