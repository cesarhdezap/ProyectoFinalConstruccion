﻿using System;
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
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    /// <summary>
    /// Interaction logic for GUIVerExpedientePorAlumno.xaml
    /// </summary>
    public partial class GUIVerExpedientePorAlumno : Window
    {
        private AdministradorDeReportesMensuales AdministradorDeReportesMensuales { get; set; }
        private AdministradorDeDocumentosDeEntregaUnica AdministradorDeDocumentosDeEntregaUnica { get; set; }
        private Alumno Alumno = new Alumno();
        public GUIVerExpedientePorAlumno(Alumno alumno)
        {
            InitializeComponent();
            this.Alumno = alumno;
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Asignacion asignacion = new Asignacion();
            AdministradorDeDocumentosDeEntregaUnica = new AdministradorDeDocumentosDeEntregaUnica();
            AdministradorDeReportesMensuales = new AdministradorDeReportesMensuales();
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            try
            {
                asignacion = asignacionDAO.CargarIDsPorMatriculaDeAlumno(Alumno.Matricula).ElementAt(0);
                asignacion = asignacionDAO.CargarAsignacionPorID(asignacion.IDAsignacion);
                AdministradorDeDocumentosDeEntregaUnica.CargarDocumentosDeEntregaUnicaPorMatricula(Alumno.Matricula);
                AdministradorDeReportesMensuales.CargarReportesMensualesPorMatricula(Alumno.Matricula);
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
            LblHorasCubiertas.Content = asignacion.ObtenerHorasCubiertas();
            LblNombreDeUsuario.Content = Alumno.Nombre;
            GrdDocumentosDeEntregaUnica.ItemsSource = AdministradorDeDocumentosDeEntregaUnica.DocumentosDeEntregaUnica;
            GrdReportesMensuales.ItemsSource = AdministradorDeReportesMensuales.ReportesMensuales;
        }

        private void BtnVerProyectoActual_Click(object sender, RoutedEventArgs e)
        {
            GUIVerProyectoActual verProyectoActual = new GUIVerProyectoActual(Alumno);
            verProyectoActual.ShowDialog();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}