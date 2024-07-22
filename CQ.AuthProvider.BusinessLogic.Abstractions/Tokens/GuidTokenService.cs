using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Tokens
{
    internal sealed class GuidTokenService
        : ITokenService
    {
        public string Create()
        {
            return Db.NewId();
        }
    }
}
