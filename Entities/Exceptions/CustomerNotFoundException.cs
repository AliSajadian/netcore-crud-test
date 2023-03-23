namespace Entities.Exceptions;

public sealed class CustomerNotFoundException : NotFoundException 
{ 
    public CustomerNotFoundException(Guid CustomerId) :
        base ($"The Customer with id: {CustomerId} doesn't exist in the database.") 
    { 

    } 
}