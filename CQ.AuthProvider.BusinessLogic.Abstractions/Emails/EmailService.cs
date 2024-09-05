
namespace CQ.AuthProvider.BusinessLogic.Abstractions.Emails;
internal sealed class EmailService : IEmailService
{
    public Task SendAsync(string to, EmailTemplateKey templateKey, object templateParams)
    {
        throw new NotImplementedException();
    }
}
