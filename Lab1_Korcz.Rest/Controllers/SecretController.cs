using System;
using Lab1_Korcz;
using Lab1_Korcz.Rest.Filters;
using Microsoft.AspNetCore.Mvc;
namespace Lab1_Korcz.Rest.Controllers
{
    [ApiKeyAuth]
    public class SecretController : ControllerBase
    {
        [HttpGet("secret")]
        public IActionResult GetSecret() {
            return Ok("I have no secrets");
        }
    }
}
