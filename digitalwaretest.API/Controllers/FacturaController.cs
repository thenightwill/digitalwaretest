using digitalwaretest.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace digitalwaretest.API.Controllers
{
    [RoutePrefix("Factura")]
    public class FacturaController : ApiController
    {

        [Route("updateFactura")]
        [HttpPut]
        public IHttpActionResult updateFactura([FromBody] Models.Factura request)
        {
            BL.Logica.Factura factura = new BL.Logica.Factura();
            if (!ModelState.IsValid)
                return BadRequest("Informacion Invalida");
            var Res = factura.actualizarFactura(request.facturaId,request.totalFactura,request.subtotalFactura, request.IVAFactura,request.fechaFactura,request.sedeId.Value, request.UserId.Value);
           

            if (Res.Estadopeticion)
            {
               
                return Ok(Res);
            }
            
            return Content(HttpStatusCode.BadRequest, Res);
        }

        [Route("createFactura")]
        [HttpPost]
        public IHttpActionResult createFactura([FromBody] Models.Factura request)
        {
            BL.Logica.Factura factura = new BL.Logica.Factura();
            if (!ModelState.IsValid)
                return BadRequest("Informacion Invalida");
            var Res = factura.crearFactura( request.totalFactura, request.subtotalFactura, request.IVAFactura, request.fechaFactura, request.sedeId.Value, request.UserId.Value,request.Lista_productos);
            
           
            if (Res.Estadopeticion)
            {
                
                return Ok(Res);
            }
           
            return Content(HttpStatusCode.BadRequest, Res);
        }

        [Route("deleteFactura")]
        [HttpDelete]
        public IHttpActionResult deleteFactura(int facturaId)
        {
            BL.Logica.Factura factura = new BL.Logica.Factura();
            var Res = factura.eliminarFactura(facturaId);
            

            if (Res.Estadopeticion)
            {
               
                return Ok(Res);
            }
            
            return Content(HttpStatusCode.BadRequest, Res);
        }

        [Route("getFacturasUser")]
        [HttpGet]
        public IHttpActionResult getfacturasxUser(int userId)
        {
            BL.Logica.Factura factura = new BL.Logica.Factura();
            var respuesta = factura.getFacturasxUser(userId);
            var jsonIgnoreNullValues = JsonConvert.SerializeObject(respuesta, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            JArray jObject = JArray.Parse(jsonIgnoreNullValues);
            
            if (respuesta.Estadopeticion)
            {
                return Ok(jObject);
            }
            return BadRequest(respuesta.ToString());
            

        }

        [Route("getFacturasxid")]
        [HttpGet]
        public IHttpActionResult getfacturaxid(int facId)
        {
            BL.Logica.Factura factura = new BL.Logica.Factura();
            var respuesta = factura.getFacturaxid(facId);
            var jsonIgnoreNullValues = JsonConvert.SerializeObject(respuesta, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            JArray jObject = JArray.Parse(jsonIgnoreNullValues);

            if (respuesta.Estadopeticion)
            {
                return Ok(jObject);
            }
            return BadRequest(respuesta.ToString());


        }
    }
}
