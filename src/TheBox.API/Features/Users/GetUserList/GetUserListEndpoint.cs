using MediatR;
using TheBox.API.Configurations.Interfaces;

namespace TheBox.API.Features.Users.GetUserList;

public class GetUserListEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/users",
            async (IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserListQuery();
                var response = await mediator.Send(query, cancellationToken);

                return Results.Ok(response);
            });
    }
}
