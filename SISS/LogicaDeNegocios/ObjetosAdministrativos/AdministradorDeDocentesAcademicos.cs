using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    /// <summary>
    /// Clase administradora de Docentes Academicos en una lista.
    /// Contiene métodos para cargar los docentes por rol.
    /// </summary>
	public class AdministradorDeDocentesAcademicos
	{
		public List<DocenteAcademico> DocentesAcademicos { get; set; }

        /// <summary>
        /// Carga Docentes Academicos a la lista <see cref="DocentesAcademicos"/> del objeto por Rol.
        /// </summary>
        /// <param name="rol">Atributo Rol de DocenteAcademico</param>
		public void CargarDocentesPorRol(Rol rol)
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            DocentesAcademicos = docenteAcademicoDAO.CargarDocentesAcademicosPorRol(rol);
		}
	}
}
