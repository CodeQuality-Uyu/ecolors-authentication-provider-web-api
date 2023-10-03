using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class DuplicatedEmailException : Exception
    {
        public string Email { get; }

        public DuplicatedEmailException(string email) : base($"Email {email} is in use") { Email = email; }
    }
}
