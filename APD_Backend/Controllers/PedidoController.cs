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
// AGREGAR VALIDACIONES
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
            var ped = new Pedidos();
            ped.id = comando.id;
            ped.estado = comando.estado;
            ped.idCliente = comando.idCliente;

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
        [Route("Pedidos/EliminarPedido/{idPedido}")]
        public ActionResult<ResultadoAPI> EliminarPedido(int idPedido, string token) {
            var resultado = new ResultadoAPI();

            var ped = db.Pedidos.Where(c => c.id == idPedido).FirstOrDefault();
            if (ped != null) {
                db.Pedidos.Remove(ped);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Pedidos.ToList();

            return resultado;

    }
    

}
}