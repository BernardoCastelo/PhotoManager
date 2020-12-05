using System;

namespace BusinessLayer
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string filetype) : base(filetype)
        {
        }
    }
}
