using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InterfazDeUsuario
{
	class ConvertidorIDPersonalADetallesDeDocenteAcademico : IValueConverter
	{
		public object Convert(object IDPersonal, Type targetType, object parameter, CultureInfo culture)
		{
			DocenteAcademico docenteAcademico = new DocenteAcademico();
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();

			docenteAcademico = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal((int)IDPersonal);

			string cadenaResultado = "ID Personal: " + docenteAcademico.IDPersonal + System.Environment.NewLine +
									 "Carrera: " + docenteAcademico.Carrera + System.Environment.NewLine +
									 "Correo Electrónico: " + docenteAcademico.CorreoElectronico + System.Environment.NewLine +
									 "Teléfono: " + docenteAcademico.Telefono;

			return cadenaResultado;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
