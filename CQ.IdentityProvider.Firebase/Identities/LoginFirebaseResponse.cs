
namespace CQ.IdentityProvider.Firebase.Identities;

internal sealed record class LoginFirebaseResponse(
    string IdToken,
    string Email,
    string RefreshToken,
    string ExpiresIn,
    string LocalId,
    bool Registered);
