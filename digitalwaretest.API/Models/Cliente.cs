using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace digitalwaretest.API.Models
{
    public class Cliente
    {
        public int userId { get; set; }
        public string numeroIdentificaionCliente { get; set; }
        public string primerNombreCliente { get; set; }
        public string primerApellidoCliente { get; set; }
        public string segundoNombreCliente { get; set; }
        public string segundoApellidoCliente { get; set; }
        public string tipoIdentificacionCliente { get; set; }
        public string direccionCliente { get; set; }
        public string correoCliente { get; set; }
        public string telefonofijoCliente { get; set; }
        public string telefonocelCliente { get; set; }
        public System.DateTime fechaNacimientoCliente { get; set; }
    }
}