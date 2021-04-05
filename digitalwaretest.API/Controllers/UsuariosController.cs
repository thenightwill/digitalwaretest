using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using digitalwaretest.BL.Logica;

using digitalwaretest.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace digitalwaretest.API.Controllers
{
    [RoutePrefix("Usuarios")]
    public class UsuariosController : ApiController
    {
       
        
        [Route("Login")]
        [HttpPost]
        public IHttpActionResult LoginUser([FromBody] Models.Usuario User )
        {
            BL.Logica.Usuario usuario = new BL.Logica.Usuario();
            var login = usuario.Login(User.username, User.password);

            if (!login.Estadopeticion)
            {
                return Content(HttpStatusCode.BadRequest, login);
            }
            var jsonIgnoreNullValues = JsonConvert.SerializeObject(login, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            JObject jObject = JObject.Parse(jsonIgnoreNullValues);
            return Ok(jObject);
        }

        [Route("updatePassword")]
        [HttpPut]
        public IHttpActionResult updatePass([FromBody] Models.updatePassRequest request)
        {
            BL.Logica.Usuario usuario = new BL.Logica.Usuario();
            if (!ModelState.IsValid)
                return BadRequest("Informacion Invalida");
            var Res=usuario.actualizarpassword(request.userId, request.password, request.newPass);
            if (Res.Estadopeticion)
            {
               
                return Ok(Res);
            }
            return Content(HttpStatusCode.BadRequest, Res);
        }

        [Route("createUser")]
        [HttpPost]
        public IHttpActionResult createUser([FromBody] Models.Usuario request)
        {
            BL.Logica.Usuario usuario = new BL.Logica.Usuario();
            if (!ModelState.IsValid)
                return BadRequest("Informacion Invalida");
            var Res = usuario.crearUser(request.username,
                                        request.password,
                                                request.CLIENTE.numeroIdentificaionCliente,
                                                request.CLIENTE.primerNombreCliente,
                                                request.CLIENTE.primerApellidoCliente,
                                                request.CLIENTE.segundoApellidoCliente,
                                                request.CLIENTE.tipoIdentificacionCliente,
                                                request.CLIENTE.direccionCliente,
                                                request.CLIENTE.correoCliente,
                                                request.CLIENTE.telefonofijoCliente,
                                                request.CLIENTE.telefonocelCliente,
                                                request.CLIENTE.fechaNacimientoCliente,
                                                request.CLIENTE.segundoNombreCliente);
           
            if (Res.Estadopeticion)
            {
                return Ok(Res);
            }
            return Content(HttpStatusCode.BadRequest, Res);
        }

        [Route("checkUser")]
        [HttpGet]
        public IHttpActionResult checkUser(string username)
        {
            BL.Logica.Usuario usuario = new BL.Logica.Usuario();
            
                var res=usuario.consultarUserExistente(username);
                if (res.Estadopeticion)
                {
                    return Ok(res);
                }
            return BadRequest(res.MensajeRespuesta);

        }


        //// PUT: api/Usuarios/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUSUARIO(int id, Models.Usuario uSUARIO)
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
        //public IHttpActionResult PostUSUARIO(Usuario uSUARIO)
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