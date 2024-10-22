using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.Utility;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

internal sealed class CreateBulkPermissionArgsValidator
    : AbstractValidator<CreateBulkPermissionArgs>
{
    public CreateBulkPermissionArgsValidator()
    {
        RuleForEach(a => a.Permissions)
            .SetValidator(new CreatePermissionArgsValidator());

        RuleFor(a => a.Permissions)
            .Must(permissions =>
            {
                var duplicatedKeys = permissions
                .GroupBy(p => p.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
                if (duplicatedKeys.Count != 0)
                {
                    return false;
                }

                return true;
            })
            .WithMessage("Duplicated permissions keys")

            .Must(permissions =>
            {
                var existPermissionsWithoutAppId = permissions.Exists(p => Guard.IsNull(p.AppId));
                if (existPermissionsWithoutAppId && accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID)
                {
                    return false;
                }

                return true;
            })
            .WithMessage("Can't create permission to auth api app")
            
            .Must(permissions =>
            {
                var appsIds = permissions
                    .GroupBy(a => a.AppId)
                    .Where(a => Guard.IsNotNullOrEmpty(a.Key))
                .Select(g => g.Key!)
                    .ToList();

                if (appsIds.Count != 0)
                {
                    var validAppsIds = accountLogged.AppsIds;

                    var invalidAppsIds = appsIds
                        .Where(id => !validAppsIds.Contains(id))
                        .ToList();

                    return false;
                }
                return true;
            })
            .WithMessage("Some appIds are invalid");
    }
}
