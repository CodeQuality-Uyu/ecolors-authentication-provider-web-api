using CQ.Utility;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Permissions;
internal sealed class CreatePermissionArgsValidator
    : AbstractValidator<CreatePermissionArgs>
{
    public CreatePermissionArgsValidator()
    {
        RuleFor(a => a.Name)
            .RequiredString();

        RuleFor(a => a.Description)
            .RequiredString();

        RuleFor(a => a.Key)
            .RequiredString();

        RuleFor(a => a.AppId)
            .Must((appId) =>
            {
                if (Guard.IsNull(appId))
                {
                    return true;
                }

                return Db.IsIdValid(appId);
            })
            .WithMessage("Invalid id");
    }
}

internal static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> RequiredString<T>(this IRuleBuilder<T, string> validator)
    {
        var options = validator
        .NotNull().WithMessage("Can't be null")
        .NotEmpty().WithMessage("Can't be empty");

        return options;
    }
}