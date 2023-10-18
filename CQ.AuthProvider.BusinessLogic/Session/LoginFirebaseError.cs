using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
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
}
