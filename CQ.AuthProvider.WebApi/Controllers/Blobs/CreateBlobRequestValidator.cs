using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.WebApi.Controllers.Blobs;

public sealed class CreateBlobRequestValidator
    : AbstractValidator<CreateBlobRequest>
{
    public CreateBlobRequestValidator()
    {
        RuleFor(x => x.ContentType)
            .Required();
    }
}
