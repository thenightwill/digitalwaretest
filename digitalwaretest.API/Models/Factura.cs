using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using digitalwaretest.BL;

namespace digitalwaretest.API.Models
{
    public class Factura
    {
        public int facturaId { get; set; }
        public double totalFactura { get; set; }
        public double subtotalFactura { get; set; }
        public double IVAFactura { get; set; }
        public System.DateTime fechaFactura { get; set; }
        public Nullable<int> sedeId { get; set; }
        public Nullable<int> UserId { get; set; }
        public List<BL.Logica.Producto> Lista_productos { get; set; }
    }
}