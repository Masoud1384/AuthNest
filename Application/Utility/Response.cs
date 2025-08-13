using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Application.Utility
{
    public class Response<T>
    {
        public bool IsSuccessed { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Item { get; set; }
        public static Response<T> Failure(string message)
        {
            var response = new Response<T>();
            response.IsSuccessed = false;
            response.ErrorMessage = message;
            return response;
        }
        public static Response<T> Succeeded(string message = "Done")
        {
            var response = new Response<T>();
            response.IsSuccessed = true;
            response.ErrorMessage = message;
            return response;
        }
    }
}
