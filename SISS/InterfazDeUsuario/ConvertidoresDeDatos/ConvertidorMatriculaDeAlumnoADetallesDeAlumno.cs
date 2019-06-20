using System;
using System.Globalization;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

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
