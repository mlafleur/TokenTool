using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TokenTool.Core.Exceptions
{
    public class MissingValueException : SystemException
    {
        const string DefaultMessage = "One or more required properties contain null or empty values.";
        public MissingValueException() : base()
        {
        }

        public MissingValueException(string message) : base(message)
        {
        }

        public MissingValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
