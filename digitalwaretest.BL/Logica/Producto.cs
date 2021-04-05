using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using digitalwaretest.Data;
using digitalwaretest.BL.Models;

namespace digitalwaretest.BL.Logica
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
        public string sedeNombre { get; set; }
        public string sedeDireccion { get; set; }
        public Respuesta<Producto> getallproductos()
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Producto> respuesta = new Respuesta<Producto>();
            respuesta.Registros = new List<Producto>();
            try
            {
                var lista = from producto in db.PRODUCTO
                            where producto.INVENTARIO.FirstOrDefault().cantidad > 0
                            select new Producto {
                                nombreProducto = producto.nombreProducto,
                                fechaExpedicionProducto = producto.fechaExpedicionProducto,
                                fechaVencimientoProducto = producto.fechaVencimientoProducto,
                                marcaProducto = producto.marcaProducto,
                                precioProducto = producto.precioProducto,
                                productoId = producto.productoId,
                                cantidad = producto.INVENTARIO.FirstOrDefault().cantidad,
                                sedeNombre= producto.INVENTARIO.FirstOrDefault().SEDE.FirstOrDefault().nombreSede,
                                sedeDireccion= producto.INVENTARIO.FirstOrDefault().SEDE.FirstOrDefault().direccionSede
                            };
                respuesta.Estadopeticion = true;
                respuesta.Registros = lista.ToList();
                return respuesta;
            }
            catch(Exception e)
            {
                respuesta.Estadopeticion = false;
                respuesta.MensajeRespuesta = "Error " + e.Message;
                return respuesta;
            }
        }

        public Respuesta<Producto> getproductosbysede(int sedeId)
        {
            DIGITALWARETESTEntities db = new DIGITALWARETESTEntities();
            Respuesta<Producto> respuesta = new Respuesta<Producto>();
            respuesta.Registros = new List<Producto>();
            try
            {
                var lista = from producto in db.PRODUCTO
                            
                            where producto.INVENTARIO.FirstOrDefault().cantidad > 0 && producto.INVENTARIO.FirstOrDefault().SEDE.FirstOrDefault().sedeId == sedeId
                            select new Producto
                            {
                                nombreProducto = producto.nombreProducto,
                                fechaExpedicionProducto = producto.fechaExpedicionProducto,
                                fechaVencimientoProducto = producto.fechaVencimientoProducto,
                                marcaProducto = producto.marcaProducto,
                                precioProducto = producto.precioProducto,
                                productoId = producto.productoId,
                                cantidad = producto.INVENTARIO.FirstOrDefault().cantidad,
                                sedeNombre = producto.INVENTARIO.FirstOrDefault().SEDE.FirstOrDefault().nombreSede,
                                sedeDireccion = producto.INVENTARIO.FirstOrDefault().SEDE.FirstOrDefault().direccionSede

                            };
                respuesta.Estadopeticion = true;
                respuesta.Registros = lista.ToList();
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
