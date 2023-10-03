using CQ.ApiFilters.Core;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQExceptionFilter : ExceptionHandlerFilter
    {
        protected override HttpException BuildErrorResponse(Exception exception)
        {
            try
            {
                throw exception;
            }
            catch (CodesNotMatchException)
            {
                return new HttpException
                {
                    StatusCode = 400,
                    InnerCode = "CodeNotMatch",
                    Message = "Codes dont't match"
                };
            }
            catch (ArgumentNullException ex)
            {
                return new HttpException
                {
                    StatusCode = 400,
                    InnerCode="MissingProp",
                    Message = ex.Message
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
