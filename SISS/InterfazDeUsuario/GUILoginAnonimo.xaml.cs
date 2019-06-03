using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Servicios;
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
            this.Show();
        }

        private void ButtonIngresar_Click(object sender, RoutedEventArgs e)
        {
            ResultadoDeAutenticacion resultadoDeAutenticacion;
            resultadoDeAutenticacion = AutenticarCredenciales(TextBoxCorreo.Text,PasswordBoxContraseña.Password);
            
            if (resultadoDeAutenticacion == ResultadoDeAutenticacion.Valido)
            {
                
            }
            



        }
    }
}
