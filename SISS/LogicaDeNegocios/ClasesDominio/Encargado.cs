using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios
{
    /// <summary>
    /// Clase <see cref="Encargado"/>.
    /// Contiene todos los métodos para la comunicación con la base de datos y
    /// la administración de sus objeto atributo internos.
    /// </summary>
	public class Encargado : Persona
	{
		public int IDEncargado { get; set; }
		public string Puesto { get; set; }
		public List<Proyecto> Proyectos { get; set; }
        public Organizacion Organizacion { get; set; }

		/// <summary>
		/// Guarda un <see cref="Encargado"/> validado por <see cref="ValidarEncargado"/> 
		/// en la base de datos.
		/// </summary>
		public void Guardar()
		{
			EncargadoDAO encargadoDAO = new EncargadoDAO();
			encargadoDAO.GuardarEncargado(this);
		}

        /// <summary>
        /// Valida los atributos del <see cref="Encargado"/>
        /// para la inserción a la base de datos.
        /// </summary>
        /// <returns></returns>
        public bool Validar()
        {
            bool resultadoDeValidacion = false;
            if (ValidarNombre(Nombre)
                && ValidarCorreoElectronico(CorreoElectronico)
                && ValidarTelefono(Telefono)
                && ValidarCadena(Puesto))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }

        /// <summary>
        /// Carga la Organizacion del Encargado por <see cref="IDEncargado"/>.
        /// </summary>
        /// <returns>La <see cref="Organizacion"/> del Encargado.</returns>
		public Organizacion CargarOrganizacion()
		{
			OrganizacionDAO organizacionDAO = new OrganizacionDAO();
			Organizacion organizacion = new Organizacion();
			organizacion = organizacionDAO.CargarIDPorIDEncargado(IDEncargado);
			organizacion = organizacionDAO.CargarOrganizacionPorID(organizacion.IDOrganizacion);
			return organizacion;
		}
	}
}
