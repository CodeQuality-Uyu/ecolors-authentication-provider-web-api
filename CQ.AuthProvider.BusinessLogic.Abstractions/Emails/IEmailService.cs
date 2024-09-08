namespace CQ.AuthProvider.BusinessLogic.Emails
{
    internal interface IEmailService
    {
        Task SendAsync(
            string to,
            EmailTemplateKey templateKey,
            object templateParams);
    }
}
