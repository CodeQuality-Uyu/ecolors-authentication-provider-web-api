using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class SessionFirebaseService : ISessionService
    {
        private readonly FirebaseAuth _firebaseAuth;

        public SessionFirebaseService(FirebaseAuth firebaseAuth)
        {
            this._firebaseAuth = firebaseAuth;
        }

        public Task<Session> CreateAsync(CreateSessionCredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}
