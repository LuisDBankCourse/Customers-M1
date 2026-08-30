namespace Customer.Domain.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(Guid customerId)
         :base($"El cliente con el id{customerId} no existe") { }
    }

    public class DuplicateDocumentNumberException : Exception
    {
        public DuplicateDocumentNumberException(string documentNumber)
         : base($"El cliente con el numero de documento {documentNumber} ya existe") { }
    }
}
