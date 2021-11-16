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
using Comandos.Clientes;

namespace APD_Backend.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class ClienteController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Cliente/ObtenerClientes")]
        public ActionResult<ResultadoAPI> Get(string token){
            ResultadoAPI resultado = new ResultadoAPI();
            resultado.Ok = true;
            resultado.Return = db.Clientes.ToList();
            return resultado;
        }

        [HttpGet]
        [Route("Cliente/ObtenerClientesRepo")]
        public ActionResult<ResultadoAPI> GetRepo(string token){
            ResultadoAPI resultado = new ResultadoAPI();
            List<ClienteCantidad> lista = new List<ClienteCantidad>();
            var clientes = db.Clientes.ToList();
            foreach (Clientes cli in clientes)
            {
                ClienteCantidad cliCant = new ClienteCantidad();
                var cantidad = 0;
                var pedidos_del_cliente = db.Pedidos.Where(c => c.idCliente == cli.id).ToList();
                foreach (Pedidos ped in pedidos_del_cliente)
                {
                    cantidad++;
                    
                }
                cliCant.nombre = cli.nombre;
                cliCant.cantidad = cantidad;
                if(cliCant.cantidad > 0 ){
                    lista.Add(cliCant);
                }
                

                
            }

            void Swap<T>(IList<T> list, int indexA, int indexB)
            {
                T tmp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = tmp;
            }

            void IntArrayBubbleSort (List<ClienteCantidad> data)
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
        [Route("Clientes/AltaCliente")]
        public ActionResult<ResultadoAPI> AltaClienteComando([FromBody]ComandoCrearCliente comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.nombre.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre del Cliente";
                return resultado;
            }
            if (comando.cantidadPedidos.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la cantidad de pedidos del cliente";
                return resultado;
            }
            if (comando.clasificaciones.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese clasificacion del cliente";
                return resultado;
            }
            if (comando.telefono.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el telefono del cliente";
                return resultado;
            }
            if (comando.mail.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el E-mail del cliente";
                return resultado;
            }
            if (comando.direccion.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la direccion del cliente";
                return resultado;
            }
            if (comando.idZona.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la zona del cliente";
                return resultado;
            }

            var cli = new Clientes();
            cli.nombre = comando.nombre;
            cli.cantidadPedidos = comando.cantidadPedidos;
            cli.clasificaciones = comando.clasificaciones;
            cli.telefono = comando.telefono;
            cli.mail = comando.mail;
            cli.direccion = comando.direccion;
            cli.idZona = comando.idZona;
            cli.Zonas = db.Zonas.Where(c => c.id == comando.idZona).FirstOrDefault();

            db.Clientes.Add(cli);
            db.SaveChanges();
            resultado.Ok = true;
            resultado.Return = db.Clientes.ToList();
            return resultado;
        }

        [HttpPut]
        [Route("Clientes/UpdateCliente")]
        public ActionResult<ResultadoAPI> UpdateCliente([FromBody]ComandoUpdateCliente comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.id.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Debe seleccionar un cliente";
                return resultado;
            }
            if (comando.nombre.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre del Cliente";
                return resultado;
            }
            if (comando.cantidadPedidos.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la cantidad de pedidos del cliente";
                return resultado;
            }
            if (comando.clasificaciones.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese clasificacion del cliente";
                return resultado;
            }
            if (comando.telefono.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el telefono del cliente";
                return resultado;
            }
            if (comando.mail.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese el E-mail del cliente";
                return resultado;
            }
            if (comando.direccion.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la direccion del cliente";
                return resultado;
            }
            if (comando.idZona.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la zona del cliente";
                return resultado;
            }

            var cli = db.Clientes.Where(c => c.id == comando.id).FirstOrDefault();
            if (cli != null)
            {
                cli.nombre = comando.nombre;
                cli.cantidadPedidos = comando.cantidadPedidos;
                cli.clasificaciones = comando.clasificaciones;
                cli.telefono = comando.telefono;
                cli.mail = comando.mail;
                cli.direccion = comando.direccion;
                cli.idZona = comando.idZona;


                db.Clientes.Update(cli);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Clientes.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("Clientes/EliminarCliente/{idCliente}")]
        public ActionResult<ResultadoAPI> EliminarCliente(int idCliente, string token) {
            var resultado = new ResultadoAPI();

            if (idCliente.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del pedido";
                return resultado;
            }

            var cli = db.Clientes.Where(c => c.id == idCliente).FirstOrDefault();
            if (cli != null) {
                db.Clientes.Remove(cli);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Clientes.ToList();

            return resultado;

        }

        [HttpGet]
        [Route("Clientes/reporteClientes")]
        public ActionResult<ResultadoAPI> ObtenerReportes(DateTime lim_inf, DateTime lim_sup, string token){
            ResultadoAPI resultado = new ResultadoAPI();
            List<ClienteCantidad> lista = new List<ClienteCantidad>();
            var clientes = db.Clientes.ToList();
            foreach (Clientes cli in clientes)
            {
                ClienteCantidad cliCant = new ClienteCantidad();
                var cantidad = 0;
                var pedidos_del_cliente = db.Pedidos.Where(c => c.idCliente == cli.id).ToList();
                foreach (Pedidos ped in pedidos_del_cliente)
                {
                 if ((lim_inf < ped.fecha) && (ped.fecha < lim_sup)){
                    cantidad++;
                 }   
                }
                cliCant.nombre = cli.nombre;
                cliCant.cantidad = cantidad;
                
                if(cliCant.cantidad > 0 ){
                    lista.Add(cliCant);
                }

                
            }

            void Swap<T>(IList<T> list, int indexA, int indexB)
            {
                T tmp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = tmp;
            }

            void IntArrayBubbleSort (List<ClienteCantidad> data)
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

    }
}