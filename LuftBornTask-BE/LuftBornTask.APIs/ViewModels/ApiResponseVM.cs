namespace LuftBornTask.APIs.ViewModels
{
    public class ApiResponseVM<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

    }
}
