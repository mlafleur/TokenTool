using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TokenTools.Core
{
    public class InvalidValueException : SystemException
    {
        const string DefaultMessage = "One or more properties contain invalid or missing values.";
        public InvalidValueException() : base()
        {
        }

        public InvalidValueException(string message) : base(message)
        {
        }

        public InvalidValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
