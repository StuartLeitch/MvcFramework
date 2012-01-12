using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Core
{
    public class DisplayUserException : Exception
    {
        public DisplayUserException(string message) : base(message)
        {
        }
    }
}
