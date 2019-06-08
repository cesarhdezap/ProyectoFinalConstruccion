using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;


namespace LogicaDeNegocios
{
	public class Alumno : Persona
	{
		private const int MAXIMO_DE_ASIGNACIONES = 2;
		public string Matricula { get; set; }
		public string Carrera { get; set; }
		public string Contraseña { get; set; }
		public EstadoAlumno EstadoAlumno { get; set; }
		public List<Asignacion> Asignaciones { get; set; }	

		public void DarDeBaja()
		{
			EstadoAlumno = EstadoAlumno.DadoDeBaja;
            ActualizarRegistroDeAlumno();
        }

		public void Aceptar()
		{
			EstadoAlumno = EstadoAlumno.Aceptado;
            ActualizarRegistroDeAlumno();
        }

		public void Rechazar()
		{
			EstadoAlumno = EstadoAlumno.Rechazado;
            ActualizarRegistroDeAlumno();
        }

        public void Solicitar()
        {
            EstadoAlumno = EstadoAlumno.EsperandoAsignacion;
            ActualizarRegistroDeAlumno();
        }

        public override string ToString()
        {
            
            string alumno = System.Environment.NewLine +
                            "Matricula: " + this.Matricula + System.Environment.NewLine +
                            "Nombre: " + this.Nombre + System.Environment.NewLine +
                            "Correo Electronico: " + this.CorreoElectronico + System.Environment.NewLine +
                            "Telefono: " + this.Telefono + System.Environment.NewLine +
                            "Estado: " + this.EstadoAlumno.ToString() + System.Environment.NewLine +
                            "Carrera" + this.Carrera;

            return alumno;
        }

        private void ActualizarRegistroDeAlumno()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            alumnoDAO.ActualizarAlumnoPorMatricula(this.Matricula, this);
        }
    }

	public enum EstadoAlumno
	{
		EsperandoAceptacion, 
        Aceptado,
        Rechazado,
        EsperandoAsignacion,
        Asignado,
		Liberado,
		DadoDeBaja,
		
	}
}
