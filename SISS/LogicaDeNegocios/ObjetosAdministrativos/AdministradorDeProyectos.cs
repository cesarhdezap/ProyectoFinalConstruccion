using System.Collections.Generic;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	/// <summary>
	/// Clase administradora de proyectos dentro de una lista en el objeto.
	/// Contiene los métodos para cargar todos los proyectos, cargarlos por estado
	/// y obtener solo sus nombres.
	/// </summary>
	public class AdministradorDeProyectos
    {
        public List<Proyecto> Proyectos { get; set; }

        /// <summary>
        /// Carga todos los <see cref="Proyecto"/> de la base de datos en la lista
        /// <see cref="Proyectos"/>.
        /// </summary>
        public void CargarProyectosTodos()
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Proyectos = proyectoDAO.CargarProyectosTodos();
		}

        /// <summary>
        /// Carga todos los proyectos con el estado en <paramref name="estadoProyecto"/>
        /// en la lista <see cref="Proyectos"/>.
        /// </summary>
        /// <param name="estadoProyecto"></param>
        public void CargarProyectosPorEstado(EstadoProyecto estadoProyecto)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            Proyectos = proyectoDAO.CargarProyectosPorEstado(estadoProyecto);
        }

        /// <summary>
        /// Filtra solo los Nombres de los Proyectos en <see cref="Proyectos"/>.
        /// </summary>
        /// <returns>Una lista de Proyectos con solo el atributo Nombre.</returns>
		public List<string> ObtenerNombresDeProyectos()
		{
			List<string> listaDeNombres = new List<string>();

			foreach (Proyecto proyecto in Proyectos)
			{
				listaDeNombres.Add(proyecto.Nombre);
			}

			return listaDeNombres;
		}
    }
}
