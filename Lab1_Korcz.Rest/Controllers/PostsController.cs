using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_Korcz.Rest.Context;
using Lab1_Korcz.Rest.Filters;
using Lab1_Korcz.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Lab1_Korcz.Rest.Controllers
{
    [ApiController][ApiKeyAuth]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly AzureDbContext db;
        public Posts post;
        public PostsController(AzureDbContext db)
        {
            this.db = db;
        }

        public AzureDbContext Db { get; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Post.ToList());
        }


        [HttpGet("find/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var posts = db.Post.FirstOrDefault(w => w.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost("add")]
        public IActionResult POST()
        {
            Posts post = new Posts
            {
                UserID = Request.Form["userId"],
                CreatedDate = Request.Form["createdDate"],
                Content = Request.Form["Content"],
                UserName = Request.Form["UserName"],
                UserLastName = Request.Form["UserLastName"]

            };
            db.Post.Add(post);
            db.SaveChanges();
            return Ok(post);
        }

        [HttpGet("delete/{test}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var post = db.Post.FirstOrDefault(w => w.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            db.Post.Remove(post);
            db.SaveChanges();
            return Ok("Deleted");
        }
        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id)
        {
            var post = db.Post.FirstOrDefault(w => w.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            post.UserID = Request.Form["userId"];
            post.CreatedDate = Request.Form["createdDate"];
            post.Content = Request.Form["content"];
            post.UserName = Request.Form["UserName"];
            post.UserLastName = Request.Form["UserLastName"];
            db.SaveChanges();
            return Ok(post);

        }
    }
}


