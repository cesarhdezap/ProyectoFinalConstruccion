using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Solicitud
	{
        public int IDSolicitud { get; set; }
		public DateTime Fecha { get; set; }
		public List<Proyecto> Proyectos { get; set; }
        public DocumentoDeEntregaUnica CartaDeSolicitud { get; set; }

        public override string ToString()
        {
            string solicitud = "IDSolicitud: " + IDSolicitud + System.Environment.NewLine +
                               "Fecha: " + Fecha.ToString();

            return solicitud;
        }
    }
}
