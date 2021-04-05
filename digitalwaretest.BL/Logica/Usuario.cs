using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using digitalwaretest.Data;
using digitalwaretest.BL.Models;



namespace digitalwaretest.BL.Logica
{
    public class Usuario 
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int clienteId { get; set; }
        public Cliente CLIENTE { get; set; }
        public Respuesta<Usuario> crearUser(string Username,
                                    string password,
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
            Cliente cli = new Cliente();
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Usuario> respuesta = new Respuesta<Usuario>();
            var cLIENTE = cli.crearCliente(noIdentificacion, primerNombre, primerApellido, segundoApellido, tipoIdentificacion, direccionCliente, correoCliente, telefonofijoCliente, telefonoCelCliente, fechanacimiento, segundoNombre);
            if (!cLIENTE.Estadopeticion)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = cLIENTE.MensajeRespuesta;
                return respuesta;
            }
            USUARIO uSUARIO = new USUARIO();
           
            if (db.USUARIO.Any(x => x.username == Username))
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Username En Uso, cambieloe intentelo nuevamente";
                return respuesta;
            }
            try
            {
                uSUARIO.clienteId = cLIENTE.Registros[0].clienteId;
                uSUARIO.username = Username;
                uSUARIO.password = password;
                db.USUARIO.Add(uSUARIO);
                db.SaveChanges();
                respuesta.Estadopeticion = true;
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error "+ e.Message;
                return respuesta;
            }

        }

        public Respuesta<Usuario> actualizarpassword(int userId, string oldPass, string newPass)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Usuario> respuesta = new Respuesta<Usuario>();
            USUARIO uSUARIO = db.USUARIO.Find(userId);

            if (!checkPass(uSUARIO.password, oldPass))
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "La contraeña actual no es correcta, verifique e intente de nuevo";
                return respuesta;
            }
            try
            {
                uSUARIO.password = newPass;
                db.SaveChanges();
                respuesta.Estadopeticion = true;
                
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error "+e.Message;
                return respuesta;
            }

        }

        public Respuesta<Usuario> eliminarUser(int UserId)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Usuario> respuesta = new Respuesta<Usuario>();
            if (db.USUARIO.Find(UserId) == null)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "El Usuario actual no existe";
                return respuesta;
            }
            try
            {
                USUARIO uSUARIO = db.USUARIO.Find(UserId);
                db.USUARIO.Remove(uSUARIO);
                db.SaveChanges();
                respuesta.Estadopeticion = true;
                
                return respuesta;
               

            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error "+e.Message;
                return respuesta;
            }

        }

        public  Respuesta<Usuario> Login(string Username, string Password)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Usuario> respuesta = new Respuesta<Usuario>();
            respuesta.Registros = new List<Usuario>();
            try
            {
                if (db.USUARIO.Any(x => x.username == Username))
                {
                    USUARIO User = db.USUARIO.Where(x => x.username == Username).FirstOrDefault();

                    if (checkPass(User.password, Password))
                    {
                        Usuario Usu = new Usuario();
                        Usu.username = User.username;
                        Usu.userId = User.userId;
                        respuesta.Registros.Add(Usu);
                        respuesta.Estadopeticion = true;
                        return respuesta;
                    }
                    respuesta.Estadopeticion = false;
                    respuesta.MensajeRespuesta = "La contraeña actual no es correcta, verifique e intente de nuevo";
                    return respuesta;

                }
                else
                {
                    respuesta.Estadopeticion = false;
                    respuesta.MensajeRespuesta = "Usuario o Contraseña incorrectos, verifique e intente de nuevo";
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error "+e.Message;
                return respuesta;
            }
        }

        public Respuesta<Usuario> consultarUserExistente(string username)
        {
            Respuesta<Usuario> res = new Respuesta<Usuario>();
            try
            {
                DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
                bool y = db.USUARIO.Any(x => x.username == username);
                res.Exist = y;
                res.Estadopeticion = true;
                return res;
            }
            catch (Exception e)
            {
                res.Estadopeticion = false;
                res.MensajeRespuesta = "Error "+e.Message;
                return res;
            }
        }

        public static bool checkPass(string Pass1, string Pass2)
        {
            string p0 = Encoding.UTF8.GetString(Convert.FromBase64String(Pass1));
            string p1 = Encoding.UTF8.GetString(Convert.FromBase64String(Pass2));
            if (p0.Equals(p1))
            {
                return true;
            }
            return false;
        }
    }
}
