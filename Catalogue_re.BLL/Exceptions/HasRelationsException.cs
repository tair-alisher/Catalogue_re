using System;

namespace Catalogue_re.BLL.Exceptions
{
    public class HasRelationsException : Exception
    {
        static string message = "Item has relations";
        public HasRelationsException() : base(message) { }
    }
}
