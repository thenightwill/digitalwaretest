using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace digitalwaretest.API.Controllers
{
    [RoutePrefix("Productos")]
    public class ProductosController : ApiController
    {
        [Route("getProductos")]
        [HttpGet]
        public IHttpActionResult getAllProductos()
        {
            BL.Logica.Producto producto = new BL.Logica.Producto();
            var Res = producto.getallproductos();
            if (Res.Estadopeticion)
            {
                return Ok(Res);
            }
            return Content(HttpStatusCode.BadRequest, Res);
        }

        [Route("getProductosSede")]
        [HttpGet]
        public IHttpActionResult getAllProductosxSede(int sedeId)
        {
            BL.Logica.Producto producto = new BL.Logica.Producto();
            var Res = producto.getproductosbysede(sedeId);
            var jsonIgnoreNullValues = JsonConvert.SerializeObject(Res, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            JObject jObject = JObject.Parse(jsonIgnoreNullValues);
            if (Res.Estadopeticion)
            {
               
                return Ok(jObject);
            }
            return Content(HttpStatusCode.BadRequest, Res);
        }
    }
}
