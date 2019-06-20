using static LogicaDeNegocios.Servicios.ServiciosDeSesion;

namespace LogicaDeNegocios.ClasesDominio
{
	public class Sesion
    {
        public string IDUsuario { get; set; }
        public TipoDeSesion TipoDeUsuario { get; set; }            
    }
}
