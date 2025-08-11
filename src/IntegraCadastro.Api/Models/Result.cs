namespace IntegraCadastro.Api.Models;

public class Result<T>
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public T? Data { get; init; }

    private Result(bool success, T? data, string? errorMessage)
    {
        Success = success;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Ok(T data) =>
        new Result<T>(true, data, null);

    public static Result<T> Fail(string errorMessage) =>
        new Result<T>(false, default, errorMessage);
}