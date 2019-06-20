using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazDeUsuario.RecursosDeTexto
{
    public static class MensajesAUsuario
    {
		public const string COMPROBAR_CAMPOS_MENSAJE = "Porfavor compruebe los campos remarcados en rojo.";
		public const string COMPROBAR_CAMPOS_TITULO = "Campos invalidos";
		public const string MATRICULA_DUPLICADA_MENSAJE = "Hubo un error al completar el registro. La matricula ingresada ya existe en el sistema.";
		public const string MATRICULA_DUPLICADA_TITULO = "Matricula duplicada";
		public const string CORREOELECTRONICO_DUPLICADO_MENSAJE = "El correo electrónico ya esta registrado en el sistema.";
		public const string CORREOELECTRONICO_DUPLICADO_TITULO = "Correo electrónico duplicado";
		public const string REGISTRO_EXITOSO_TITULO = "¡Registro Exitoso!";
		public const string REGISTRO_EXITOSO_MENSAJE = "Ha sido registrado exitosamente.";
		public const string ERROR_DESCONOCIDO_TITULO = "Error desconocido";
		public const string ERROR_DESCONOCIDO_MENSAJE = "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.";
		public const string ERROR_INTERNO_TITULO = "Error interno";
		public const string CONEXION_FALLIDA_MENSAJE = "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.";
		public const string CONEXION_FALLIDA_TITULO = "Conexion fallida";
		public const string	ERROR_PETICION_MENSAJE = "Hubo un error al completar la petición. Recarge la página e intentelo nuevamente, si el problema persiste, contacte a su administrador.";
		public const string ERROR_GUARDAR_REGISTRO = "Hubo un error al guardar su registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.";
		public const string	CONTRASEÑA_INVALIDA = "Debe tener entre 6 y 255 caracteres.";
		public const string NOMBRE_INVALIDO = "Solo puede ser letras y signos de acentuación.";
		public const string CORREOELECTRONICO_INVALIDO = "No es un correo electrónico valido.";
		public const string MATRICULA_INVALIDA = "Debe tener el formato s12345678.";
		public const string VALOR_ENTERO_INVALIDO = "Deber ser un valor entero entre 1 y 255.";
		public const string TELEFONO_INVALIDO = "Deben ser 10 caracteres numéricos.";
		public const string CONFIRMACION_INVALIDA = "Los valores no coinciden.";
		public const string REGISTRO_EXITOSO_COORDINADOR = "El coordinador ha sido registrado exitosamente.";
		public const string CADENA_INVALIDA = "Debe tener entre 1 y 255 caracteres.";
		public const string ERROR_OBJETO_NO_EXISTE_MENSAJE = "El objeto especificado no se encontro en la base de datos.";
		public const string ERROR_OBJETO_NO_EXISTE_TITULO = "Objeto no encontrado";
		public const string COMBO_BOX_INVALIDO = "Porfavor, elija un elemento.";
		public const string REGISTRO_EXITOSO_ENCARGADO = "El encargado ha sido registrado exitosamente.";
		public const string ERROR_AL_CONVERTIR_OBJETO = "Error al cargar datos del servidor.";
		public const string REGISTRO_EXITOSO_ORGANIZACION = "La organización ha sido registrada exitosamente.";
		public const string TIPO_DE_SESION_INVALIDO_MENSAJE = "Tipo de sesion no valida. Contacte a su administrador.";
		public const string CREDENCIALES_INVALIDAS_MENSAJE = "Correo o contraseña no validos.";
		public const string CREDENCIALES_INVALIDAS_TITULO = "Credenciales no validas";
		public const string CAMPOS_LOGIN_VACIOS_MENSAJE = "Debe ingresar su contraseña y su correo para continuar.";
		public const string ASIGNACION_EXITOSA_MENSAJE = "El alumno fue asignado con exito.";
		public const string ASIGNACION_EXITOSA_TITULO = "¡Asignación exitosa!";
		public const string SELECCION_DE_PROYECTOS_EXITOSA_MENSAJE = "Su seleccion de proyectos ha sido guardada con exito.";
		public const string SELECCION_DE_PROYECTOS_EXITOSA_TITULO = "¡Proyectos seleccionados exitosamente!";
		public const string CANTIDAD_INVALIDA_DE_PROYECTOS_SELECCIONANDOS_MENSAJE = "Debe escoger 3 proyectos.";
		public const string CANTIDAD_INVALIDA_DE_PROYECTOS_SELECCIONANDOS_TITULO = "Cantidad de proyectos seleccionados invalida";
		public const string ACTUALIZACION_DE_REPORTE_MENSUAL_EXITOSA_MENSAJE = "El reporte mensual fue actualizado exitosamente.";
		public const string ARCHIVO_NO_SLECCIONADO_MENSAJE = "Debe seleccionar un archivo para continuar.";
		public const string ARCHIVO_NO_SLECCIONADO_TITULO = "Archivo no seleccionado";
		public const string ADVERTENCIA_TITULO = "Advertencia";
		public const string CONFIRMACION_BAJA_DE_PROYECTO_MENSAJE = "¿Esta seguro que desea dar de baja el proyecto seleccionado? Este cambio no puede deshacerse.";
		public const string BAJA_DE_PROYECTO_EXITOSA_MENSAJE = "El proyecto fue dado de baja exitosamente.";
		public const string BAJA_DE_ALUMNO_EXITOSA_MENSAJE = "El alumno fue dado de baja exitosamente.";
		public const string OPERACION_EXITOSA_TITULO = "Operacion exitosa.";
		public const string REGISTRO_EXITOSO_PROYECTO = "El proyecto ha sido registrado exitosamente";
		public const string REGISTRO_EXITOSO_TECNICO_ACADEMICO = "El técnico académico ha sido registrado exitosamente.";
		public const string ACEPTACION_EXITOSA_TITULO = "Aceptación exitosa";
		public const string ACEPTACION_EXITOSA_MENSAJE = "Los alumnos fueron aceptados exitosamente.";
		public const string ADVERTENCIA_ACEPTACION_MENSAJE = "Los alumnos seleccionados seran aceptados, mientras que los demas seran rechazados. ¿Seguro que desea continuar?";
		public const string ADVERTENCIA_BAJA_ALUMNO_MENSAJE = "¿Esta seguro que desea dar de baja al alumno seleccionado? Este cambio no puede deshacerse.";
		public const string REPORTE_NO_SELECCIONADO_MENSAJE = "Debe seleccionar un reporte mensual para actualizar.";
		public const string REPORTE_NO_SELECCIONADO_TITULO = "Ningun reporte seleccionado";
		public const string REGISTRO_EXITOSO_DOCUMENTO = "El documento fue registrado exitosamente.";
		public const string DOCUMENTO_YA_ENTREGAOD_TITULO = "Documento ya entregado";
		public const string DOCUMENTO_YA_ENTREGADO_MENSAJE = "El documento ya fue entregado.";
		public const string REGISTRO_EXITOSO_REPORTE_MENSUAL = "El reporte mensual fue registrado exitosamente.";
		public const string MES_DUPLICADO_TITULO = "Mes duplicado";
		public const string MES_DUPLICADO_MENSAJE = "Un reporte mensual con el mes seleccionado ya fue entregado.";
		public const string NUMERO_DE_HORAS_INVALIDO_TITULO = "Número de horas invalido";
		public const string NUMERO_DE_HORAS_INVALIDO_MENSAJE = "El número de horas reportadas debe ser un valor entero mayor a 0 y menor a 255.";
		public const string MAXIMO_DE_REPORTES_MENSUALES_ENTREGADO = "El numero maximo de reportes ha sido entregado.";
		public const string PROYECTO_NO_TIENE_CUPO_MENSAJE = "El proyecto al que intento asignar el alumno no tiene cupo.";
		public const string PROYECTO_NO_TIENE_CUPO_TITULO = "Cupo lleno";
	}
}
