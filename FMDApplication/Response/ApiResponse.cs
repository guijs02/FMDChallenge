using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FMDApplication.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(T? data, bool success = true, string? message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }
    }

    public class PagedResponse<TData> : ApiResponse<TData>
    {
        public PagedResponse(
                        TData? data,
                        int totalCount = 10,
                        int currentPage = 1,
                        int pageSize = 5
                        ) : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; } 
        public int TotalCount { get; private set; }
    }
}
