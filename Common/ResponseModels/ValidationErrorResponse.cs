using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResponseModels
{
    public class ValidationErrorResponse<T> : ErrorResponse<T>
    {
        public ValidationErrorResponse(List<string> errors)
            : base(errors, ResponseType.ValidationError)
        {
        }
    }
}
