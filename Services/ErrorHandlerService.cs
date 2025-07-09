using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using TOHBackend.DTOS;

namespace TOHBackend.Services
{
    public class ErrorHandlerService
    {
        public ErrorHandlerDTO HandleError(Exception ex)
        {
            ErrorHandlerDTO error = new ErrorHandlerDTO();
            error.Message = ex.Message;

            switch (ex) 
            {
                case ArgumentNullException:
                    error.StatusCode = 400;
                    break;

                case InvalidDataException:
                case BadHttpRequestException:
                    error.StatusCode = 404;
                    break;

                default:
                    error.StatusCode = 500;
                    break; 
            }

            return error;
        }
    }
}
