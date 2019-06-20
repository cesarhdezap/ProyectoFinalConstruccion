using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.Utilerias
{
	public static class UtileriasDeElementosGraficos
    {
		public static void MostrarEstadoDeValidacionContraseña(TextBox textBoxContraseña)
		{
			if (ValidarContraseña(textBoxContraseña.Text))
			{
				textBoxContraseña.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxContraseña);
			}
			else
			{
				textBoxContraseña.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxContraseña, CONTRASEÑA_INVALIDA);
			}
		}

		public static void MostrarEstadoDeValidacionTelefono(TextBox textBoxTelefono)
		{
			if (ValidarTelefono(textBoxTelefono.Text))
			{
				textBoxTelefono.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxTelefono);
			}
			else
			{
				textBoxTelefono.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxTelefono, TELEFONO_INVALIDO);
			}
		}

		public static void MostrarEstadoDeValidacionCorreoElectronico(TextBox textBoxCorreoElectronico)
		{
			if (ValidarCorreoElectronico(textBoxCorreoElectronico.Text))
			{
				textBoxCorreoElectronico.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxCorreoElectronico);
			}
			else
			{
				textBoxCorreoElectronico.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxCorreoElectronico, CORREOELECTRONICO_INVALIDO);
			}
		}

		public static void MostrarEstadoDeValidacionConfirmacion(TextBox textBoxCampo, TextBox textBoxConfirmarCampo)
		{
			if (textBoxCampo.Text == textBoxConfirmarCampo.Text)
			{
				textBoxConfirmarCampo.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxConfirmarCampo);
			}
			else
			{
				textBoxConfirmarCampo.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxConfirmarCampo, CONFIRMACION_INVALIDA);
			}
		}

		public static void MostrarEstadoDeValidacionNombre(TextBox textBoxNombre)
		{
			if (ValidarNombre(textBoxNombre.Text))
			{
				textBoxNombre.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxNombre);
			}
			else
			{
				textBoxNombre.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxNombre, NOMBRE_INVALIDO);
			}
		}

		public static void MostrarEstadoDeValidacionMatricula(TextBox textBoxMatricula)
		{
			if (ValidarMatricula(textBoxMatricula.Text))
			{
				textBoxMatricula.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxMatricula);
			}
			else
			{
				textBoxMatricula.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxMatricula, MATRICULA_INVALIDA);
			}
		}

		public static void MostrarEstadoDeValidacionCampoNumerico(TextBox textBoxNumero)
		{
			if (ValidarEntero(textBoxNumero.Text))
			{
				textBoxNumero.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxNumero);
			}
			else
			{
				textBoxNumero.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxNumero, VALOR_ENTERO_INVALIDO);
			}
		}

		public static void MostrarEstadoDeValidacionCadena(TextBox textBoxCadena)
		{
			if (ValidarCadena(textBoxCadena.Text))
			{
				textBoxCadena.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxCadena);
			}
			else
			{
				textBoxCadena.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxCadena, CADENA_INVALIDA);
			}
		}

		public static void MostrarEstadoDeValidacionComboBox(ComboBox comboBox)
		{
			if (ValidarSeleccionComboBox(comboBox))
			{
				comboBox.ClearValue(Control.BorderBrushProperty);
				OcultarToolTip(comboBox);
			}
			else
			{
				comboBox.BorderBrush = Brushes.Red;
				MostrarToolTip(comboBox, COMBO_BOX_INVALIDO);
			}
		}

		public static bool ValidarSeleccionComboBox(ComboBox comboBox)
		{

			bool resultadoDeValidacion = false;

			if (comboBox.SelectedIndex > -1 && comboBox.SelectedIndex < comboBox.Items.Count)
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

		public static void MostrarEstadoDeValidacionCorreoDuplicado(TextBox textBoxCorreo)
		{
			if (ValidarCorreoElectronico(textBoxCorreo.Text))
			{
				if (ValidarDisponibilidadDeCorreo(textBoxCorreo.Text))
				{
					textBoxCorreo.BorderBrush = Brushes.Green;
					OcultarToolTip(textBoxCorreo);
				}
				else
				{
					textBoxCorreo.BorderBrush = Brushes.Red;
					MostrarToolTip(textBoxCorreo, CORREOELECTRONICO_DUPLICADO_MENSAJE);
				}
			}
		}

		public static void MostrarEstadoDeValidacionMatriculaDuplicada(TextBox textBoxMatricula)
		{
			if (ValidarMatricula(textBoxMatricula.Text)){
				if (ValidarDisponibilidadDeMatricula(textBoxMatricula.Text))
				{
					textBoxMatricula.BorderBrush = Brushes.Green;
					OcultarToolTip(textBoxMatricula);
				}
				else
				{
					textBoxMatricula.BorderBrush = Brushes.Red;
					MostrarToolTip(textBoxMatricula, MATRICULA_DUPLICADA_MENSAJE);
				}
			}
		}

		public static void CambiarEstadoDeExpander(object expander)
		{
			for (Visual elementoVisual = expander as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
			{
				if (elementoVisual is DataGridRow fila)
				{
					if (fila.DetailsVisibility == Visibility.Visible)
					{
						fila.DetailsVisibility = Visibility.Collapsed;
					}
					else
					{
						fila.DetailsVisibility = Visibility.Visible;
					}

					break;
				}	
			}
		}

		public static void MostrarMessageBoxDeExcepcion(Window padre, AccesoADatosException e)
		{
			MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
			mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(e);
			MessageBox.Show(padre, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public static void MostrarMessageBoxDeExcepcion(AccesoADatosException e)
		{
			MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
			mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(e);
			MessageBox.Show(mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public static string MostrarVentanaDeSeleccionDeArchivos(Imagen imagen)
		{

			string direccionDeArchivoSeleccionado = string.Empty;

			OpenFileDialog ventanaDeSeleccionDeArchivo = new OpenFileDialog
			{
				Filter = "Imagenes (*.jpg)|*.jpg",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};

			if (ventanaDeSeleccionDeArchivo.ShowDialog() == true)
			{
				direccionDeArchivoSeleccionado = ventanaDeSeleccionDeArchivo.FileName;
				imagen.DireccionDeImagen = ventanaDeSeleccionDeArchivo.FileName;
			}

			return direccionDeArchivoSeleccionado;
		}

		public static void MostrarPantalla(Window padre, Window hijo)
		{
			padre.Hide();
			hijo.ShowDialog();
			padre.ShowDialog();
		}

		private static void MostrarToolTip(Control controlGrafico, string mensaje)
		{
			if (controlGrafico.ToolTip == null)
			{
				controlGrafico.ToolTip = new ToolTip()
				{
					Content = mensaje,
					Placement = System.Windows.Controls.Primitives.PlacementMode.Right,
				};
			}

			((ToolTip)controlGrafico.ToolTip).IsEnabled = true;
			ToolTipService.SetPlacementTarget((ToolTip)controlGrafico.ToolTip, controlGrafico);	
			((ToolTip)controlGrafico.ToolTip).IsOpen = true;
		}

		private static void OcultarToolTip(Control controlGrafico)
		{
			if (controlGrafico.ToolTip != null)
			{
				((ToolTip)controlGrafico.ToolTip).IsOpen = false;
				((ToolTip)controlGrafico.ToolTip).IsEnabled = false;
				controlGrafico.ToolTip = null;
			} 
		}
	}
}
