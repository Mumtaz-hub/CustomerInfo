using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Api.Middlewares
{
    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionsHandlerMiddleware> logger;

        public ExceptionsHandlerMiddleware(RequestDelegate next, ILogger<ExceptionsHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (DbUpdateException ex)
            {
                await HandleDatabaseException(context, ex);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode != null)
                    await HandleException(context, ex.Message, (HttpStatusCode) ex.StatusCode);
                else
                    await HandleException(context, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleDatabaseException(HttpContext context, DbUpdateException ex)
        {
            await WriteException(context, PopulateDataBaseExceptionInfo(ex), HttpStatusCode.InternalServerError);
        }

        private async Task HandleException(HttpContext context, string errorMessage, HttpStatusCode statusCode)
        {
            await WriteException(context, errorMessage, statusCode);
        }

        private async Task WriteException(HttpContext context, string exceptionMessage, HttpStatusCode statusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new FailureResult(exceptionMessage);
            response.StatusCode = (int)statusCode;
            var result = responseModel.ToJson();
            logger.LogError($"Exception Occured {result}");
            await response.WriteAsync(result);
        }

        private static string PopulateDataBaseExceptionInfo(Exception exception)
        {
            var errorMessage = string.Empty;
            var rootException = exception.GetBaseException();

            var sqlException = rootException as SqlException;
            var exceptionMessage = rootException.Message;

            if (sqlException == null)
                return exceptionMessage;

            const string sqlErrorMessage = "Cannot insert duplicate record in {0}";

            // cannot insert duplicate record
            switch (sqlException.Number)
            {
                case 515:
                case 547:
                    errorMessage = sqlException.Message;
                    break;
                case 2601:
                    var startPos = exceptionMessage.IndexOf(@"with unique index '", StringComparison.Ordinal);
                    var endPos = exceptionMessage.IndexOf(@"'.", startPos, StringComparison.Ordinal);
                    startPos += "with unique index '".Length;
                    var indexName = exceptionMessage.Substring(startPos, (endPos - startPos));
                    var qualifiedIndexName = indexName.Substring(indexName.IndexOf('_') + 1).Replace('_', ' ');
                    errorMessage = string.Format(sqlErrorMessage, qualifiedIndexName);
                    break;
            }

            return errorMessage;
        }
    }
}
