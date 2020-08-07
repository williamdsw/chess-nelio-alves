﻿
using System;

namespace Entities.Exceptions
{
    public class ChessException : ApplicationException
    {
        public ChessException(string message) : base(message)
        { }
    }
}
