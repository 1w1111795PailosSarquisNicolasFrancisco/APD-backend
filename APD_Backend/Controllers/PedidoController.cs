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

namespace APD_Backend.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class PedidoController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Pedidos/ObtenerPedidos")]
        public ActionResult<ResultadoAPI> Get(string token){
            ResultadoAPI resultado = new ResultadoAPI();
            resultado.Ok = true;
            resultado.Return = db.Pedidos.ToList();
            return resultado;
        }

        [HttpPost]
        [Route("Pedidos/AltaPedidos")]
        public ActionResult<ResultadoAPI> AltaPersonaComando([FromBody]ComandoCrearPedido comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.idCliente.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del cliente";
                return resultado;
            }
            if (comando.estado.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese Estado";
                return resultado;
            }

            var ped = new Pedidos();
            ped.estado = comando.estado;
            ped.idCliente = comando.idCliente;
            ped.Clientes = db.Clientes.Where(c => c.id == comando.idCliente).FirstOrDefault();

            db.Pedidos.Add(ped);
            db.SaveChanges();
            resultado.Ok = true;
            resultado.Return = db.Pedidos.ToList();
            return resultado;
        }

        [HttpPut]
        [Route("Pedidos/UpdatePedidos")]
        public ActionResult<ResultadoAPI> UpdatePedidos([FromBody]ComandoUpdatePedido comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.id.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del pedido";
                return resultado;
            }
            if (comando.idCliente.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del cliente";
                return resultado;
            }
            if (comando.estado.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese Estado";
                return resultado;
            }

            var ped = db.Pedidos.Where(c => c.id == comando.id).FirstOrDefault();
            if (ped != null)
            {
                ped.estado = comando.estado;
                ped.idCliente = comando.idCliente;
                db.Pedidos.Update(ped);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Pedidos.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("Pedidos/EliminarPedido/{id}")]
        public ActionResult<ResultadoAPI> EliminarPedido(String id, string token) {
            var resultado = new ResultadoAPI();

            if (id.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del pedido";
                return resultado;
            }

            var detxped = db.ArticuloXPedido.Where(c => c.idPedido == Int32.Parse(id));
            var ped = db.Pedidos.Where(c => c.id == Int32.Parse(id)).FirstOrDefault();
            if (ped != null) {
                foreach (var detalle in detxped)
                {
                    db.ArticuloXPedido.Remove(detalle);
                }
                db.Pedidos.Remove(ped);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Pedidos.ToList();

            return resultado;

        }
    

    }
}