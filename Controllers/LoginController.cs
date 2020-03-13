using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Vn_post_server.Common;
using Vn_post_server.Models;

namespace Vn_post_server.Controllers
{
    public class LoginController : ApiController
    {
        Vn_postEntities api = new Vn_postEntities();
        [HttpPost]
        public ApiResponse<object> postData([FromBody] user data)
        {
            var query = api.users.Where(m => m.username == data.username && m.password == data.password);

            if (query.Any())
            {
                var username = query.FirstOrDefault();
                // trang khi goi save lam luu lai cai object nay (luu lai se lam loi password vi phia duoi set password= null)
                api.Entry(username).State = System.Data.Entity.EntityState.Detached;
                // ko tra ve passsword
                username.password = null;

                return new ApiResponse<object> { error = 0, message = "thanh cong", data = new { token= JwtUtil.GenerateToken(username.username, username.id_user), info = username } };
            }
            else
                return new ApiResponse<object> { error = 1, message = " khong thanh cong", data = null };

        }

        [Authorize]
        [HttpGet]


        public int? get()
        {
            return JwtUtil.getUserId(HttpContext.Current.User);
        }

       // tao token
        [HttpGet]
        public string maketoken()
        {
           
            return JwtUtil.GenerateToken("giaynhap",1);
        }

    }
}