using PaymentValidationAPI.Models.Common;

namespace PaymentValidationAPI.Models
{
    /// <summary>
    /// Response class to hold data and errors
    /// </summary>
    /// <typeparam name="T">The type of data</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Indicates if the request details were valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return Errors == null || Errors.Count == 0;
            }
        }

        /// <summary>
        /// Holds the response data. This will be null if there are errors.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Holds the list of errors
        /// </summary>
        public List<Error> Errors { get; set; }
        
        public Response()
        {
            Errors = new List<Error>();
        }
    }
}
