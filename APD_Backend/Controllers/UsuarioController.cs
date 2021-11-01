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

        [HttpPost]
        [Route("Usuario/AltaUsuario")]
        public ActionResult<ResultadoAPI> CrearUsuario([FromBody]ComandoCrearUsuario comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.user.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre de usuario";
                return resultado;
            }
            if (comando.password.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la contraseña del usuario";
                return resultado;
            }
            if (comando.idRol.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Debe seleccionar un rol de usuario";
                return resultado;
            }

            var usu = new Usuarios();
            usu.usuario = comando.user;
            usu.clave = comando.password;
            usu.idRol = comando.idRol;

            db.Usuarios.Add(usu);
            db.SaveChanges();
            resultado.Ok = true;
            resultado.Return = db.Usuarios.ToList();
            return resultado;
        }
    

    [HttpPut]
        [Route("Usuario/UpdateUsuario")]
        public ActionResult<ResultadoAPI> UpdateUsuario([FromBody]ComandoUpdateUsuario comando, string token) {
            var resultado = new ResultadoAPI();

            if (comando.id.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Debe seleccionar un usuario";
                return resultado;
            }
            if (comando.user.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre de usuario";
                return resultado;
            }
            if (comando.password.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese la contraseña del usuario";
                return resultado;
            }
            if (comando.idRol.Equals(null)) {
                resultado.Ok = false;
                resultado.Error = "Debe seleccionar un rol de usuario";
                return resultado;
            }

            var usu = db.Usuarios.Where(c => c.id == comando.id).FirstOrDefault();
            if (usu != null)
            {
                usu.usuario = comando.user;
                usu.clave = comando.password;
                usu.idRol = comando.idRol;
                db.Usuarios.Update(usu);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Usuarios.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("Usuario/EliminarUsuario/{idUsuario}")]
        public ActionResult<ResultadoAPI> EliminarUsuario(int idUsuario, string token) {
            var resultado = new ResultadoAPI();

            if (idUsuario.Equals("")) {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID del usuario";
                return resultado;
            }

            var usu = db.Usuarios.Where(c => c.id == idUsuario).FirstOrDefault();
            if (usu != null) {
                db.Usuarios.Remove(usu);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Usuarios.ToList();

            return resultado;

        }

        [HttpGet]
        [Route("Usuario/ObtenerUsuarios")]
        public ActionResult<ResultadoAPI> Get(string token){
            ResultadoAPI resultado = new ResultadoAPI();
            resultado.Ok = true;
            resultado.Return = db.Usuarios.ToList();
            return resultado;
        }

    
}
}
