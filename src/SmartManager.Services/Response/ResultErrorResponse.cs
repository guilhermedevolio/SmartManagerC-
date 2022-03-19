using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SmartManager.API.Response
{
    public class ResultErrorResponse {
        public string Message { get; set; }
        public int Status { get; set; }
        public dynamic Errors { get; set; }
    }
}