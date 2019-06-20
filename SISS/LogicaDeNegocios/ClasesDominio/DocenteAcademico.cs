using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;

namespace LogicaDeNegocios
{
    /// <summary>
    /// Clase <see cref="DocenteAcademico"/>.
    /// Contiene todos los métodos para realizar operaciones con la base de datos.
    /// <para>Hereda de <see cref="Persona"/>.</para>
    /// </summary>
	public class DocenteAcademico : Persona
	{
		public int IDPersonal { get; set; }
		public string Carrera { get; set; }
		public string Contraseña { get; set; }
		public int Cubiculo { get; set; }
		public bool EsActivo { get; set; }
		public DocenteAcademico Coordinador { get; set; }
		public Rol Rol { get; set; }

        /// <summary>
        /// Convierte los atributos del <see cref="DocenteAcademico"/>
        /// a una cadena con espacios para debugging.
        /// </summary>
        /// <returns>Cadena con los datos de los atributos.</returns>
        public override string ToString()
        {
            string docenteAcademico = System.Environment.NewLine +
                                      "IDPersonal: " + IDPersonal + System.Environment.NewLine +
                                      "Nombre: " + Nombre + System.Environment.NewLine +
                                      "CorreoElectronico: " + CorreoElectronico + System.Environment.NewLine +
                                      "Telefono: " + Telefono + System.Environment.NewLine +
                                      "Cubiculo: " + Cubiculo + System.Environment.NewLine +
                                      "EsActivo: " + EsActivo + System.Environment.NewLine +
                                      "Rol: " + Rol.ToString() + System.Environment.NewLine;
            return docenteAcademico;
        }

        /// <summary>
        /// Carga el primer <see cref="DocenteAcademico"/> de la base de datos
        /// con el <see cref="Rol.Coordinador"/> de la <paramref name="carrera"/>.
        /// </summary>
        /// <param name="carrera">Cadena con la carrera a buscar.</param>
        /// <returns>DocenteAcademico con <see cref="Rol.Coordinador"/> de la <paramref name="carrera"/></returns>
		public DocenteAcademico CargarCoordinadorPorCarrera(string carrera)
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
			DocenteAcademico docenteAcademico = new DocenteAcademico();
			docenteAcademico = docenteAcademicoDAO.CargarIDCoordinadorPorCarrera(carrera);
			docenteAcademico = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(docenteAcademico.IDPersonal);
			return docenteAcademico;
		}

        /// <summary>
        /// Guarda el <see cref="DocenteAcademico"/> en la base
        /// de datos.
        /// </summary>
		public void Guardar()
		{
			DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            string contraseñaEncriptada = EncriptarContraseña(Contraseña);
            Contraseña = contraseñaEncriptada;
            docenteAcademicoDAO.GuardarDocenteAcademico(this);
		}

        /// <summary>
        /// Valida si los atributos del <see cref="DocenteAcademico"/> 
        /// son correctos para la inserción a base de datos.
        /// </summary>
        /// <returns>Si sus atributos son válidos.</returns>
		public bool Validar()
		{
			bool resultadoDeValidacion = false;

			if (ValidarContraseña(Contraseña)
				&& ValidarCorreoElectronico(CorreoElectronico)
				&& ValidarNombre(Nombre)
				&& ValidarTelefono(Telefono)
                && Cubiculo > VALOR_ENTERO_MINIMO_PERMITIDO
                && ValidarDisponibilidadDeCorreo(CorreoElectronico))
            {
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}
	}
	
    /// <summary>
    /// Enumerador con los roles del <see cref="DocenteAcademico"/>.
    /// </summary>
	public enum Rol
	{
        Coordinador,
        TecnicoAcademico
	}
}
