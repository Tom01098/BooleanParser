using System;

namespace BooleanParser
{
    [Serializable]
    public class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException() { }

        public UnexpectedTokenException(string message) 
            : base(message) { }

        public UnexpectedTokenException(string message, Exception inner) 
            : base(message, inner) { }

        protected UnexpectedTokenException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) 
            : base(info, context) { }
    }
}
