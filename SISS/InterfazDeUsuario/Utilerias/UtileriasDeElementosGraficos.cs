using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios;
using LogicaDeNegocios.Servicios;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

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

		private static void MostrarToolTip(TextBox cajaDeTexto, string mensaje)
		{
			if (cajaDeTexto.ToolTip == null)
			{
				cajaDeTexto.ToolTip = new ToolTip()
				{
					Content = mensaje,
					Placement = System.Windows.Controls.Primitives.PlacementMode.Right,
				};
			}
			((ToolTip)cajaDeTexto.ToolTip).IsEnabled = true;
			ToolTipService.SetPlacementTarget((ToolTip)cajaDeTexto.ToolTip, cajaDeTexto);	
			((ToolTip)cajaDeTexto.ToolTip).IsOpen = true;
		}

		private static void OcultarToolTip(TextBox cajaDeTexto)
		{
			if (cajaDeTexto.ToolTip != null)
			{
				((ToolTip)cajaDeTexto.ToolTip).IsOpen = false;
				((ToolTip)cajaDeTexto.ToolTip).IsEnabled = false;
				cajaDeTexto.ToolTip = null;
			} 
		}
	}
}
