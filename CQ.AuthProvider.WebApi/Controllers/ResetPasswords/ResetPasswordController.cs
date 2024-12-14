using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.ResetPasswords;

[ApiController]
[Route("reset-passwords")]
public class ResetPasswordController(IResetPasswordService resetPasswordService)
    : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync(CreateResetPasswordArgs request)
    {
        await resetPasswordService
            .CreateAsync(request)
            .ConfigureAwait(false);
    }

    [HttpPut("{id}")]
    public async Task AcceptAsync(
        Guid id,
        AcceptResetPasswordArgs request)
    {
        await resetPasswordService
            .AcceptAsync(
            id,
            request)
            .ConfigureAwait(false);
    }
}
