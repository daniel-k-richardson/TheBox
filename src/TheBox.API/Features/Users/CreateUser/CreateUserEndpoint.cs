using FluentValidation;
using MediatR;
using TheBox.API.Configurations.Interfaces;

namespace TheBox.API.Features.Users.CreateUser;

public class CreateUserEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/users", async (IMediator mediator, CreateUserCommand command, IValidator<CreateUserCommand> validator) =>
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }

            await mediator.Send(command);

            return Results.Ok();
        });
    }
}