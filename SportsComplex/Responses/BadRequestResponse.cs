using System;

namespace SportsComplex.Responses
{
    public class BadRequestResponse
    {
        public string ErrorMessage { get; }

        public BadRequestResponse(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            ErrorMessage = message;
        }
    }
}
