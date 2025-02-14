using FluentValidation;

namespace TheBox.API.Features.Users.CreateUser;

public sealed class CreateUserValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidation()
    {
        this.RuleFor(user => user.FirstName)
            .NotEmpty();

        this.RuleFor(user => user.LastName)
            .NotEmpty();
    }
}