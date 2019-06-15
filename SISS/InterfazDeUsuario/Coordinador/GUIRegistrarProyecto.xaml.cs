using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarProyecto : Window
    {
        private AdministradorDeOrganizaciones AdministradorDeOrganizaciones;
        private AdministradorDeEncargados AdministradorDeEncargados;
        private const int SIN_INDICE = -1;

        public GUIRegistrarProyecto(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;

            AdministradorDeOrganizaciones = new AdministradorDeOrganizaciones();
            AdministradorDeOrganizaciones.CargarOrganizaciones();
            ComboBoxOrganizacionAsociada.DisplayMemberPath = "Nombre";
            ComboBoxOrganizacionAsociada.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
            ComboBoxOrganizacionAsociada.SelectedIndex = 0;

            AdministradorDeEncargados = new AdministradorDeEncargados();
            AdministradorDeEncargados.CargarEncargadosTodos();
			ComboBoxEncargadoAsociado.SelectedIndex = 0;
		}

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Proyecto proyecto = new Proyecto()
            {
                Estado = EstadoProyecto.Activo,
                Nombre = TextBoxNombre.Text,
                ObjetivoGeneral = TextBoxObjetivoGeneral.Text,
                DescripcionGeneral = TextBoxDescripcionGeneral.Text,

            };

            int indiceDeEncargado = ComboBoxEncargadoAsociado.SelectedIndex;
            if (indiceDeEncargado > SIN_INDICE)
			{
                proyecto.Encargado = ComboBoxEncargadoAsociado.SelectedItem as Encargado;

                if (ValidarEntero(TextBoxEstudiantesSolicitados.Text))
                {
					proyecto.Cupo = Int32.Parse(TextBoxEstudiantesSolicitados.Text);
                }
                else
				{
                    proyecto.Cupo = VALOR_ENTERO_MINIMO_PERMITIDO;
				}

                Mouse.OverrideCursor = Cursors.Wait;
                if (proyecto.Validar())
                {
                    bool resultadoDeCreacion = false;
                    try
                    {
                        proyecto.Guardar();
                        resultadoDeCreacion = true;
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
                    {
                        MessageBox.Show(this, CONEXION_FALLIDA_MENSAJE, CONEXION_FALLIDA_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
                    {
                        MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    catch(AccesoADatosException ex) when(ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
                    {
                        MessageBox.Show(this, ERROR_GUARDAR_REGISTRO, ERROR_DESCONOCIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
                    {
                        MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
                    {
                        MessageBox.Show(this, ERROR_PETICION_MENSAJE, ERROR_INTERNO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
                    {
                        MessageBox.Show(this, ERROR_DESCONOCIDO_MENSAJE, ERROR_DESCONOCIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }

                    if (resultadoDeCreacion)
                    {
                        MessageBox.Show(this, "Proyecto guardado exitosamente.", "¡Registro exitoso!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show(COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Mouse.OverrideCursor = null;
            }
			else
			{
				MessageBox.Show(this, "Debe seleccionar un encargado para ser asociado al proyecto.", "Encargado no seleccionado", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxNombre.Text))
            {
                TextBoxNombre.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxNombre.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxEstudiantesSolicitados_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TextBoxEstudiantesSolicitados.Text, out int resultado))
            {
                TextBoxEstudiantesSolicitados.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxEstudiantesSolicitados.BorderBrush = Brushes.Red;
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAdjuntarEvidencia_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBoxOrganizacionAsociada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indiceOrganizacion = ComboBoxOrganizacionAsociada.SelectedIndex;
            if (indiceOrganizacion > SIN_INDICE)
            {
                int IDOrganizacion = AdministradorDeOrganizaciones.Organizaciones[indiceOrganizacion].IDOrganizacion;
                ComboBoxEncargadoAsociado.DisplayMemberPath = "Nombre";
                ComboBoxEncargadoAsociado.ItemsSource = AdministradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(IDOrganizacion);
                ComboBoxEncargadoAsociado.SelectedIndex = 0;
            }
        }

        private void TextBoxObjetivoGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxObjetivoGeneral.Text))
            {
                TextBoxObjetivoGeneral.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxObjetivoGeneral.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxDescripcionGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxDescripcionGeneral.Text))
            {
                TextBoxDescripcionGeneral.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxDescripcionGeneral.BorderBrush = Brushes.Red;
            }
        }
    }
}
