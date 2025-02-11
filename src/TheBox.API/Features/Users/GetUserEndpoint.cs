using MediatR;
using TheBox.API.Interfaces;
using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Exceptions;

namespace TheBox.API.Features.Users;

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
            catch (UserNotFoundException e)
            {
                return Results.NotFound("The user was not found");
            }
        });
    }
}