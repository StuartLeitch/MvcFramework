using System.Linq;
using System;

namespace MvcFramework.Mvc.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RetainHttpsAttribute : Attribute {}
}