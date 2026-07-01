namespace test_bp.Shared.Messaging
{
    public static class ApiResponseExtensions
    {
        public static IResult ToResult<T>(this ApiResponse<T> response)
        {
            return response.Status switch
            {
                ApiStatusCode.Success => TypedResults.Ok(response),
                ApiStatusCode.Created => TypedResults.Created(string.Empty, response),
                ApiStatusCode.BadRequest => TypedResults.BadRequest(response),
                ApiStatusCode.Unauthorized => TypedResults.Json(response, statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized),
                ApiStatusCode.Forbidden => TypedResults.Json(response, statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden),
                ApiStatusCode.NotFound => TypedResults.NotFound(response),
                ApiStatusCode.Conflict => TypedResults.Conflict(response),
                _ => TypedResults.InternalServerError(response)
            };
        }
    }
}
