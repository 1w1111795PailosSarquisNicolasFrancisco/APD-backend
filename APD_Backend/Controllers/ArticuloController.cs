using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Comandos;
using Resultados;
using Data;
using Comandos.Pedidos;
using Comandos.Articulos;

namespace APD_Backend.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class ArticuloController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly ILogger<ArticuloController> _logger;

        public ArticuloController(ILogger<ArticuloController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Articulos/ObtenerArticulos")]
        public ActionResult<ResultadoAPI> Get(string token){
            ResultadoAPI resultado = new ResultadoAPI();
            resultado.Ok = true;
            resultado.Return = db.Articulos.ToList();
            return resultado;
        }

        [HttpGet]
        [Route("Articulos/ReporteProductosMasVendidos")]
        public ActionResult<ResultadoAPI> ReporteProductosMasVendidos(string token){
            ResultadoAPI resultado = new ResultadoAPI();

            resultado.Ok = true;
            var ListaArticulos = db.Articulos.ToList();
            
            List<ArticulosCantidad> lista = new List<ArticulosCantidad>();

            foreach (Articulos art in ListaArticulos)
            {
                ArticulosCantidad arti = new ArticulosCantidad();

                var ListaArticuloXPedido = db.ArticuloXPedido.ToList();
                var cant = db.ArticuloXPedido.Where(c => c.idArticulo == art.id).ToList().Count();

                arti.nombre = art.nombre;
                arti.cantidad = cant;
                
                if(arti.cantidad > 0 ){
                    lista.Add(arti);
                }
            }

            void Swap<T>(IList<T> list, int indexA, int indexB)
            {
                T tmp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = tmp;
            }

            void IntArrayBubbleSort (List<ArticulosCantidad> data)
            {
                int i, j;
                int N = data.Count;

                for (j=N-1; j>0; j--) {
                    for (i=0; i<j; i++) {
                     if (data[i].cantidad < data[i + 1].cantidad)
                         Swap(data, i, i + 1);
                 }
                 }
            }

            IntArrayBubbleSort(lista);

            resultado.Return = lista.Take(10);
            return resultado;
        }

        [HttpGet]
        [Route("Clientes/reporteArticulos")]
        public ActionResult<ResultadoAPI> ObtenerReportes(DateTime lim_inf, DateTime lim_sup, string token){
            ResultadoAPI resultado = new ResultadoAPI();
            List<ArticulosCantidad> lista = new List<ArticulosCantidad>();
            var articulos = db.Articulos.ToList();
            
            foreach (Articulos art in articulos)
            {
                ArticulosCantidad artCant = new ArticulosCantidad();
                var cantidad = 0;
                var artxped = db.ArticuloXPedido.Where(c => c.idArticulo == art.id).ToList();
                foreach (ArticuloXPedido axp in artxped){
                var pedido = db.Pedidos.Where(c => c.id == axp.idPedido).FirstOrDefault();
                if ((lim_inf < pedido.fecha) && (pedido.fecha < lim_sup)){
                    cantidad++;
                 }   
                }
                artCant.nombre = art.nombre;
                artCant.cantidad = cantidad;
                
                if(artCant.cantidad > 0 ){
                    lista.Add(artCant);
                }
                
            }

            void Swap<T>(IList<T> list, int indexA, int indexB)
            {
                T tmp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = tmp;
            }

            void IntArrayBubbleSort (List<ArticulosCantidad> data)
            {
                int i, j;
                int N = data.Count;

                for (j=N-1; j>0; j--) {
                    for (i=0; i<j; i++) {
                     if (data[i].cantidad < data[i + 1].cantidad)
                         Swap(data, i, i + 1);
                 }
                 }
            }

            IntArrayBubbleSort(lista);

            resultado.Ok = true;
            resultado.Return = lista.Take(10);
            return resultado;
        }

        [HttpPost]
        [Route("Articulos/AltaArticulos")]
        public ActionResult<ResultadoAPI> AltaPersonaComando([FromBody]ComandoCrearArticulo comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.nombre.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre del articulo";
                return resultado;
            }
            if (comando.stock.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el stock";
                return resultado;
            }
            if (comando.precio.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el precio del articulo";
                return resultado;
            }
            if (comando.idProveedor.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el ID del proveedor";
                return resultado;
            }

            var art = new Articulos();
            art.nombre = comando.nombre;
            art.stock = comando.stock;
            art.precio = comando.precio;
            art.idProveedor = comando.idProveedor;
            art.proveedores = db.Proveedores.Where(c => c.id == comando.idProveedor).FirstOrDefault();

            db.Articulos.Add(art);
            db.SaveChanges();
            resultado.Ok = true;
            resultado.Return = db.Articulos.ToList();
            return resultado;
        }

        [HttpPost]
        [Route("Articulos/AltaArticulosXPedido")]
        public ActionResult<ResultadoAPI> AltaPersonaComando([FromBody]ComandoCrearArticuloXPedido comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.idArticulo.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese id del articulo";
                return resultado;
            }
            if (comando.idPedido.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese id del pedido";
                return resultado;
            }
            if (comando.cantidad.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese id del pedido";
                return resultado;
            }

            var artxped = new ArticuloXPedido();
            artxped.idArticulo = comando.idArticulo;
            artxped.idPedido = comando.idPedido;
            artxped.cantidad = comando.cantidad;
            artxped.articulos = db.Articulos.Where(c => c.id == comando.idArticulo).FirstOrDefault();
            artxped.pedidos = db.Pedidos.Where(c => c.id == comando.idPedido).FirstOrDefault();

            db.ArticuloXPedido.Add(artxped);
            db.SaveChanges();
            resultado.Ok = true;
            resultado.Return = db.ArticuloXPedido.ToList();

            return resultado;
        }

        [HttpGet]
        [Route("Articulos/ReporteProductosSinStock")]
        public ActionResult<ResultadoAPI> ReporteProductosSinStock(string token)
        {
            ResultadoAPI resultado = new ResultadoAPI();

            resultado.Ok = true;
            var ListaArticulos = db.Articulos.ToList();

            List<Articulos> lista = new List<Articulos>();

            foreach (Articulos art in ListaArticulos)
            {
                var stoock = 0;
                Articulos arti = new Articulos();

                if (art.stock == stoock)
                {
                    arti.id= art.id;
                    arti.nombre = art.nombre;
                    arti.stock = art.stock;
                    arti.precio= art.precio;
                    arti.idProveedor=art.idProveedor;
                    lista.Add(arti);
                }




            }

            resultado.Return = lista;
            return resultado;
        }

        [HttpPut]
        [Route("Articulos/UpdateArticulo")]
        public ActionResult<ResultadoAPI> UpdateArticulo([FromBody]ComandoUpdateArticulo comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.id.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del articulo";
                return resultado;
            }
            if (comando.nombre.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre del articulo";
                return resultado;
            }
            if (comando.stock.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el stock";
                return resultado;
            }
            if (comando.precio.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el precio del articulo";
                return resultado;
            }
            if (comando.idProveedor.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el ID del proveedor";
                return resultado;
            }

            var art = db.Articulos.Where(c => c.id == comando.id).FirstOrDefault();
            if (art != null)
            {
                art.id = comando.id;
                art.nombre = comando.nombre;
                art.stock = comando.stock;
                art.precio = comando.precio;
                art.idProveedor = comando.idProveedor;
                art.proveedores = db.Proveedores.Where(c => c.id == comando.idProveedor).FirstOrDefault();


                db.Articulos.Update(art);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Articulos.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("Pedidos/EliminarArticulo/{idArticulo}")]
        public ActionResult<ResultadoAPI> EliminarPedido(int idArticulo, string token) {
            var resultado = new ResultadoAPI();

            if (idArticulo.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del pedido";
                return resultado;
            }

            var art = db.Articulos.Where(c => c.id == idArticulo).FirstOrDefault();
            if (art != null) {
                db.Articulos.Remove(art);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Articulos.ToList();

            return resultado;

        }

        [HttpGet]
        [Route("Articulos/ObtenerArticulosXPedido/{idPedido}")]
        public ActionResult<ResultadoAPI> Get(int idPedido, string token){
            ResultadoAPI resultado = new ResultadoAPI();
            var ListaArticulosxped = db.ArticuloXPedido.ToList();
            
            List<ArticuloXPedido> lista = new List<ArticuloXPedido>();

            foreach (ArticuloXPedido art in ListaArticulosxped)
            {
               if(art.idPedido == idPedido){
                   var pedido = db.Pedidos.Where(c => c.id == art.idPedido).FirstOrDefault();
                   var articulo = db.Articulos.Where(c => c.id == art.idArticulo).FirstOrDefault();
                   art.pedidos = pedido;
                   art.articulos = articulo;
                    lista.Add(art);
               }                
                
            }
            resultado.Ok = true;
            resultado.Return = lista;
            return resultado;
        }

         [HttpGet]
        [Route("Articulos/ObtenerProveedores")]
        public ActionResult<ResultadoAPI> GetProveedores(string token){
            ResultadoAPI resultado = new ResultadoAPI();
            resultado.Ok = true;
            resultado.Return = db.Proveedores.ToList();
            return resultado;
        }
    }
}