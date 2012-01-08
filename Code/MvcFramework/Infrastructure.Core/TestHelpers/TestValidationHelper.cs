using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Core.EventBroker;

namespace Infrastructure.Core.TestHelpers
{
    public static class TestValidationHelper
    {
        public static void Write(IEnumerable<ValidationFailure> validationFailures)
        {
            foreach (var validationFailure in validationFailures.Where(x => x.Exception != null))
            {
                WriteMessage(validationFailure.Exception, 1);
            }
        }

        private static void WriteMessage(Exception ex, int indentDepth)
        {
            Console.WriteLine(ex.Message);

            for (var i = 0; i < indentDepth; i++)
            {
                Console.Write(" ");
            }

            if (ex.InnerException != null)
            {
                WriteMessage(ex.InnerException, indentDepth + 1);
            }
        }
    }
}
