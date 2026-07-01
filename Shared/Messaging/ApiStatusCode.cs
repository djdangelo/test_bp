namespace test_bp.Shared.Messaging
{
    public enum ApiStatusCode
    {
        // 2xx Success
        Success = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,

        // 4xx Client Errors
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        UnprocessableEntity = 422,

        // 5xx Server Errors
        InternalServerError = 500
    }
}
