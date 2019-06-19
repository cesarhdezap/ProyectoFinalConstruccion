using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using System.Collections.Generic;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario.GUIsDeDirector
{
	public partial class GUIBuscarCoordinador : Window
	{
		private Director Director { get; set; }
		private AdministradorDeDocentesAcademicos AdministradorDeDocentesAcademicos { get; set; }
        public GUIBuscarCoordinador(Director director)
        {
            InitializeComponent();
            Director = director;
			AdministradorDeDocentesAcademicos = new AdministradorDeDocentesAcademicos();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				AdministradorDeDocentesAcademicos.CargarDocentesPorRol(Rol.Coordinador);
			}
			catch (AccesoADatosException ex)
			{
				MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
				mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
				MessageBox.Show(this, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}
            LabelNombreDeUsuario.Content = director.Nombre;
            DataGridCoordinadores.ItemsSource = AdministradorDeDocentesAcademicos.DocentesAcademicos;
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Wait;

			CambiarEstadoDeExpander(sender);
			Mouse.OverrideCursor = null;
		}

		private void Expander_Collapsed(object sender, RoutedEventArgs e)
		{
			CambiarEstadoDeExpander(sender);
		}

		private void TextBoxBuscarCoordinadorPorNombre_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBoxBuscarCoordinadorPorNombre.Text == string.Empty)
			{
				DataGridCoordinadores.ItemsSource = AdministradorDeDocentesAcademicos.DocentesAcademicos;
			}
			else
			{
				List<DocenteAcademico> coordinadoresFiltrados = AdministradorDeDocentesAcademicos.DocentesAcademicos.FindAll(delegate (DocenteAcademico coordinador)
				{
					return coordinador.Nombre.Contains(TextBoxBuscarCoordinadorPorNombre.Text);
				});
				DataGridCoordinadores.ItemsSource = coordinadoresFiltrados;
			}
		}

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}
	}
}
