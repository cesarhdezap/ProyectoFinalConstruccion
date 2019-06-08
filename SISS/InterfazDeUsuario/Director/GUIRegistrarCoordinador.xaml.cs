using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios;
using LogicaDeNegocios.Servicios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
namespace InterfazDeUsuario.GUIsDeDirector
{
    /// <summary>
    /// Interaction logic for GUIRegistrarCoordinador.xaml
    /// </summary>
    public partial class GUIRegistrarCoordinador : Window
    {
        private Director Director { get; set; }
        public GUIRegistrarCoordinador(Director director)
        {
            InitializeComponent();
            this.Director = director;
            LblNombreDeUsuario.Content = director.Nombre;
            InitializeComponent();
            CbxCarrera.Items.Add("LIS");
            CbxCarrera.Items.Add("RYSC");
            CbxCarrera.Items.Add("TC");
            CbxCarrera.SelectedIndex = 0;
        }

        private void TxtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarNombre(TxtNombre.Text) == ResultadoDeValidacion.Valido)
            {
                TxtNombre.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtNombre.BorderBrush = Brushes.Red;
            }
        }

        private void TxtCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCorreoElectronico(TxtCorreoElectronico.Text) == ResultadoDeValidacion.Valido)
            {
                TxtCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtCorreoElectronico.BorderBrush = Brushes.Red;
            }

            if (TxtCorreoElectronico.Text == TxtConfirmarCorreoElectronico.Text)
            {
                TxtConfirmarCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtConfirmarCorreoElectronico.BorderBrush = Brushes.Red;
            }
        }

        private void TxtConfirmarCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtCorreoElectronico.Text == TxtConfirmarCorreoElectronico.Text)
            {
                TxtConfirmarCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtConfirmarCorreoElectronico.BorderBrush = Brushes.Red;
            }
        }

        private void TxtTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarTelefono(TxtTelefono.Text) == ResultadoDeValidacion.Valido)
            {
                TxtTelefono.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtTelefono.BorderBrush = Brushes.Red;
            }
        }

        private void TxtContraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarContraseña(TxtContraseña.Text) == ResultadoDeValidacion.Valido)
            {
                TxtContraseña.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtContraseña.BorderBrush = Brushes.Red;
            }
            if (TxtContraseña.Text == TxtConfirmarContraseña.Text)
            {
                TxtConfirmarContraseña.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtConfirmarContraseña.BorderBrush = Brushes.Red;
            }
        }

        private void TxtConfirmarContraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtContraseña.Text == TxtConfirmarContraseña.Text)
            {
                TxtConfirmarContraseña.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtConfirmarContraseña.BorderBrush = Brushes.Red;
            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            DocenteAcademico coordinador = new DocenteAcademico
            {
                Nombre = TxtNombre.Text,
                CorreoElectronico = TxtCorreoElectronico.Text,
                Telefono = TxtTelefono.Text,
                Coordinador = null,
                Carrera = CbxCarrera.SelectedValue.ToString(),
                EsActivo = true,
                Contraseña = ServiciosDeAutenticacion.EncriptarContraseña(TxtContraseña.Text)
            };
            try
            {
                coordinador.Cubiculo = Int32.Parse(TxtCubiculo.Text);
                if (ValidarCoordinador(coordinador) == ResultadoDeValidacion.Valido && TxtCorreoElectronico.Text == TxtConfirmarCorreoElectronico.Text && TxtContraseña.Text == TxtConfirmarContraseña.Text && CbxCarrera.SelectedValue != null)
                {
                    DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
                    try
                    {
                        docenteAcademicoDAO.GuardarDocenteAcademico(coordinador);
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
                    {
                        Mouse.OverrideCursor = null;
                        MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ObjetoNoExiste)
                    {
                        Mouse.OverrideCursor = null;
                        MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
                    {
                        Mouse.OverrideCursor = null;
                        MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    Mouse.OverrideCursor = null;
                    MessageBoxResult messageBoxCerrada = MessageBox.Show("Ha sido registrado exitosamente.", "¡Registro Exitoso!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
                    if (messageBoxCerrada == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Porfavor compruebe los campos remarcados en rojo.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } catch (InvalidCastException ex)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Porfavor compruebe los campos remarcados en rojo.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtCubiculo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
