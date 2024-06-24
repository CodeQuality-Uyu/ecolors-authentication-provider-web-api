

namespace CQ.IdentityProvider.Firebase.Identities;

internal sealed record class FirebaseError
{
    public ConcreteFirebaseError Error { get; set; }
}

internal sealed record class ConcreteFirebaseError
{
    public int Code { get; set; }

    public FirebaseAuthErrorCode AuthCode { get { return new FirebaseAuthErrorCode(Message); } }

    public string Message { get; set; }
}

internal sealed record class FirebaseAuthErrorCode
{
    public static FirebaseAuthErrorCode EmailNotFound = new("EMAIL_NOT_FOUND");

    public static FirebaseAuthErrorCode InvalidPassword = new("INVALID_PASSWORD");

    public static FirebaseAuthErrorCode UserDisabled = new("USER_DISABLED");

    public string Value { get; }

    public FirebaseAuthErrorCode(string value)
    {
        Value = value;
    }
}
