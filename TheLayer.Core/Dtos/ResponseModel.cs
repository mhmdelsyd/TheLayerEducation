using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLayer.Core.Dtos
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Response { get; set; }
    }
}
