using System;

namespace DialogSystem.Exceptions
{
    
    public class GraphEndException : Exception
    {
        public GraphEndException()
        {
        }

        public GraphEndException(string message)
            : base(message)
        {
        }

        public GraphEndException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}