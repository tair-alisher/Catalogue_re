namespace Catalogue_re.BLL.Infrastructure
{
    public class OperationDetails
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }

        public OperationDetails(bool success, string message, string property)
        {
            Success = success;
            Message = message;
            Property = property;
        }
    }
}
