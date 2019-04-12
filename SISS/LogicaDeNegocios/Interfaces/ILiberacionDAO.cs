namespace LogicaDeNegocios.Interfaces
{
	interface ILiberacionDAO
	{
        int GuardarLiberacion(Liberacion liberacion);
        Liberacion CargarLiberacionPorIDAsignacion(int IDasignacion);
	}
}
