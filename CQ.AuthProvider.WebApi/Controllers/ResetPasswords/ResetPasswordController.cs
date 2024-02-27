using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.ResetPasswords
{
    [ApiController]
    [Route("reset-passwords")]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IResetPasswordService _resetPasswordService;

        public ResetPasswordController(IResetPasswordService resetPasswordService)
        {
            _resetPasswordService = resetPasswordService;
        }

        [HttpPost]
        public async Task<ResetPasswordApplication> CreateAsync(ResetPasswordRequest request)
        {
            var email = request.Map();

            var resetPassword = await _resetPasswordService.CreateAsync(email).ConfigureAwait(false);

            return resetPassword;
        }

        [HttpPut("{id}")]
        public async Task AcceptAsync(string id, ResetPasswordAcceptedRequest request)
        {
            var mapped = request.Map();

            await _resetPasswordService.AcceptAsync(id, mapped).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task GetAsync(string id)
        {
            await _resetPasswordService.GetActiveByIdAsync(id).ConfigureAwait(false);
        }
    }
}
