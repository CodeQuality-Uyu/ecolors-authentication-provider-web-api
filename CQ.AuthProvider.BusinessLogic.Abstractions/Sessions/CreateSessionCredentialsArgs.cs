using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions
{
    public readonly struct CreateSessionCredentialsArgs
    {
        public readonly string Email;

        public readonly string Password;

        public readonly string AppId;

        public CreateSessionCredentialsArgs(
            string email,
            string password,
            string appId)
        {
            Email = Guard.Encode(
                email,
                nameof(email));
            Guard.ThrowIsInputInvalidEmail(Email);
            
            Password = Guard.Encode(
                password,
                nameof(password));
            Guard.ThrowIsInputInvalidPassword(
                Password,
                minLength:6);

            Db.ThrowIsInvalidId(
                appId,
                nameof(appId));
            AppId = appId;
        }
    }
}
