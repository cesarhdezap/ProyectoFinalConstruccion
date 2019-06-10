using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios;
using LogicaDeNegocios.Servicios;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    /// <summary>
    /// Interaction logic for GUIRegistrarAlumno.xaml
    /// </summary>
    public partial class GUIRegistrarAlumno : Window
    {
        public GUIRegistrarAlumno()
        {
            InitializeComponent();
            CbxCarrera.Items.Add("LIS");
            CbxCarrera.Items.Add("RYSC");
            CbxCarrera.Items.Add("TC");
            CbxCarrera.SelectedIndex = 0;
            CbxCarrera.SelectedItem = 0;
        }

        private void TxtMatricula_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarMatricula (TxtMatricula.Text))
            {
                TxtMatricula.BorderBrush = Brushes.Green;
            } else
            {
                TxtMatricula.BorderBrush = Brushes.Red;
            }

        }

        private void TxtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarNombre(TxtNombre.Text))
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
            if (ValidarCorreoElectronico(TxtCorreoElectronico.Text))
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
            if (ValidarTelefono(TxtTelefono.Text))
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
            if (ValidarContraseña(TxtContraseña.Text))
            {
                TxtContraseña.BorderBrush = Brushes.Green;
            }
            else
            {
                TxtContraseña.BorderBrush = Brushes.Red;
            }
            TxtConfirmarContraseña_TextChanged(sender, e);
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Alumno alumno = new Alumno
            {
                Nombre = TxtNombre.Text,
                CorreoElectronico = TxtCorreoElectronico.Text,
                Telefono = TxtTelefono.Text,
                Matricula = TxtMatricula.Text,
                Carrera = CbxCarrera.SelectedValue.ToString(),
                EstadoAlumno = EstadoAlumno.EsperandoAceptacion,
                Contraseña = ServiciosDeAutenticacion.EncriptarContraseña(TxtContraseña.Text)
            };

            if (TxtCorreoElectronico.Text == TxtConfirmarCorreoElectronico.Text && TxtContraseña.Text == TxtConfirmarContraseña.Text)
            {
                bool resultadoDeCreacion = false;
                try
                {
                    resultadoDeCreacion = alumno.Guardar();
                } 
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.InsercionFallidaPorLlavePrimariDuplicada)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Hubo un error al completar el registro. La matricula ingresada ya existe.", "Matricula duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ObjetoNoExiste)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }

                if (resultadoDeCreacion)
                {
                    MessageBox.Show("Ha sido registrado exitosamente.", "¡Registro Exitoso!");
                    Mouse.OverrideCursor = null;
                    Close();
                }
            }
            else
            {
				Mouse.OverrideCursor = null;
				MessageBox.Show("Porfavor compruebe los campos remarcados en rojo.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
