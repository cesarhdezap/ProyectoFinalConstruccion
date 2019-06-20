namespace LogicaDeNegocios.Interfaces
{
	interface IServiciosDeValidacionDAO
    {
        int ContarOcurrenciasDeCorreo(string correo);
        int ContarOcurrenciasDeMatricula(string matricula);
    }
}
