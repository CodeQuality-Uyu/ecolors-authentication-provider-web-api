using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    public readonly struct CreateSessionCredentialsArgs
    {
        public readonly string Email;

        public readonly string Password;

        public readonly string? ListenerServer;

        public CreateSessionCredentialsArgs(
            string email,
            string password,
            string? listenerServer = null)
        {
            this.Email = Guard.Encode(email, nameof(email));
            this.Password = Guard.Encode(password, nameof(password));

            Guard.ThrowIsInvalidEmailFormat(this.Email);

            ListenerServer = listenerServer;
        }
    }
}
