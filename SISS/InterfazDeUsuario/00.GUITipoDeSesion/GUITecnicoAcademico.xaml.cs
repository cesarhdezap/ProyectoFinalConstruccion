﻿using LogicaDeNegocios.ClasesDominio;
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
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;


namespace InterfazDeUsuario.GUITipoDeSesion
{
    /// <summary>
    /// Interaction logic for GUITecnicoAcademico.xaml
    /// </summary>
    public partial class GUITecnicoAcademico : Window
    {
        private DocenteAcademico TecnicoAdministrativo { get; set; }
        public GUITecnicoAcademico(Sesion sesion)
        {
            InitializeComponent();

            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            this.TecnicoAdministrativo = new DocenteAcademico();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                this.TecnicoAdministrativo = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(Int32.Parse(sesion.IDUsuario));
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
			LabelNombreDeUsuario.Content = this.TecnicoAdministrativo.Nombre;
        }

        private void ButtonBuscarAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIsDeTecnicoAcademico.GUIBuscarAlumnoPorTecnicoAcademico buscarAlumnoPorTecnicoAcademico = new GUIsDeTecnicoAcademico.GUIBuscarAlumnoPorTecnicoAcademico(TecnicoAdministrativo);
            buscarAlumnoPorTecnicoAcademico.ShowDialog();
        }
    }
}
