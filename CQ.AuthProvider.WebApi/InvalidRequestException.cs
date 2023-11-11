namespace CQ.AuthProvider.WebApi
{
    public sealed class InvalidRequestException : Exception
    {
        public readonly string Prop;

        public readonly string Error;

        public InvalidRequestException(string prop, string error)
        {
            this.Prop = prop;
            this.Error = error;
        }
    }
}
