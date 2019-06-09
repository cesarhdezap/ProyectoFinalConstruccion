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
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            if (ValidarAlumno(alumno) && TxtCorreoElectronico.Text == TxtConfirmarCorreoElectronico.Text && TxtContraseña.Text == TxtConfirmarContraseña.Text && CbxCarrera.SelectedValue != null)
            {   
                AlumnoDAO alumnoDAO = new AlumnoDAO();
                try
                {
                    alumnoDAO.GuardarAlumno(alumno);
                } 
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.InsercionFallidaPorLlavePrimariDuplicada)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Hubo un error al completar el registro. La matricula ingresada ya existe.", "Matricula duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
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
                Mouse.OverrideCursor = Cursors.Wait;
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
        }
    }
}
