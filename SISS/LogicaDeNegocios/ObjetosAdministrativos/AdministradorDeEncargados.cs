using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    /// <summary>
    /// Clase que administra encargados en una lista dentro del objeto.
    /// Contiene métodos para cargar todos los encargados y cargar encargados por ID de Organizacion.
    /// </summary>
    public class AdministradorDeEncargados
    {
        public List<Encargado> Encargados;
        
        /// <summary>
        /// Carga todos los encargados de la base de datos a la lista <see cref="Encargados"/>.
        /// </summary>
        public void CargarEncargadosTodos()
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            Encargados = encargadoDAO.CargarEncargadosConIDNombreYOrganizacion();
        }

        /// <summary>
        /// Filtra los Encargados en la lista <see cref="Encargados"/> del objeto según el atributo IDOrganizacion.
        /// </summary>
        /// <param name="IDOrganizacion">Atributo de Encargado.</param>
        /// <returns>Lista de encargados con el <paramref name="IDOrganizacion"/>.</returns>
        public List<Encargado> SeleccionarEncargadosPorIDOrganizacion(int IDOrganizacion)
        {
            List<Encargado> encargados = new List<Encargado>();
            CargarEncargadosTodos();
            if (Encargados.Count > 0)
            {
                encargados = Encargados.FindAll(encargadoActual => encargadoActual.Organizacion.IDOrganizacion == IDOrganizacion);
            }
            return encargados;
        }
    }
}
