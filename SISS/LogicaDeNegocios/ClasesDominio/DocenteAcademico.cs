using LogicaDeNegocios.ObjetoAccesoDeDatos;

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
		public Rol Rol { get; set; }


        public override string ToString()
        {
            string docenteAcademico = System.Environment.NewLine +
                                      "IDPersonal: " + this.IDPersonal + System.Environment.NewLine +
                                      "Nombre: " + this.Nombre + System.Environment.NewLine +
                                      "CorreoElectronico: " + this.CorreoElectronico + System.Environment.NewLine +
                                      "Telefono: " + this.Telefono + System.Environment.NewLine +
                                      "Cubiculo: " + this.Cubiculo + System.Environment.NewLine +
                                      "EsActivo: " + this.EsActivo + System.Environment.NewLine +
                                      "Rol: " + this.Rol.ToString() + System.Environment.NewLine;

            return docenteAcademico;
        }
        public void Desactivar()
		{
            EsActivo = false;
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            docenteAcademicoDAO.ActualizarDocenteAcademicoPorIDPersonal(IDPersonal, this);

            this.EsActivo = false;
        }

	}
	
	public enum Rol
	{
        Coordinador = 1,
        TecnicoAcademico = 2
	}
}
