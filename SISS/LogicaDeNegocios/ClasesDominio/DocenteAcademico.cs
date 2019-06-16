using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;

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

            EsActivo = false;
        }

		public DocenteAcademico CargarCoordinadorPorCarrera(string carrera)
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			DocenteAcademico docenteAcademico = new DocenteAcademico();
			docenteAcademico = docenteAcademicoDAO.CargarIDCoordinadorPorCarrera(carrera);
			docenteAcademico = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(docenteAcademico.IDPersonal);
			return docenteAcademico;
		}

		public void Guardar()
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            string contraseñaEncriptada = EncriptarContraseña(Contraseña);
            Contraseña = contraseñaEncriptada;
            docenteAcademicoDAO.GuardarDocenteAcademico(this);
		}

		public bool Validar()
		{
			bool resultadoDeValidacion = false;
			if (ValidarContraseña(Contraseña)
				&& ValidarCorreoElectronico(CorreoElectronico)
				&& ValidarNombre(Nombre)
				&& ValidarTelefono(Telefono)
                && Cubiculo > VALOR_ENTERO_MINIMO_PERMITIDO)
            {
				resultadoDeValidacion = true;
			}
			return resultadoDeValidacion;
		}
	}
	
	public enum Rol
	{
        Coordinador,
        TecnicoAcademico
	}
}
