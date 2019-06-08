using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace InterfazDeUsuario
{
    class ConvertidorMatriculaDeAlumnoADetallesDeAlumno : IValueConverter
    {
        public object Convert(object matricula, Type targetType, object parameter, CultureInfo culture)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumno alumno = new Alumno();
            alumno = alumnoDAO.CargarAlumnoPorMatricula((string)matricula);

            string cadenaResultado = "Carrera: " + alumno.Carrera + System.Environment.NewLine +
                                     "Correo Electronico: " + alumno.CorreoElectronico + System.Environment.NewLine +
                                     "Matrícula: " + alumno.Matricula + System.Environment.NewLine +
                                     "Teléfono: " + alumno.Telefono;

            return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
