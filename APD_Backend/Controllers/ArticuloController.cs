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
            
            var Articulo = new Dictionary<string, int>();

            foreach (Articulos art in ListaArticulos)
            {
                var ListaArticuloXPedido = db.ArticuloXPedido.ToList();
                var cant = db.ArticuloXPedido.Where(c => c.idArticulo == art.id).ToList().Count();
                
                Articulo.Add(art.nombre, cant);
            }

            resultado.Return = Articulo;
            return resultado;
        }

        [HttpPost]
        [Route("Articulos/AltaArticulos")]
        public ActionResult<ResultadoAPI> AltaPersonaComando([FromBody]ComandoCrearArticulo comando, string token) {
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

            var art = new Articulos();
            art.id = comando.id;
            art.nombre = comando.nombre;
            art.stock = comando.stock;
            art.precio = comando.precio;
            art.idProveedor = comando.idProveedor;

            db.Articulos.Add(art);
            db.SaveChanges();
            resultado.Ok = true;
            resultado.Return = db.Articulos.ToList();
            return resultado;
        }

        [HttpPut]
        [Route("Pedidos/UpdatePedidos")]
        public ActionResult<ResultadoAPI> UpdatePedidos([FromBody]ComandoUpdateArticulo comando, string token) {
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
    }
}