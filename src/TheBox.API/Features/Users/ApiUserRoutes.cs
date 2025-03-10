namespace TheBox.API.Features.Users;

public static class ApiUserRoutes
{
    public const string GetUsers =  BaseRoute.ApiBase + "users";
    public const string GetUser =  BaseRoute.ApiBase + "users/{id:guid}";
    public const string CreateUser =  BaseRoute.ApiBase + "users";
}
