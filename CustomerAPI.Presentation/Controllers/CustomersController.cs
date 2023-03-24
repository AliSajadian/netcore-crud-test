using Microsoft.AspNetCore.Mvc; 
using MediatR;
using Marvin.Cache.Headers;

using Shared.DTO;
using Application.Customers.Queries;
using Application.Customers.Commands;
using Application.Notifications;
using CustomerAPI.Presentation.ModelBinders;
using CustomerAPI.Presentation.Filters.ActionFilters;

namespace CustomerAPI.Presentation.Controllers;


[Route("api/customers")] 
[ApiController] 
public class CustomersController : ControllerBase 
{ 
    private readonly ISender _sender; 
    private readonly IPublisher _publisher;

    public CustomersController(ISender sender, IPublisher publisher) 
    { 
        _sender = sender; 
        _publisher = publisher; 
    }

    /// <summary> 
    /// Gets the list of all customers 
    /// </summary> 
    /// <returns>The customers list</returns>
    [HttpGet] 
    public async Task<IActionResult> GetCustomers() 
    { 
        var customers = await _sender.Send(new GetCustomersQuery(TrackChanges: false)); 
        return Ok(customers); 
    }

    /// <summary> 
    /// Gets the list of a collection of customers 
    /// </summary> 
    /// <param name="ids"></param>
    /// <returns>The customers collection</returns>
    /// <response code="400">If the item is null</response>
    // [HttpGet("collection/({ids})", Name = "CustomerCollection")]
    // public async Task<IActionResult> GetCustomerCollection (
    //     [ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids) 
    // { 
    //     var customers = await _sender.Send(new GetCustomerCollectionQuery(ids, TrackChanges: false)); 
    //     return Ok(customers); 
    // }

    /// <summary> 
    /// Gets the specified customer 
    /// </summary> 
    /// <param name="id"></param>
    /// <returns>The customer</returns>
    [HttpGet("{id:guid}", Name = "CustomerById")]
    // [ResponseCache(CacheProfileName = "120SecondsDuration")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)] 
    [HttpCacheValidation(MustRevalidate = false)]
    public async Task<IActionResult> GetCustomer(Guid id) 
    { 
        var customer = await _sender.Send(new GetCustomerQuery(id, TrackChanges: false));
        return Ok(customer); 
    }

    /// <summary> 
    /// Creates a newly created customer 
    /// </summary> 
    /// <param name="customerForCreationDto"></param>
    /// <returns>A newly created customer</returns> 
    /// <response code="201">Returns the newly created item</response> 
    /// <response code="400">If the item is null</response> 
    /// <response code="422">If the model is invalid</response> 
    [HttpPost(Name = "CreateCustomer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)] 
    [ProducesResponseType(422)]
    // [ServiceFilter(typeof(ValidationFilterAttribute))] 
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreationDto customerForCreationDto) 
    { 
        if (customerForCreationDto is null) 
            return BadRequest("CustomerForCreationDto object is null"); 
            
        var customer = await _sender.Send(new CreateCustomerCommand(customerForCreationDto)); 
        
        return CreatedAtRoute("CustomerById", new { id = customer.Id }, customer); 
    }

    /// <summary> 
    /// Creates a collection of created customers 
    /// </summary> 
    /// <param name="customerCollection"></param>
    /// <returns>The collection of created customers</returns> 
    /// <response code="201">Returns collection of created item</response> 
    /// <response code="400">If the item is null</response> 
    /// <response code="422">If the model is invalid</response> 
    // [HttpPost("collection")] 
    // [ProducesResponseType(201)]
    // [ProducesResponseType(400)] 
    // [ProducesResponseType(422)]
    // public async Task<IActionResult> CreateCustomerCollection (
    //     [FromBody] IEnumerable<CustomerForCreationDto> customerCollection) 
    // { 
    //     var result = await _sender.Send(new CreateCustomerCollectionCommand(customerCollection));
    //     return CreatedAtRoute("CustomerCollection", new { result.ids }, result.customers); 
    // }

    /// <summary> 
    /// Updates a customer 
    /// </summary> 
    /// <param name="id"></param>
    /// <param name="customerForUpdateDto"></param>
    /// <returns>An updated customer</returns> 
    /// <response code="200">Returns the updated item</response> 
    /// <response code="400">If the item is null</response> 
    /// <response code="422">If the model is invalid</response> 
    [HttpPut("{id:guid}")] 
    [ProducesResponseType(200)]
    [ProducesResponseType(400)] 
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCustomer(Guid id, CustomerForUpdateDto customerForUpdateDto)
    { 
        if (customerForUpdateDto is null) 
            return BadRequest("CustomerForUpdateDto object is null"); 
        
        await _sender.Send(new UpdateCustomerCommand(id, customerForUpdateDto, TrackChanges: true)); 
        
        return NoContent(); 
    }

    /// <summary> 
    /// Delete the specified customer 
    /// </summary> 
    /// <param name="id"></param>
    /// <returns>Nothing</returns> 
    /// <response code="200">Nothing</response> 
    /// <response code="400">If the id is null</response> 
    [HttpDelete("{id:guid}")] 
    [ProducesResponseType(200)]
    [ProducesResponseType(400)] 
    public async Task<IActionResult> DeleteCustomer(Guid id) 
    { 
        //await _sender.Send(new DeleteCustomerCommand(id, TrackChanges: false)); 
        await _publisher.Publish(new CustomerDeletedNotification(id, TrackChanges: false));

        return NoContent(); 
    }
}

