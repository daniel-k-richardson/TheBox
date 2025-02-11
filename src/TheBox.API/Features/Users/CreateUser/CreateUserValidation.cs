using FluentValidation;
using TheBox.Domain.Users.Entities;

namespace TheBox.API.Features.Users.CreateUser;

public sealed class CreateUserValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidation()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty();

        RuleFor(user => user.LastName)
            .NotEmpty();
    }
}