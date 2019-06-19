using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace InterfazDeUsuario
{
    class ConvertidorMatriculaDeAlumnoADetallesDeAlumno : IValueConverter
    {
        public object Convert(object matricula, Type targetType, object parameter, CultureInfo culture)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumno alumno = new Alumno();
			string cadenaResultado = string.Empty;
			try
			{

				alumno = alumnoDAO.CargarAlumnoPorMatricula((string)matricula);

				cadenaResultado = "- Carrera: " + alumno.Carrera + System.Environment.NewLine +
										 "- Correo Electronico: " + alumno.CorreoElectronico + System.Environment.NewLine +
										 "- Matrícula: " + alumno.Matricula + System.Environment.NewLine +
										 "- Teléfono: " + alumno.Telefono;
			}
			catch (AccesoADatosException e)
			{
				MensajeDeErrorParaMessageBox mensaje;
				mensaje = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(e);
				cadenaResultado = mensaje.Mensaje;
			}
			return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
