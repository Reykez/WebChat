using System.Linq;
using FluentValidation;
using WebChat.Database;
using WebChat.Domain.Models;

namespace WebChat.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(IWebChatDbContext dbContext)
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .MinimumLength(4)
                .Custom((value, context) =>
                {
                    var isUsernameInUse = dbContext.Users.Any(r => r.Username.ToLower() == value.ToLower());
                    if(isUsernameInUse)
                        context.AddFailure("Username", "Account with this username already exists.");
                });

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}