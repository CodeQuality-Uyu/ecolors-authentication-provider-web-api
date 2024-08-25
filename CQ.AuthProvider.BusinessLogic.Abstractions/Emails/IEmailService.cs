namespace CQ.AuthProvider.BusinessLogic.Abstractions.Emails
{
    internal interface IEmailService
    {
        Task SendAsync(
            string to,
            string template,
            string templateParams);
    }
}
