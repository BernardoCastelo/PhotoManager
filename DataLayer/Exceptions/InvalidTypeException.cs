using System;

namespace DataLayer.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string filetype) : base(filetype)
        {
        }
    }
}
