using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using digitalwaretest.Data;
using digitalwaretest.BL.Models;

namespace digitalwaretest.BL.Logica
{
    public class Cliente
    {
        public int clienteId { get; set; }
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
        public Respuesta<Cliente> crearCliente(string noIdentificacion, 
                                    string primerNombre, 
                                    string primerApellido, 
                                    string segundoApellido,
                                    string tipoIdentificacion,
                                    string direccionCliente,
                                    string correoCliente,
                                    string telefonofijoCliente,
                                    string telefonoCelCliente,
                                    DateTime fechanacimiento,
                                    string segundoNombre = null)
        {
            CLIENTE cLIENTE = new CLIENTE();
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Cliente> respuesta = new Respuesta<Cliente>();
            if (db.CLIENTE.Any(x=>x.numeroIdentificaionCliente==noIdentificacion))
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "El numero de identificacion Ingresado ya esta en uso";
                return respuesta;
            }
            try {
                cLIENTE.numeroIdentificaionCliente = noIdentificacion;
                cLIENTE.primerNombreCliente = primerNombre;
                cLIENTE.segundoNombreCliente = segundoNombre;
                cLIENTE.primerApellidoCliente = primerApellido;
                cLIENTE.segundoApellidoCliente = segundoApellido;
                cLIENTE.tipoIdentificacionCliente = tipoIdentificacion;
                cLIENTE.direccionCliente = direccionCliente;
                cLIENTE.correoCliente = correoCliente;
                cLIENTE.telefonofijoCliente = telefonofijoCliente;
                cLIENTE.telefonocelCliente = telefonoCelCliente;
                cLIENTE.fechaNacimientoCliente = fechanacimiento;
                db.CLIENTE.Add(cLIENTE);
                db.SaveChanges();
                respuesta.Estadopeticion = true;
                Cliente c = new Cliente();
                c.clienteId = cLIENTE.clienteId;
                return respuesta;
            }catch(Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error "+ e.Message;
                return respuesta;
            }
            
        }

        public Respuesta<Cliente>  actualizarCliente(int userid,
                                    string noIdentificacion,
                                    string primerNombre,
                                    string primerApellido,
                                    string segundoApellido,
                                    string tipoIdentificacion,
                                    string direccionCliente,
                                    string correoCliente,
                                    string telefonofijoCliente,
                                    string telefonoCelCliente,
                                    DateTime fechanacimiento,
                                    string segundoNombre = null)
        {
            CLIENTE cLIENTE = new CLIENTE();
            Respuesta<Cliente> respuesta = new Respuesta<Cliente>();
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            try
            {
                if (db.USUARIO.Find(userid) != null)
                {
                    respuesta.Estadopeticion = false;
                    respuesta.MensajeRespuesta = "Cliente No Existente";
                    return respuesta;
                }
                var User = db.USUARIO.Find(userid);
                cLIENTE = db.CLIENTE.Find(User.clienteId);
                cLIENTE.numeroIdentificaionCliente = noIdentificacion;
                cLIENTE.primerNombreCliente = primerNombre;
                cLIENTE.segundoNombreCliente = segundoNombre;
                cLIENTE.primerApellidoCliente = primerApellido;
                cLIENTE.segundoApellidoCliente = segundoApellido;
                cLIENTE.tipoIdentificacionCliente = tipoIdentificacion;
                cLIENTE.direccionCliente = direccionCliente;
                cLIENTE.correoCliente = correoCliente;
                cLIENTE.telefonofijoCliente = telefonofijoCliente;
                cLIENTE.telefonocelCliente = telefonoCelCliente;
                cLIENTE.fechaNacimientoCliente = fechanacimiento;
                db.SaveChanges();
                respuesta.Estadopeticion = true;
                respuesta.MensajeRespuesta = "Actualizacion Exitosa";
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error "+e.Message;
                return respuesta;
               
            }

        }

        public Respuesta<Cliente> consultarOldCliente(string identificacion)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Cliente> respuesta = new Respuesta<Cliente>();
            try
            {
                respuesta.Exist= db.CLIENTE.Any(x => x.numeroIdentificaionCliente == identificacion);
                respuesta.Estadopeticion = true;
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error " + e.Message;
                return respuesta;
            }
        }

        public Respuesta<Cliente> consultarCliente(int userid)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Cliente> respuesta = new Respuesta<Cliente>();
            respuesta.Registros = new List<Cliente>();
            try
            {
                
                respuesta.Estadopeticion = true;
                var cliente = db.USUARIO.Find(userid);
                Cliente cli = new Cliente();
                cli.primerNombreCliente = cliente.CLIENTE.primerNombreCliente;
                cli.segundoNombreCliente = cliente.CLIENTE.segundoNombreCliente;
                cli.primerApellidoCliente = cliente.CLIENTE.primerApellidoCliente;
                cli.numeroIdentificaionCliente = cliente.CLIENTE.numeroIdentificaionCliente;
                cli.tipoIdentificacionCliente = cliente.CLIENTE.tipoIdentificacionCliente;
                cli.correoCliente = cliente.CLIENTE.correoCliente;
                cli.direccionCliente = cliente.CLIENTE.direccionCliente;
                cli.telefonocelCliente = cliente.CLIENTE.telefonocelCliente;
                cli.telefonofijoCliente = cliente.CLIENTE.telefonofijoCliente;
                respuesta.Registros.Add(cli);
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error " + e.Message;
                return respuesta;
            }
        }


        /* public bool eliminarCliente(int clienteID)
         {
             DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
             if (db.CLIENTE.Find(clienteID)==null)
             {
                 return false;
             }
             try {
                 CLIENTE cLIENTE = db.CLIENTE.Find(clienteID);
                 db.CLIENTE.Remove(cLIENTE);
                 db.SaveChanges();
                 return true;

             }catch(Exception e)
             {
                 return false;
             }

         }*/

    }
}
