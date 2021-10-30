using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Comandos.Usuario;
using Resultados;
using Data;

namespace APD_Backend.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class UsuarioController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("Usuario/Login")]
        public ActionResult<ResultadoAPI> Login([FromBody]ComandoUsuarioLogin comando){
            var resultado = new ResultadoAPI();
            var usuario = comando.user;
            var password = comando.password;
            try {
                var usu = db.Usuarios.FirstOrDefault(x => x.usuario.Equals(usuario) && x.clave.Equals(password));
                if(usu != null) {
                    Guid g = Guid.NewGuid();
                    usu.tokenSesion = g.ToString();
                    db.SaveChanges();
                    resultado.Ok = true;
                    resultado.Return = usu;
                } else {
                    resultado.Ok = false;
                    resultado.Error = "Usuario o contrasena incorrectos";
                }

                return resultado;
            }
            catch(Exception ex) {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Usuario no encontrado - " + ex.Message;

                return resultado;
            }
        }
    }
    
}
