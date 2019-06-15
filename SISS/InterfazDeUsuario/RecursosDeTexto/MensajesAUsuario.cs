﻿using System;
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
		public const string CORREOELECTRONICO_DUPLICADO_MENSAJE = "Hubo un problema al completar su registro. El correo electrónico ingresado ya esta registrado en el sistema.";
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
		public const string	CONTRASEÑA_INVALIDA = "Debe tener mínimo 6 caracteres o máximo 255.";
		public const string NOMBRE_INVALIDO = "Solo puede ser letras y signos de acentuación.";
		public const string CORREOELECTRONICO_INVALIDO = "No es un correo electrónico valido.";
		public const string MATRICULA_INVALIDA = "Debe tener el formato s12345678.";
		public const string VALOR_ENTERO_INVALIDO = "Deber ser un valor numérico entre 1 y 255.";
		public const string TELEFONO_INVALIDO = "Deben ser 10 caracteres numéricos.";
		public const string CONFIRMACION_INVALIDA = "Los valores no coinciden.";
		public const string REGISTRO_EXITOSO_COORDINADOR = "El coordinador ha sido registrado exitosamente.";
		//public const string;
		//public const string;
		//public const string;

		//public const string;
	}
}