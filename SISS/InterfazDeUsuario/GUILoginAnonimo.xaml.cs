using System;
using System.Windows;
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
    }
}
