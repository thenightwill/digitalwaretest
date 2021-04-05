using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using digitalwaretest.Data;
using digitalwaretest.BL.Models;

namespace digitalwaretest.BL.Logica
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
        public List<Producto> productos { get; set; }
        public Respuesta<Factura> crearFactura(double totalFactura,
                                    double subtotalFactura,
                                    double Ivafactura,
                                    DateTime fechaFactura,
                                    int sedeID,
                                    int UserID,
                                    List<Producto> ListaProductos)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            FACTURA fACTURA = new FACTURA();
            Respuesta<Factura> respuesta = new Respuesta<Factura>();
            try
            {
                fACTURA.totalFactura = totalFactura;
                fACTURA.subtotalFactura = subtotalFactura;
                fACTURA.IVAFactura = Ivafactura;
                fACTURA.fechaFactura = fechaFactura;
                fACTURA.sedeId = sedeID;
                fACTURA.UserId = UserID;
                
                for (int i=0;i<ListaProductos.Count;i++)
                {
                    PRODUCTO pro = db.PRODUCTO.Find(ListaProductos[i].productoId);
                    var x = db.SEDE.Find(sedeID);
                    var a=pro.INVENTARIO.Where(y => y.inventarioId == x.inventarioId).FirstOrDefault();
                    a.cantidad--;
                    db.Entry(a).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.FACTURA.Add(fACTURA);
                db.SaveChanges();
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

        public Respuesta<Factura> actualizarFactura(int facturaID,
                                    double totalFactura,
                                    double subtotalFactura,
                                    double Ivafactura,
                                    DateTime fechaFactura,
                                    int sedeID,
                                    int UserID
                                    )
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            FACTURA fACTURA = db.FACTURA.Find(facturaID);
            Respuesta<Factura> respuesta = new Respuesta<Factura>();
            try
            {
                fACTURA.totalFactura = totalFactura;
                fACTURA.subtotalFactura = subtotalFactura;
                fACTURA.IVAFactura = Ivafactura;
                fACTURA.fechaFactura = fechaFactura;
                fACTURA.sedeId = sedeID;
                fACTURA.UserId = UserID;
                db.SaveChanges();
                respuesta.Estadopeticion = true;
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = e.Message;
                return respuesta;
            }
        }

        public Respuesta<Factura> eliminarFactura(int facturaId)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            var facturaactual = db.FACTURA.Find(facturaId);
            Respuesta<Factura> respuesta = new Respuesta<Factura>();
            if (facturaactual==null)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Factura no existente";
                return respuesta;
            }
            try
            {
                var temp = facturaactual.FACTURA_PRODUCTO.Where(x => x.facturaId == facturaId);
                db.FACTURA_PRODUCTO.RemoveRange(temp);
                db.SaveChanges();

                //foreach (var facturaxproducto in facturaactual.FACTURA_PRODUCTO.Where(x=>x.facturaId==facturaId).ToList())
                //{

                //    facturaactual.FACTURA_PRODUCTO.Remove(facturaxproducto);
                //    db.SaveChanges();
                //}
                db.FACTURA.Remove(facturaactual);
                db.SaveChanges();
                respuesta.Estadopeticion = true;
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = e.Message;
                return respuesta;
            }
        }

        public Respuesta<Factura> getFacturasxUser(int userId)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Factura> respuesta = new Respuesta<Factura>();
            respuesta.Registros = new List<Factura>();
            try {
                var facturas = from facs in db.FACTURA
                               where facs.UserId == userId
                               select new Factura
                               {
                                   facturaId = facs.facturaId,
                                   fechaFactura = facs.fechaFactura,
                                   totalFactura = facs.totalFactura,
                                   subtotalFactura = facs.subtotalFactura,
                                   IVAFactura = facs.IVAFactura

                               };
                respuesta.Estadopeticion = true;
                respuesta.Registros = facturas.ToList();
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error " + e.Message;
                return respuesta;

            }

        }

        public Respuesta<Factura> getFacturaxid(int facId)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Factura> respuesta = new Respuesta<Factura>();
            respuesta.Registros = new List<Factura>();
            try
            {
                var facturas = from facs in db.FACTURA
                               where facs.facturaId == facId
                               select new Factura
                               {
                                   facturaId = facs.facturaId,
                                   fechaFactura = facs.fechaFactura,
                                   totalFactura = facs.totalFactura,
                                   subtotalFactura = facs.subtotalFactura,
                                   IVAFactura = facs.IVAFactura,
                                   productos = (from pro in db.PRODUCTO
                                                where pro.FACTURA_PRODUCTO.FirstOrDefault().facturaId == facId
                                                select new Producto
                                                {
                                                    productoId=pro.productoId,
                                                    nombreProducto=pro.nombreProducto,
                                                    cantidad=pro.FACTURA_PRODUCTO.FirstOrDefault().cantidadFactura,
                                                    marcaProducto=pro.marcaProducto,
                                                    precioProducto=pro.precioProducto
                                                }).ToList()


                                               };
                respuesta.Estadopeticion = true;
                respuesta.Registros = facturas.ToList();
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error " + e.Message;
                return respuesta;

            }

        }



    }
}
