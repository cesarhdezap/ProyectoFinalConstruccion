using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IAlumnoDAO
	{
		void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno);
        List<Alumno> CargarAlumnosPorEstado(EstadoAlumno estadoAlumno);
        Alumno CargarAlumnoPorMatricula(string matricula);
        List<Alumno> CargarAlumnosTodos();
        DataTable ConvertirAlumnoADataTable(Alumno alumno);
        Alumno ConvertirDataTableAAlumno(DataTable alumno);
        List<Alumno> ConvertirDataTableAListaDeAlumnos(DataTable alumno);
        void GuardarAlumno(Alumno alumno);

	}
}
