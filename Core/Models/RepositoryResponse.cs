using System.Collections.Generic;

namespace Project.Models
{
    public class RepositoryResponse<T>
    {
        public RepositoryResponse(bool isSucceed, string message, List<string> errors, T data)
        {
            Succeed = isSucceed;
            Message = message;
            Errors = errors;
            Data = data;
        }
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
    }
}
