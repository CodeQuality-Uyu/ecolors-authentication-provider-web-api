using Amazon.Runtime.Internal;
using CQ.UnitOfWork.Core;
using FirebaseAdmin.Auth;
using MongoDB.Driver;
using Newtonsoft.Json;
using PlayerFinder.Auth.Core.Domain;
using PlayerFinder.Auth.Core.Exceptions;
using System.Net.Http.Json;
using System.Security.Authentication;

namespace PlayerFinder.Auth.Core
{
    internal class FirebaseAuthService : IAuthService
    {
        private readonly FirebaseAuth _firebaseAuth;
        private readonly IUserService _userService;

        public FirebaseAuthService(FirebaseAuth firebaseAuth, IUserService userService)
        {
            this._firebaseAuth = firebaseAuth;
            this._userService = userService;
        }

        public async Task<User> CreateAsync(AuthUser auth)
        {
            var userRecords = new UserRecordArgs
            {
                Email = auth.Email,
                Password = auth.Password,
                DisplayName = $"{auth.Firstname} {auth.Lastname}",
            };

            var firebaseAuth = await this._firebaseAuth.CreateUserAsync(userRecords).ConfigureAwait(false);

            var newUser = await this._userService.CreateAsync(new User
            {
                Id = firebaseAuth.Uid,
                Email = firebaseAuth.Email,
                Name = firebaseAuth.DisplayName,
                Firstname = auth.Firstname,
                Lastname = auth.Lastname,
            });


            return newUser;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> DeserializeTokenAsync(string token)
        {
            var session = await this._firebaseAuth.VerifyIdTokenAsync(token).ConfigureAwait(false);
            var uid = session.Uid;

            var user = await this._firebaseAuth.GetUserAsync(uid).ConfigureAwait(false);
            var nameSplitted = user.DisplayName.Split(' ');
            var firstname = nameSplitted[0];
            var lastname = nameSplitted[1];

            return new User
            {
                Id = uid,
                Email = user.Email,
                Name = user.DisplayName,
                Firstname = firstname,
                Lastname = lastname,
            };
        }

        public async Task UpdatePasswordAsync(string newPassword, User userLogged)
        {
            var userUpdated = new UserRecordArgs
            {
                Uid = userLogged.Id,
                Password = newPassword,
            };

            var userRecord = await this._firebaseAuth.UpdateUserAsync(userUpdated).ConfigureAwait(false);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var firebaseUser = await this._firebaseAuth.GetUserByEmailAsync(email).ConfigureAwait(false);

            var nameSplitted = firebaseUser.DisplayName.Split(" ");
            var firstname = nameSplitted[0];
            var lastname = nameSplitted[1];

            return new User
            {
                Id = firebaseUser.Uid,
                Email = firebaseUser.Email,
                Name = firebaseUser.DisplayName,
                Firstname = firstname,
                Lastname = lastname,
            };
        }
    }
}