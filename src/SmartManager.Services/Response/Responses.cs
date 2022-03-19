using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SmartManager.API.Response
{
    public static class Responses {
        public static ResultErrorResponse DomainErrorMessage(string message, List<String> errors) {
            return new ResultErrorResponse {
                Message = message,
                Status = 400,
                Errors = new {
                    Domain = errors
                }
            };
        } 
    }
}