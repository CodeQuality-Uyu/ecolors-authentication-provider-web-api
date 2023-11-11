using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class Roles
    {
        public readonly string Value;

        public Roles(string value)
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
