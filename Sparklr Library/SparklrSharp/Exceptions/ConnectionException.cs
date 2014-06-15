using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.Exceptions
{
    /// <summary>
    /// Represents an error that is related to the connectivity
    /// </summary>
    public class ConnectionException : Exception
    {
        internal ConnectionException() : base()
        {

        }

        internal ConnectionException(string message) : base(message)
        {

        }

        internal ConnectionException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
