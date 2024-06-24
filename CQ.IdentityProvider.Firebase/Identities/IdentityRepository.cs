using FirebaseAdmin.Auth;
using CQ.Exceptions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.Utility;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions.Exceptions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.IdentityProvider.Firebase.AppConfig;

namespace CQ.IdentityProvider.Firebase.Identities;

internal sealed class IdentityRepository(
    IdentityFirebaseSection identitySection,
    HttpClientAdapter firebaseApi,
    FirebaseAuth firebaseAuth)
    : IIdentityRepository,
    IIdentityProviderHealthService
{
    public async Task CreateAsync(Identity identity)
    {
        var userRecords = new UserRecordArgs
        {
            Uid = identity.Id,
            Email = identity.Email,
            Password = identity.Password,
        };

        try
        {
            await firebaseAuth
                .CreateUserAsync(userRecords)
                .ConfigureAwait(false);
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.AuthErrorCode == AuthErrorCode.EmailAlreadyExists)
            {
                throw new SpecificResourceDuplicatedException<Account>(nameof(Account.Email), userRecords.Email);
            }

            throw;
        }
    }

    public async Task<Identity> GetByEmailAsync(string email)
    {
        try
        {
            var userWithEmail = await firebaseAuth
                .GetUserByEmailAsync(email)
                .ConfigureAwait(false);

            if (userWithEmail == null)
            {
                throw new SpecificResourceNotFoundException<Identity>(nameof(Identity.Email), email);
            }

            return new Identity
            {
                Id = userWithEmail.Uid,
                Email = email,
            };
        }
        catch (FirebaseAuthException ex)
        {
            throw new AuthNotFoundException(email, ex);
        }
    }

    public async Task UpdatePasswordAsync(string identityId, string newPassword)
    {
        var userUpdated = new UserRecordArgs
        {
            Uid = identityId,
            Password = newPassword,
        };

        await firebaseAuth
            .UpdateUserAsync(userUpdated)
            .ConfigureAwait(false);
    }

    public async Task<Identity> GetByCredentialsAsync(string email, string password)
    {
        var response = await firebaseApi
            .PostAsync<LoginFirebaseResponse, FirebaseError>(
                $"v1/accounts:signInWithPassword?key={identitySection.ApiKey}",
                new { email, password, returnSecureToken = true },
                (error) =>
                {
                    return ProcessLoginError(error, email);
                })
            .ConfigureAwait(false);

        return null;
    }

    private static Exception? ProcessLoginError(FirebaseError error, string email)
    {
        if (error.Error.AuthCode == FirebaseAuthErrorCode.EmailNotFound ||
        error.Error.AuthCode == FirebaseAuthErrorCode.InvalidPassword)
        {
            return new InvalidCredentialsException(email);
        }

        if (error.Error.AuthCode == FirebaseAuthErrorCode.UserDisabled)
        {
            return new AuthDisabledException(email);
        }

        return null;
    }

    #region Health
    public string GetProvider()
    {
        return "Firebase";
    }

    public string GetName()
    {
        return $"";
    }

    public bool Ping()
    {
        try
        {
            var task = firebaseAuth.GetUserByEmailAsync("ping@gmail.com");

            task.Wait();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion

    public async Task DeleteByIdAsync(string id)
    {
        await firebaseAuth
            .DeleteUserAsync(id)
            .ConfigureAwait(false);
    }

    #region Unused
    public async Task<bool> ExistByEmailAsync(string email)
    {
        try
        {
            await GetByEmailAsync(email).ConfigureAwait(false);

            return true;
        }
        catch (AuthNotFoundException)
        {
            return false;
        }
    }

    public async Task<Session> GetByTokenAsync(string token)
    {
        var result = await firebaseAuth
            .VerifyIdTokenAsync(token)
            .ConfigureAwait(false);

        return null;
    }
    #endregion
}