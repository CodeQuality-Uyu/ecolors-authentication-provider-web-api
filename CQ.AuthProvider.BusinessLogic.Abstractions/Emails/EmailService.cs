namespace CQ.AuthProvider.BusinessLogic.Emails;
internal sealed class EmailService : IEmailService
{
    public Task SendAsync(string to, EmailTemplateKey templateKey, object templateParams)
    {
        throw new NotImplementedException();
    }
}
