using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vn_post_server.Models
{
    public class ApiResponse <T>
    {
        public int error;
        public String message;
        public T data;
    }
}