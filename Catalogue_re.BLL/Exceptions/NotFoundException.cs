using System;

namespace Catalogue_re.BLL.Exceptions
{
    public class NotFoundException : Exception
    {
        static string message = "Item with given id was not found.";
        public NotFoundException() : base(message) { }
    }
}
