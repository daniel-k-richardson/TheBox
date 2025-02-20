#region
using MediatR;
using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Interfaces;
#endregion

namespace TheBox.API.Features.Users.CreateUser;

public sealed class CreateUserHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.FirstName, request.LastName);
        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}
