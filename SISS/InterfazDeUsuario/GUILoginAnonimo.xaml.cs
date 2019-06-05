using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
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

        public TipoDeSesion IdentificarSesion()
        {
            throw new NotImplementedException();
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
                SesionDAO sesionDAO = new SesionDAO();
                tipoDeSesion = sesionDAO.CargarTipoDeSesionPorCorreo(correo);

                if (tipoDeSesion == TipoDeSesion.Alumno)
                {
                    sesion.IDUsuario = sesionDAO.CargarMatriculaDeAlumnoPorCorreo(correo);
                }
                else if (tipoDeSesion != TipoDeSesion.NoValido)
                {
                    sesion.IDUsuario = sesionDAO.CargarIDDeUsuarioPorCorreo(correo).ToString();
                }
                sesion.TipoDeUsuario = tipoDeSesion;
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }
    }
}
