using System.Numerics;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Data;
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
            var listaPedidos = db.Pedidos.ToList();
            foreach (Pedidos ped in listaPedidos)
            {
                ped.Clientes = db.Clientes.Where(c => c.id == ped.idCliente).FirstOrDefault();
                
            }
            resultado.Ok = true;
            resultado.Return = listaPedidos;
            return resultado;
        }

        [HttpGet]
        [Route("Pedidos/ObtenerPedidosPorCliente/{idCliente}")]
        public ActionResult<ResultadoAPI> GetClientes(String idCliente, string token){
            ResultadoAPI resultado = new ResultadoAPI();
            var listaPedidos = db.Pedidos.ToList();
            var listaFinal = new List<Pedidos>();
            var id = Int32.Parse(idCliente);


            foreach (Pedidos ped in listaPedidos)
            {
                if (ped.idCliente == id) {
                    listaFinal.Add(ped);
                    ped.Clientes = db.Clientes.Where(c => c.id == ped.idCliente).FirstOrDefault();
                }

                
            }
            resultado.Ok = true;
            resultado.Return = listaFinal;
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
            if (comando.fecha.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese Fecha";
                return resultado;
            }

            var ped = new Pedidos();
            ped.estado = comando.estado;
            ped.idCliente = comando.idCliente;
            ped.fecha = comando.fecha;
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
                ped.fecha = comando.fecha;
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

        [HttpGet]
        [Route("Pedidos/FiltrarPorFecha")]
        public ActionResult<ResultadoAPI> FiltrarPorFecha(DateTime fechaInicial, DateTime fechaFinal, string token) {
            ResultadoAPI resultado = new ResultadoAPI();
            var listaPedidos = db.Pedidos.ToList();
            var listaFinal = new List<Pedidos>();
            foreach (Pedidos ped in listaPedidos)
            {
                if (ped.fecha > fechaInicial && ped.fecha < fechaFinal) {
                    
                    ped.Clientes = db.Clientes.Where(c => c.id == ped.idCliente).FirstOrDefault();
                    listaFinal.Add(ped);
                }
            }
            resultado.Ok = true;
            resultado.Return = listaFinal;
            return resultado;
        }
    

    }
}