using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using LogicaDeNegocios.Excepciones;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class PruebasAsignacionDAO
    {
        [TestMethod]
        public void ProbarGuardarAsignacion_SinExcepcion_RegresaVoid()
        {
            Alumno alumno = new Alumno()
            {
                Matricula = "s16012931"
            };
            Proyecto proyecto = new Proyecto()
            {
                IDProyecto = 999
            };
            Solicitud solicitud = new Solicitud()
            {
                IDSolicitud = 999
            };
            Asignacion asignacion = new Asignacion()
            {
                EstadoAsignacion = EstadoAsignacion.Activo,
                FechaDeInicio = DateTime.Now,
                Alumno = alumno,
                Proyecto = proyecto,
                Solicitud = solicitud
            };

            AsignacionDAO asignacionDAO = new AsignacionDAO();
            asignacionDAO.GuardarAsignacion(asignacion);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ProbarGuardarAsignacion_IDProyectoErronea_RegresaAccesoADatosException()
        {
            Alumno alumno = new Alumno()
            {
                Matricula = "s16012931"
            };
            Proyecto proyecto = new Proyecto()
            {
                IDProyecto = 0
            };
            Solicitud solicitud = new Solicitud()
            {
                IDSolicitud = 999
            };
            Asignacion asignacion = new Asignacion()
            {
                IDAsignacion = 4,
                EstadoAsignacion = EstadoAsignacion.Activo,
                FechaDeInicio = DateTime.Now,
                Alumno = alumno,
                Proyecto = proyecto,
                Solicitud = solicitud
            };

            AsignacionDAO asignacionDAO = new AsignacionDAO();
            TipoDeErrorDeAccesoADatos tipoDeError = TipoDeErrorDeAccesoADatos.IDInvalida;
            bool excepcionTirada = false;
            try
            {
                asignacionDAO.GuardarAsignacion(asignacion);
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == tipoDeError)
            {
                excepcionTirada = true;
            }

            if (!excepcionTirada)
            {
                throw new AssertFailedException("Se esperaba AccesoADatosException con el error " + tipoDeError.ToString());
            }
        }

        [TestMethod]
        public void ProbarActualizarAsignacionPorID_SinExcepcion_RegresaVoid()
        {
            Alumno alumno = new Alumno()
            {
                Matricula = "s99999999"
            };
            Proyecto proyecto = new Proyecto()
            {
                IDProyecto = 1000
            };
            Solicitud solicitud = new Solicitud()
            {
                IDSolicitud = 1000
            };
            Liberacion liberacion = new Liberacion()
            {
                IDLiberacion = 99
            };
            Asignacion asignacion = new Asignacion()
            {
                IDAsignacion = 4,
                EstadoAsignacion = EstadoAsignacion.Activo,
                FechaDeInicio = DateTime.Now,
                Alumno = alumno,
                Proyecto = proyecto,
                Solicitud = solicitud,
                Liberacion = liberacion
            };
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            asignacionDAO.ActualizarAsignacionPorID(asignacion.IDAsignacion, asignacion);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ProbarActualizarAsignacionPorID_IDAsignacionNoExiste_RegresaAccesoADatosException()
        {
            Alumno alumno = new Alumno()
            {
                Matricula = "s99999999"
            };
            Proyecto proyecto = new Proyecto()
            {
                IDProyecto = 1000
            };
            Solicitud solicitud = new Solicitud()
            {
                IDSolicitud = 1000
            };
            Liberacion liberacion = new Liberacion()
            {
                IDLiberacion = 99
            };
            Asignacion asignacion = new Asignacion()
            {
                IDAsignacion = 9999,
                EstadoAsignacion = EstadoAsignacion.Activo,
                FechaDeInicio = DateTime.Now,
                Alumno = alumno,
                Proyecto = proyecto,
                Solicitud = solicitud,
                Liberacion = liberacion
            };

            bool excepcionTirada = false;
            TipoDeErrorDeAccesoADatos tipoDeError = TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto;
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            try
            {
                asignacionDAO.ActualizarAsignacionPorID(asignacion.IDAsignacion, asignacion);
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == tipoDeError)
            {
                excepcionTirada = true;
            }

            if (!excepcionTirada)
            {
                throw new AssertFailedException("Se esperaba AccesoADatosException con el error " + tipoDeError.ToString());
            }
        }

        [DataTestMethod]
        [DataRow(4)]
        public void ProbarCargarAsignacionPorID_AsignacionEncontradaSinExcepciones_RegresaAsignacion(int IDAsignacion)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Asignacion asignacion = asignacionDAO.CargarAsignacionPorID(IDAsignacion);
            Assert.AreEqual(IDAsignacion, asignacion.IDAsignacion);
        }
        
        [TestMethod]
        public void ProbarCargarAsignacionPorID_AsignacionNoEncontrada_RegresaAsignacion()
        {
            int IDAsignacion = 9999;
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Asignacion asignacion = asignacionDAO.CargarAsignacionPorID(IDAsignacion);
            Assert.AreNotEqual(IDAsignacion, asignacion.IDAsignacion);
        }

        [DataTestMethod]
        [DataRow("s16012931",4)]
        public void ProbarCargarIDPorMatriculaDeAlumno_IDEncontradaSinExcepciones_RegresaAsignacion(string matricula, int IDAsignacion)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Asignacion asignacion = asignacionDAO.CargarIDPorMatriculaDeAlumno(matricula);
            Assert.AreEqual(IDAsignacion, asignacion.IDAsignacion);
        }

        [DataTestMethod]
        [DataRow(999, 4)]
        public void ProbarCargarIDsPorIDProyecto_SinExcepciones_RegresaAsignacion(int IDProyecto, int IDAsignacion)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            List<Asignacion> asignacion = asignacionDAO.CargarIDsPorIDProyecto(IDProyecto);
            Assert.IsTrue(true);
        }

    }
}
