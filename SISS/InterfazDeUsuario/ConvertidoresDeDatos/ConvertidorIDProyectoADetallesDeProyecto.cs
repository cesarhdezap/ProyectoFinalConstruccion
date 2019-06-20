using System;
using System.Globalization;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario
{
	class ConvertidorIDProyectoADetallesDeProyecto : IValueConverter
    {
        public object Convert(object IDProyecto, Type cadena, object parametro, CultureInfo culture)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Proyecto proyecto = new Proyecto();
            Encargado encargado = new Encargado();
            Organizacion organizacion = new Organizacion();
            string cadenaResultado = string.Empty;

            try
            {
                proyecto = proyectoDAO.CargarProyectoPorID((int)IDProyecto);
                encargado = encargadoDAO.CargarIDPorIDProyecto(proyecto.IDProyecto);
                organizacion = organizacionDAO.CargarIDPorIDEncargado(encargado.IDEncargado);
                encargado = encargadoDAO.CargarEncargadoPorID(encargado.IDEncargado);
                organizacion = organizacionDAO.CargarOrganizacionPorID(organizacion.IDOrganizacion);
                cadenaResultado = "- Dependencia: " + organizacion.Nombre + System.Environment.NewLine +
                                  "- Direccion: " + organizacion.Direccion + System.Environment.NewLine +
                                  "- Encargado: " + encargado.Nombre + System.Environment.NewLine +
                                  "- Correo Electronico: " + encargado.CorreoElectronico + System.Environment.NewLine +
                                  "- Cupo: " + proyecto.ObtenerDisponibilidad() + System.Environment.NewLine +
                                  "- Descripcion general: " + proyecto.DescripcionGeneral;
            }
            catch (AccesoADatosException e)
            {
				MostrarMessageBoxDeExcepcion(e);
            }

            return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
