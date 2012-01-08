using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Core
{
    public class DisplayUserException : Exception
    {
        public DisplayUserException(string message) : base(message)
        {
        }
    }
}
