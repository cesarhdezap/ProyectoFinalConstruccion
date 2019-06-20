using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Globalization;
using System.Windows.Data;
using LogicaDeNegocios.Excepciones;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario
{
	class ConvertidorIDPersonalADetallesDeDocenteAcademico : IValueConverter
	{
		public object Convert(object IDPersonal, Type targetType, object parameter, CultureInfo culture)
		{
			DocenteAcademico docenteAcademico = new DocenteAcademico();
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			string cadenaResultado = string.Empty;

			try
			{
				docenteAcademico = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal((int)IDPersonal);
				cadenaResultado = "- ID Personal: " + docenteAcademico.IDPersonal + System.Environment.NewLine +
								  "- Carrera: " + docenteAcademico.Carrera + System.Environment.NewLine +
								  "- Correo Electrónico: " + docenteAcademico.CorreoElectronico + System.Environment.NewLine +
								  "- Teléfono: " + docenteAcademico.Telefono;
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
