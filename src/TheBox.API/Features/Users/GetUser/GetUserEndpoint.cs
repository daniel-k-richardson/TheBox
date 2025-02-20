#region
using MediatR;
using TheBox.API.Configurations.Interfaces;
using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Exceptions;
#endregion

namespace TheBox.API.Features.Users.GetUser;

public sealed class GetUserEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/users/{id:guid}", async (
            IMediator mediator,
            Guid id,
            CancellationToken cancellationToken) =>
        {
            var query = new GetUserQuery(new UserId(id));
            try
            {
                var response = await mediator.Send(query, cancellationToken);

                return Results.Ok(response);
            }
            catch (UserNotFoundException)
            {
                return Results.NotFound("The user was not found");
            }
        });
    }
}
