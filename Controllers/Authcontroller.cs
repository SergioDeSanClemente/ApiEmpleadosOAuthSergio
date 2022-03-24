using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEmpleadosOAuth.Repositories;
using ApiEmpleadosOAuth.Helpers;
using ApiEmpleadosOAuth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ApiEmpleadosOAuth.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class Authcontroller : ControllerBase {
        private EmpleadosRepository repo;
        private HelperOAuthToken helper;
        public Authcontroller(EmpleadosRepository repo, HelperOAuthToken helper) {
            this.repo = repo;
            this.helper = helper;
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult Login(LoginModel model) {
            Empleado empleado = this.repo.ExistsEmpleado(model.UserName, int.Parse(model.Password));
            if (empleado == null) {
                return Unauthorized();
            } else {
                SigningCredentials credentials = new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer:this.helper.Issuer,
                    audience:this.helper.Issuer,
                    signingCredentials:credentials,
                    expires:DateTime.UtcNow.AddMinutes(30),
                    notBefore:DateTime.UtcNow
                );
                return Ok(
                    new {
                        response = new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }
    }
}
