using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiOptimization.Application.Responses
{
    public class ResponseBuilder<T> where T : class
    {
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
