namespace LogicaDeNegocios
{
	public class DocenteAcademico : Persona
	{
		public int IDPersonal { get; set; }
		public string Carrera { get; set; }
		public string Contraseña { get; set; }
		public int Cubiculo { get; set; }
		public bool EsActivo { get; set; }
		public DocenteAcademico Coordinador { get; set; }
		public ERol Rol { get; set; }

        public void Desactivar()
		{
			this.EsActivo = false;
		}

	}
	
	public enum ERol
	{
		TecnicoAcademico,
		Coordinador
	}
}
