using System;
using System.Collections.Generic;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
namespace LogicaDeNegocios
{
	public class Solicitud
	{
        public int IDSolicitud { get; set; }
		public DateTime Fecha { get; set; }
		public List<Proyecto> Proyectos { get; set; }
		public Alumno Alumno { get; set; }
        public DocumentoDeEntregaUnica CartaDeSolicitud { get; set; }
		public Solicitud()
		{

		}

		public Solicitud(Alumno alumno)
		{
            Alumno = alumno;
		}

        public override string ToString()
        {
            string solicitud = "IDSolicitud: " + IDSolicitud + System.Environment.NewLine +
                               "Fecha: " + Fecha.ToString();

            return solicitud;
        }

		public void Guardar()
		{
			SolicitudDAO solicitudDAO = new SolicitudDAO();
			solicitudDAO.GuardarSolicitud(this);
		}
    }
}
