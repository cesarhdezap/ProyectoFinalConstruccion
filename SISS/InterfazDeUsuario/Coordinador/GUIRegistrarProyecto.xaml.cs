using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using System.Collections.Generic;

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

            AdministradorDeEncargados = new AdministradorDeEncargados();
            AdministradorDeEncargados.CargarEncargadosTodos();

            if (AdministradorDeOrganizaciones.Organizaciones.Count > 0)
            {
                ComboBoxOrganizacionAsociada.DisplayMemberPath = "Nombre";
                ComboBoxOrganizacionAsociada.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
                ComboBoxOrganizacionAsociada.SelectedIndex = 0;
            }
            else
            {
                ComboBoxOrganizacionAsociada.IsEnabled = false;
                ComboBoxEncargadoAsociado.IsEnabled = false;
            }

            ComboBoxEncargadoAsociado.DisplayMemberPath = "Nombre";
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
                        MessageBox.Show(this, ERROR_AL_CONVERTIR_OBJETO, "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MostrarEstadoDeValidacionCadena(TextBoxNombre);
                    MostrarEstadoDeValidacionCadena(TextBoxObjetivoGeneral);
                    MostrarEstadoDeValidacionCadena(TextBoxDescripcionGeneral);
                    MostrarEstadoDeValidacionCampoNumerico(TextBoxEstudiantesSolicitados);
                    Mouse.OverrideCursor = null;
                }
            }
			else
			{
                MessageBox.Show(this, "Seleccione un encargado.");
            }
		}

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena(TextBoxNombre);
        }

        private void TextBoxEstudiantesSolicitados_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCampoNumerico(TextBoxEstudiantesSolicitados);
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBoxOrganizacionAsociada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indiceOrganizacion = ComboBoxOrganizacionAsociada.SelectedIndex;
            if (indiceOrganizacion > SIN_INDICE)
            {
                Organizacion organizacion = ComboBoxOrganizacionAsociada.SelectedItem as Organizacion;

                List<Encargado> encargadosPorOrganizacion = AdministradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(organizacion.IDOrganizacion);
                if (encargadosPorOrganizacion.Count > 0)
                {
                    ComboBoxEncargadoAsociado.ItemsSource = encargadosPorOrganizacion;
                    ComboBoxEncargadoAsociado.SelectedIndex = 0;
                }
                else
                {
                    ComboBoxEncargadoAsociado.IsEnabled = false;
                }
            }
        }

        private void TextBoxObjetivoGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena(TextBoxObjetivoGeneral);
        }

        private void TextBoxDescripcionGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena(TextBoxDescripcionGeneral);
        }
    }
}
