using LogicaDeNegocios.ClasesDominio;
using System.Windows;
using static LogicaDeNegocios.Servicios.ServiciosDeSesion;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;
using InterfazDeUsuario.GUITipoDeSesion;
using InterfazDeUsuario.GUIsDeAlumno;

namespace InterfazDeUsuario
{
    public partial class GUILoginAnonimo : Window
    {
        public GUILoginAnonimo()
        {
            InitializeComponent();
        }

        private void ButtonIngresar_Click(object sender, RoutedEventArgs e)
        {
            string correo = TextBoxCorreo.Text;
            if (correo != null && PasswordBoxContraseña.Password != null)
            {
                bool resultadoDeAutenticacion = AutenticarCredenciales(correo, PasswordBoxContraseña.Password);
                if (resultadoDeAutenticacion)
                {
                    Sesion sesion = CargarSesion(correo);
                    Hide();
                    InstanciarVentanaDeSesion(sesion);
                    Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña no validos.");
                }
            }
            else
            {
                MessageBox.Show("No se han detectado datos.");
            }
        }

        private void InstanciarVentanaDeSesion(Sesion sesion)
        {
            if (sesion.TipoDeUsuario == TipoDeSesion.Alumno)
            {
                GUIAlumno interfazAlumno = new GUIAlumno(sesion);
                interfazAlumno.ShowDialog();
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Coordinador)
            {
                GUICoordinador interfazCoordinador = new GUICoordinador(sesion);
                interfazCoordinador.ShowDialog();
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Director)
            {
                GUIDirector interfazDirector = new GUIDirector(sesion);
                interfazDirector.ShowDialog();
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Tecnico)
            {
                GUITecnicoAcademico interfazTecnico = new GUITecnicoAcademico(sesion);
                interfazTecnico.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tipo de sesion no valida.");
            }
        }

        private void ButtonRegistrarseComoAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarAlumno registrarAlumno = new GUIRegistrarAlumno();
            registrarAlumno.Show();
        }
    }
}
