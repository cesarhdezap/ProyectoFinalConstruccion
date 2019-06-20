namespace InterfazDeUsuario
{
	public class MensajeDeErrorParaMessageBox
	{
		public string Titulo { get; }
		public string Mensaje { get; }

		public MensajeDeErrorParaMessageBox()
		{
			Titulo = "Error desconocido";
			Mensaje = "Error de AccesoADatos desconocido en ManjearExcepcionDeAccesoADatos.";
		}

		public MensajeDeErrorParaMessageBox(string mensaje, string titulo)
		{
			Mensaje = mensaje;
			Titulo = titulo;
		}
	}

}
