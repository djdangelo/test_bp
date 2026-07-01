namespace test_bp.Shared.Messaging;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public ApiStatusCode Status { get; init; }
    public T? Data { get; init; }
    public List<string>? Errors { get; init; }
    public string? TraceId { get; init; }

    public ApiResponse(bool success, string? message, ApiStatusCode status, T? data = default, List<string>? errors = null)
    {
        Success = success;
        Message = message;
        Status = status;
        Data = data;
        Errors = errors;
    }

    public static ApiResponse<T> Ok(T data, string? message = "Success") =>
        new(true, message, ApiStatusCode.Success, data);

    public static ApiResponse<T> Created(T data, string? message = "Resource created successfully") =>
        new(true, message, ApiStatusCode.Created, data);

    public static ApiResponse<T> Failure(ApiStatusCode status, string message, List<string>? errors = null) =>
        new(false, message, status, default, errors);

    public static ApiResponse<T> NotFound(string message = "Resource not found") =>
        Failure(ApiStatusCode.NotFound, message);

    public static ApiResponse<T> BadRequest(string message, List<string>? errors = null) =>
        Failure(ApiStatusCode.BadRequest, message, errors);

    public static ApiResponse<T> InternalError(string message = "An internal error occurred") =>
        Failure(ApiStatusCode.InternalServerError, message);

    public static ApiResponse<T> Unauthorized(string message = "Unauthorized access") => Failure(ApiStatusCode.Unauthorized, message); 

    public static ApiResponse<T> Forbidden(string message = "Access forbidden") => Failure(ApiStatusCode.Forbidden, message);
}