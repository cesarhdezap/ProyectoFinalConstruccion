using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios;
using LogicaDeNegocios.Servicios;
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
				if (textBoxContraseña.ToolTip != null)
				{
					((ToolTip)textBoxContraseña.ToolTip).IsOpen = false;
					((ToolTip)textBoxContraseña.ToolTip).IsEnabled = false;
				}
			}
			else
			{
				textBoxContraseña.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxContraseña, "Debe tener mínimo 6 caracteres o máximo 255.");
			}
		}

		public static void MostrarEstadoDeValidacionTelefono(TextBox textBoxTelefono)
		{
			if (ValidarTelefono(textBoxTelefono.Text))
			{
				textBoxTelefono.BorderBrush = Brushes.Green;
				if (textBoxTelefono.ToolTip != null)
				{
					((ToolTip)textBoxTelefono.ToolTip).IsOpen = false;
					((ToolTip)textBoxTelefono.ToolTip).IsEnabled = false;
				}
			}
			else
			{
				textBoxTelefono.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxTelefono, "Solo pueden ser caracteres numéricos.");
			}
		}

		public static void MostrarEstadoDeValidacionCorreoElectronico(TextBox textBoxCorreoElectronico)
		{
			if (ValidarCorreoElectronico(textBoxCorreoElectronico.Text))
			{
				textBoxCorreoElectronico.BorderBrush = Brushes.Green;
				if (textBoxCorreoElectronico.ToolTip != null)
				{
					((ToolTip)textBoxCorreoElectronico.ToolTip).IsOpen = false;
					((ToolTip)textBoxCorreoElectronico.ToolTip).IsEnabled = false;
				}
			}
			else
			{
				textBoxCorreoElectronico.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxCorreoElectronico, "No es un correo electrónico valido.");
			}
		}

		public static void MostrarEstadoDeValidacionConfirmacion(TextBox textBoxCampo, TextBox textBoxConfirmarCampo)
		{
			if (textBoxCampo.Text == textBoxConfirmarCampo.Text)
			{
				textBoxConfirmarCampo.BorderBrush = Brushes.Green;
				if (textBoxConfirmarCampo.ToolTip != null)
				{
					((ToolTip)textBoxConfirmarCampo.ToolTip).IsOpen = false;
					((ToolTip)textBoxConfirmarCampo.ToolTip).IsEnabled = false;
				}
			}
			else
			{
				textBoxConfirmarCampo.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxConfirmarCampo, "Los valores no coinciden.");
			}
		}

		public static void MostrarEstadoDeValidacionNombre(TextBox textBoxNombre)
		{
			if (ValidarNombre(textBoxNombre.Text))
			{
				textBoxNombre.BorderBrush = Brushes.Green;
				if (textBoxNombre.ToolTip != null) {
					((ToolTip)textBoxNombre.ToolTip).IsOpen = false;
					((ToolTip)textBoxNombre.ToolTip).IsEnabled = false;
				}
			}
			else
			{
				textBoxNombre.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxNombre, "Solo puede ser letras y signos de acentuación.");
			}
		}

		public static void MostrarEstadoDeValidacionMatricula(TextBox textBoxMatricula)
		{
			if (ValidarMatricula(textBoxMatricula.Text))
			{
				textBoxMatricula.BorderBrush = Brushes.Green;
				if (textBoxMatricula.ToolTip != null)
				{
					((ToolTip)textBoxMatricula.ToolTip).IsOpen = false;
					((ToolTip)textBoxMatricula.ToolTip).IsEnabled = false;
				}
			}
			else
			{
				textBoxMatricula.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxMatricula, "Debe tener el formato s12345678.");
			}
		}

		public static void MostrarEstadoDeValidacionCampoNumerico(TextBox textBoxNumero)
		{
			if (ValidarEntero(textBoxNumero.Text))
			{
				textBoxNumero.BorderBrush = Brushes.Green;
				if (textBoxNumero.ToolTip != null)
				{
					((ToolTip)textBoxNumero.ToolTip).IsOpen = false;
					((ToolTip)textBoxNumero.ToolTip).IsEnabled = false;
				}
			}
			else
			{
				textBoxNumero.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxNumero, "Deber ser un valor numérico entre 1 y 255.");
			}
		}

		private static void MostrarToolTip(TextBox textBox, string mensaje)
		{
			if (textBox.ToolTip == null)
			{
				textBox.ToolTip = new ToolTip()
				{
					Content = mensaje,
					Placement = System.Windows.Controls.Primitives.PlacementMode.Right,

				};
			}
			((ToolTip)textBox.ToolTip).IsEnabled = true;
			ToolTipService.SetPlacementTarget((ToolTip)textBox.ToolTip, textBox);
			ToolTipService.SetShowDuration((ToolTip)textBox.ToolTip, 1000);
			((ToolTip)textBox.ToolTip).IsOpen = true;
		}
	}
}
