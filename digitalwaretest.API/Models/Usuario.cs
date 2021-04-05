using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace digitalwaretest.API.Models
{
    public partial class Usuario
    {
        public int userId { get; set; }
        
        public string username { get; set; }
        public string password { get; set; }
        public int clienteId { get; set; }
        public Cliente CLIENTE { get; set; }
    }
}