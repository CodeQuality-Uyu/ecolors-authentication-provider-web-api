namespace CQ.AuthProvider.BusinessLogic.Abstractions.Emails
{
    internal interface IEmailService
    {
        Task SendAsync(
            string to,
            EmailTemplateKey templateKey,
            object templateParams);
    }
}
