using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace digitalwaretest.API.Models
{
    public class Producto
    {
        public int productoId { get; set; }
        public string nombreProducto { get; set; }
        public string marcaProducto { get; set; }
        public string fechaExpedicionProducto { get; set; }
        public string fechaVencimientoProducto { get; set; }
        public string precioProducto { get; set; }
        public int cantidad { get; set; }
    }
}