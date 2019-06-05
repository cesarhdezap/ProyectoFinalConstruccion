using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Excepciones;
using System.Data.SqlClient;
using System.Linq;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AsignacionDAO : IAsignacionDAO
	{
		public void ActualizarAsignacionPorID(int IDAsignacion, Asignacion asignacion)
		{
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al Actualizar Asignacion Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }
            SqlParameter[] parametrosDeAsignacion = InicializarParametrosDeSql(asignacion);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("UPDATE Asignaciones SET EstadoAsignacion = @EstadoAsignacion, FechaDeFinal = @FechaDeFinalAsignacion, IDLiberacion = @IDLiberacionAsignacion WHERE IDAsignacion = @IDAsignacion ", parametrosDeAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al actualizar Asignacion: " + asignacion.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("La Asignacion con IDAsignacion: " + IDAsignacion + " no existe.");
            }
        }

		public Asignacion CargarAsignacionPorID(int IDAsignacion)
		{
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar Asignacion Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }
            DataTable tablaDeAsignacion = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };
            try
            {
                tablaDeAsignacion = AccesoADatos.EjecutarSelect("SELECT * FROM Asignaciones WHERE IDAsignacion = @IDAsignacion", parametroIDAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Asignacion con IDAsigncion: " + IDAsignacion, e);
            }

            Asignacion asignacion = new Asignacion();
            try
            {
                asignacion = ConvertirDataTableAAsignacion(tablaDeAsignacion);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Asignacion en cargar Asignacion con IDAsignacion: " + IDAsignacion, e);
            }
            return asignacion;

		}
        public List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula)
		{
            DataTable tablaDeAsignaciones = new DataTable();
            SqlParameter[] parametroMatricula = new SqlParameter[1];
            parametroMatricula[0] = new SqlParameter
            {
                ParameterName = "@Matricula",
                Value = matricula
            };
            try
            {
                tablaDeAsignaciones = AccesoADatos.EjecutarSelect("SELECT IDAsignacion FROM Alumnos WHERE Matricula = @Matricula", parametroMatricula);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDAsignacion con matricula: " + matricula, e);
            }

            List<Asignacion> asignaciones = new List<Asignacion>();
            try
            {
                asignaciones = ConvertirDataTableAListaDeAsignacionesConSoloID(tablaDeAsignaciones);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista de Asignaciones en cargar Asignacion con matricula: " + matricula, e);
            }
            return asignaciones;
        }


        private Asignacion ConvertirDataTableAAsignacion(DataTable tablaDeAsignaciones)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            Asignacion asignacion = new Asignacion();
            foreach (DataRow fila in tablaDeAsignaciones.Rows) {
                asignacion.IDAsignacion = (int)fila["IDAsignacion"];
                asignacion.EstadoAsignacion = (EstadoAsignacion)fila["estadoAsignacion"];
                asignacion.FechaDeInicio = DateTime.Parse(fila["fechaDeInicio"].ToString());
                asignacion.FechaDeFinal = DateTime.Parse(fila["fechaDeFinal"].ToString());
                asignacion.Alumno = alumnoDAO.CargarMatriculaPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.Proyecto = proyectoDAO.CargarIDProyectoPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion((int)fila["IDAsignacion"]);
                asignacion.Liberacion = new Liberacion { IDLiberacion = (int)fila["IDLiberacion"] };
                asignacion.Solicitud = new Solicitud { IDSolicitud =(int)fila["IDLiberacion"]};
            }

            return asignacion;
        }

        private List<Asignacion> ConvertirDataTableAListaDeAsignacionesConSoloID(DataTable dataTableAsignaciones)
        {
            List<Asignacion> listaDeAsignaciones = new List<Asignacion>();
            foreach (DataRow fila in dataTableAsignaciones.Rows)
            {
                Asignacion asignacion = new Asignacion
                {
                    IDAsignacion = (int)fila["IDAsignacion"],
                };
                listaDeAsignaciones.Add(asignacion);
            }
            return listaDeAsignaciones;
        }

        public List<Asignacion> CargarIDsPorIDProyecto(int IDProyecto)
        {
            if (IDProyecto <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDAsignacion Por IDProyecto: " + IDProyecto + ". IDProyecto no es valido.");
            }
            DataTable tablaDeAsignaciones = new DataTable();
            SqlParameter[] parametroIDProyecto = new SqlParameter[1];
            parametroIDProyecto[0] = new SqlParameter
            {
                ParameterName = "@IDProyecto",
                Value = IDProyecto
            };

            try
            {
                tablaDeAsignaciones = AccesoADatos.EjecutarSelect("SELECT IDAsignacion FROM Asignaciones WHERE IDProyecto = @IDProyecto", parametroIDProyecto);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDsAsignacion con IDProyecto: " + IDProyecto, e);
            }
            List<Asignacion> listaDeAsignaciones = new List<Asignacion>();
            try
            {
                listaDeAsignaciones = ConvertirDataTableAListaDeAsignacionesConSoloID(tablaDeAsignaciones);
            }
            catch (FormatException e)
            { 
                throw new AccesoADatosException("Error al convertir datatable a lista de Asignaciones en cargar IDsAsignacion con IDProyecto: " + IDProyecto, e);
            }
            return listaDeAsignaciones;
        }
        public void GuardarAsignacion(Asignacion asignacion)
		{
            SqlParameter[] parametrosDeAsignacion = InicializarParametrosDeSql(asignacion);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Asignaciones(EstadoAsignacion, FechaDeInicion, HorasCubiertas, MatriculaDeAlumno, IDProyecto, IDSolicitud) VALUES(@EstadoAsignacion, @FechaDeInicio, @HorasCubiertas, @MatriculaDeAlumnoAsignacion, @IDProyectoAsignacion, @IDSolicitudAsignacion)", parametrosDeAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar Asignacion:" + asignacion.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Asignacion: " + asignacion.ToString() + " no fue guardada.");
            }
		}
        
        private SqlParameter[] InicializarParametrosDeSql(Asignacion asignacion)
        {
            SqlParameter[] parametrosDeAsignacion = new SqlParameter[9];

            for (int i = 0; i < parametrosDeAsignacion.Length; i++)
            {
                parametrosDeAsignacion[i] = new SqlParameter();
            }

            parametrosDeAsignacion[0].ParameterName = "@IDAsignacion";
            parametrosDeAsignacion[0].Value = asignacion.IDAsignacion;
            parametrosDeAsignacion[1].ParameterName = "@EstadoAsignacion";
            parametrosDeAsignacion[1].Value = asignacion.EstadoAsignacion;
            parametrosDeAsignacion[2].ParameterName = "@FechaDeInicioAsignacion";
            parametrosDeAsignacion[2].Value = asignacion.FechaDeInicio.ToString();
            parametrosDeAsignacion[3].ParameterName = "@FechaDeFinalAsignacion";
            parametrosDeAsignacion[3].Value = asignacion.FechaDeFinal.ToString();
            parametrosDeAsignacion[4].ParameterName = "@MatriculaDeAlumnoAsignacion";
            parametrosDeAsignacion[4].Value = asignacion.Alumno.Matricula;
            parametrosDeAsignacion[5].ParameterName = "@IDProyectoAsignacion";
            parametrosDeAsignacion[5].Value = asignacion.Proyecto.IDProyecto;
            parametrosDeAsignacion[6].ParameterName = "@IDSolicitudAsignacion";
            parametrosDeAsignacion[6].Value = asignacion.Solicitud.IDSolicitud; 
            parametrosDeAsignacion[7].ParameterName = "@IDLiberacionAsignacion";
            parametrosDeAsignacion[7].Value = asignacion.Liberacion.IDLiberacion;
            parametrosDeAsignacion[8].ParameterName = "@HorasCubiertas";
            parametrosDeAsignacion[8].Value = asignacion.HorasCubiertas;

            return parametrosDeAsignacion;
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}
