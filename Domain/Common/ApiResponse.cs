namespace Domain.Common;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }

    public static new ApiResponse<T> Fail(string message)
    {
        return new ApiResponse<T> { Succeeded = false, Message = message };
    }

    public static new ApiResponse<T> Success(T data, string? message = "Action executed with success.")
    {
        return new ApiResponse<T> { Data = data, Succeeded = false, Message = message! };
    }
}
