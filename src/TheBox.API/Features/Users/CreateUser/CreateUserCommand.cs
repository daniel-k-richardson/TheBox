using MediatR;

namespace TheBox.API.Features.Users.CreateUser;

public record CreateUserCommand(string FirstName, string LastName) : IRequest;
