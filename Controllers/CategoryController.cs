using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vn_post_server.Models;

namespace Vn_post_server.Controllers
{
   
    public class CategoryController : ApiController
    {
        Vn_postEntities api = new Vn_postEntities();
        [HttpGet]
        public IHttpActionResult getCategory(int id)
        {
            var listData = api.categories.Where(m=>m.id_sort==id).Select(m=>new { m.id_category, m.category1}).ToList();
            if (listData != null)
            {
                return Ok(listData);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IHttpActionResult getData(int id)
        {

            var data = (from m2 in api.categories                       
                        join m1 in api.sorts on m2.id_sort equals m1.id
                        join m3 in api.post1 on m2.id_category equals m3.id_category
                        where (m1.id == id)
                        select new { category1 = m2.category1,
                                    id_category = m2.id_category,
                                    post = m3.post,
                                    }).ToList();
            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }
        [HttpGet]
        public IHttpActionResult getPost(int id)
        {
            var data = api.post1.Where(m => m.id_category == id).Select(m => m.post).ToList();
            if (data != null)
            {
                return Ok(data);
            }
            else
                return NotFound();
        }
        


    }
}