using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzraBrooks.XMLtoSSC
{
    class CommandReaderException : Exception
    {
        /// <summary>
        /// Default CommandReaderException
        /// </summary>
        public CommandReaderException()
        {

        }

        /// <summary>
        /// CommandReaderException with message
        /// </summary>
        /// <param name="message">Message you want the Exception to carry</param>
        public CommandReaderException(string message) : base(message)
        {

        }

        /// <summary>
        /// CommandReaderException with message and inner Exception
        /// </summary>
        /// <param name="message">Message you want the Exception to carry</param>
        /// <param name="inner">Exception that caused this exception</param>
        public CommandReaderException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
