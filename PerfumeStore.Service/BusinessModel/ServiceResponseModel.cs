using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ServiceResponse<T> SuccessResponse(T data) => new ServiceResponse<T> { Data = data, Success = true };
        public static ServiceResponse<T> ErrorResponse(string message) => new ServiceResponse<T> { Success = false, Message = message };
    }

}
