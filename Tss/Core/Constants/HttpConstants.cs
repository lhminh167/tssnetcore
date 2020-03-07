using System.Net;

namespace Tss.Core.Constants
{
    public static class HttpContentType {
        public const string ProblemJson = "application/problem+json";
    }

    public static class HttpMessage {
        public const string InputValidationError = "Input validation failed. Please check the 'errors' field for more details.";
        public const string InternalServerErrorTitle = "An unexpected error has occurred! Please use following Error Instance ID to contact the administrator for a solution.";
        public const string InternalServerErrorDetail = "Please check the 'instance' field to get the Error Instance ID. This ID will be used to retrieve error's information from the log.";

    }

    public static class URL {
        private const string ErrorTypeUrl = "https://httpstatuses.com/";
        public static string GetErrorType(HttpStatusCode status)
        {
            return ErrorTypeUrl + (int)status;
        }
    }
}
