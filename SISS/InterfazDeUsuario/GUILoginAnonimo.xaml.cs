using InterfazDeUsuario.GUITipoDeSesion;
using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using System.Windows;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;
using static LogicaDeNegocios.ServiciosDeSesion;

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
            ResultadoDeAutenticacion resultadoDeAutenticacion;
            resultadoDeAutenticacion = AutenticarCredenciales(correo, PasswordBoxContraseña.Password);
            Sesion sesion = new Sesion();

            if (resultadoDeAutenticacion == ResultadoDeAutenticacion.Valido)
            {
                TipoDeSesion tipoDeSesion = TipoDeSesion.NoValido;
                tipoDeSesion = CargarTipoDeSesionPorCorreo(correo);

                if (tipoDeSesion == TipoDeSesion.Alumno)
                {
                    sesion.IDUsuario = ServiciosDeSesion.CargarMatriculaDeAlumnoPorCorreo(correo);
                }
                else if (tipoDeSesion != TipoDeSesion.NoValido)
                {
                    sesion.IDUsuario = CargarIDDeUsuarioPorCorreo(correo).ToString();
                }
                sesion.TipoDeUsuario = tipoDeSesion;
                InstanciarVentanaDeSesion(sesion);
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
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
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Director)
            {
                GUIDirector interfazDirector = new GUIDirector(sesion);
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Tecnico)
            {
                GUITecnicoAcademico interfazTecnico = new GUITecnicoAcademico(sesion);
            }
            else
            {
                MessageBox.Show("Tipo de sesion no valida.");
            }
            Hide();
        }
    }
}
