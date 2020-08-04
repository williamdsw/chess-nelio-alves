
using System;

namespace Entities.Exceptions
{
    public class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message)
        { }
    }
}
