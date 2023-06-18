using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProjeto.DTos
{
    public class ApiResponse
    {
        public bool Sucess { get; set; }
        public object Data { get; set; }

        public ApiResponse(bool sucess, object data)
        {
            Sucess = sucess;
            Data = data;
        }
        public ApiResponse()
        {

        }
    }
}
