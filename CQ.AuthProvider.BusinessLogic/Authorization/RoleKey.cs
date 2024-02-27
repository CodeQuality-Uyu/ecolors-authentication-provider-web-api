using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class RoleKey
    {
        private readonly string Value;

        public RoleKey(string value)
        {
            Value = Guard.Encode(value.Trim());

            Guard.ThrowIsNullOrEmpty(Value, nameof(Value));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
