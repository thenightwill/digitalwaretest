using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using digitalwaretest.API.Models;

namespace digitalwaretest.API.Controllers
{
    [RoutePrefix("Cliente")]
    public class ClienteController : ApiController
    {
        [Route("updateData")]
        [HttpPut]
        public IHttpActionResult updateClientData([FromBody] Models.Cliente request)
        {
            BL.Logica.Cliente cliente = new BL.Logica.Cliente();
            if (!ModelState.IsValid)
                return BadRequest("Informacion Invalida");
            var Res = cliente.actualizarCliente(request.userId,
                                                request.numeroIdentificaionCliente,
                                                request.primerNombreCliente,
                                                request.primerApellidoCliente,
                                                request.segundoApellidoCliente,
                                                request.tipoIdentificacionCliente,
                                                request.direccionCliente,
                                                request.correoCliente,
                                                request.telefonofijoCliente,
                                                request.telefonocelCliente,
                                                request.fechaNacimientoCliente,
                                                request.segundoNombreCliente);
            
            if (Res.Estadopeticion)
            {
                return Ok(Res);
            }
            
            return Content(HttpStatusCode.BadRequest, Res);
        }

        [Route("getclient")]
        [HttpGet]
        public IHttpActionResult getClientData(int userId)
        {
            BL.Logica.Cliente cliente = new BL.Logica.Cliente();
            if (!ModelState.IsValid)
                return BadRequest("Informacion Invalida");

            var Res = cliente.consultarCliente(userId);
            if (Res.Estadopeticion)
            {
                return Ok(Res);
            }

            return Content(HttpStatusCode.BadRequest, Res);
        }


        //private DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();

        //// GET: api/Usuarios
        //public IQueryable<Cliente> GetClientes()
        //{
        //    return db.USUARIO;
        //}

        //// GET: api/Usuarios/5
        //[ResponseType(typeof(Cliente))]
        //public IHttpActionResult GetCliente(int id)
        //{
        //    Usuario uSUARIO = db.USUARIO.Find(id);
        //    if (uSUARIO == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(uSUARIO);
        //}

        //// PUT: api/Usuarios/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUSUARIO(int id, Cliente uSUARIO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != uSUARIO.userId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(uSUARIO).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!USUARIOExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Usuarios
        //[ResponseType(typeof(Usuario))]
        //public IHttpActionResult PostUSUARIO(Cliente uSUARIO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.USUARIO.Add(uSUARIO);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = uSUARIO.userId }, uSUARIO);
        //}

        //// DELETE: api/Usuarios/5
        //[ResponseType(typeof(Usuario))]
        //public IHttpActionResult DeleteUSUARIO(int id)
        //{
        //    Usuario uSUARIO = db.USUARIO.Find(id);
        //    if (uSUARIO == null)
        //    {
        //        return NotFound();
        //    }

        //    db.USUARIO.Remove(uSUARIO);
        //    db.SaveChanges();

        //    return Ok(uSUARIO);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool USUARIOExists(int id)
        //{
        //    return db.USUARIO.Count(e => e.userId == id) > 0;
        //}
    }
}
