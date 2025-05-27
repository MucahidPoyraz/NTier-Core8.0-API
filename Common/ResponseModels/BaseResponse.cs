using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResponseModels
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public ResponseType ResponseType { get; set; }
        public List<string>? Errors { get; set; }
    }
}
