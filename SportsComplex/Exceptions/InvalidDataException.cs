using System;

namespace SportsComplex.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
}
