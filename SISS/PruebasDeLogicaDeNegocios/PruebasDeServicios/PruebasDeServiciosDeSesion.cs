using System;
using LogicaDeNegocios.ClasesDominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LogicaDeNegocios.Servicios.ServiciosDeSesion;

namespace Pruebas.PruebasDeServicios
{
    [TestClass]
    public class PruebasDeServiciosDeSesion
    {
        [DataTestMethod]
        [DataRow("cesarhdez@gmail.com", "s17012947", TipoDeSesion.Alumno)]
        [DataRow("raul@correo.com", "1", TipoDeSesion.Director)]
        [DataRow("julio@correo.com", "1", TipoDeSesion.Coordinador)]
        [DataRow("joseph@correo.com", "4",TipoDeSesion.Tecnico)]
        public void ProbarCargarSesion_Limites_RegresaSesion(string correo, string IDEsperada, TipoDeSesion tipoDeSesionEsperada)
        {
            Sesion sesionActual = new Sesion();
            sesionActual = CargarSesion(correo);

            if (sesionActual.IDUsuario != IDEsperada)
            {
                string mensaje = "IDActual: " + sesionActual.IDUsuario + " IDEsperada: " + IDEsperada;
                if (sesionActual.TipoDeUsuario != tipoDeSesionEsperada)
                {
                    mensaje += "\nTipoDeUsuarioActual: " + sesionActual.TipoDeUsuario + " TipoDeUsuarioEsperado: " + tipoDeSesionEsperada;
                }
                Assert.Fail(mensaje);
            }
            else
            {
                
                if (sesionActual.TipoDeUsuario != tipoDeSesionEsperada)
                {
                    string mensaje = "TipoDeUsuarioActual: " + sesionActual.TipoDeUsuario + " TipoDeUsuarioEsperado: " + tipoDeSesionEsperada;
                    Assert.Fail(mensaje);
                }
            }

            Assert.IsTrue(true);
        }
    }
}
