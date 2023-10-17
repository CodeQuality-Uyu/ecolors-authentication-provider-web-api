using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig.Firebase.Exceptions
{
    internal sealed class FirebaseInitAuthException : FirebaseInitAppException
    {
        public FirebaseInitAuthException(string projectId) : base(projectId, $"Init of firebase auth of project {projectId} failed") { }
    }
}
