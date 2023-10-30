using CQ.AuthProvider.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using PlayerFinder.Auth.Core;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("reset-password")]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IResetPasswordService _resetPasswordService;

        public ResetPasswordController(IResetPasswordService resetPasswordService)
        {
            this._resetPasswordService = resetPasswordService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ResetPasswordRequestUnauthenticated request)
        {
            var resetPassword = await this._resetPasswordService.CreateAsync(request.Email).ConfigureAwait(false);

            return Ok(resetPassword);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AcceptAsync(string id, [FromBody] ResetPasswordRequest request)
        {
            await this._resetPasswordService.AcceptAsync(id, new NewPasswordRequest
            {
                NewPassword = request.NewPassword,
                Code = request.Code,
                Email = request.Email
            }).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var resetPassword = await this._resetPasswordService.GetActiveResetPasswordByIdAsync(id).ConfigureAwait(false);

            return Ok(resetPassword);
        }
    }

    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }

        public string Code { get; set; }

        public string Email { get; set; }
    }
}
