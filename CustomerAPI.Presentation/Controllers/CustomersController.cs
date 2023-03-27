using Microsoft.AspNetCore.Mvc; 
using MediatR;
using Marvin.Cache.Headers;

using Shared.DTO;
using Application.Features.Customers.Queries;
using Application.Features.Customers.Commands;
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
    /// Gets the specified customer 
    /// </summary> 
    /// <param name="id"></param>
    /// <returns>The customer</returns>
    [HttpGet("{id:int}", Name = "CustomerById")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)] 
    [HttpCacheValidation(MustRevalidate = false)]
    public async Task<IActionResult> GetCustomer(int id) 
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
    // [ServiceFilter(typeof(ValidationFilterAttribute))] 
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreationDto customerForCreationDto) 
    { 
        if (customerForCreationDto is null) 
            return BadRequest("CustomerForCreationDto object is null"); 
            
        var response = await _sender.Send(new CreateCustomerCommand(customerForCreationDto)); 
        
        return Ok(response); 
    }

    /// <summary> 
    /// Updates a customer 
    /// </summary> 
    /// <param name="id"></param>
    /// <param name="customerForUpdateDto"></param>
    /// <returns>An updated customer</returns> 
    /// <response code="200">Returns the updated item</response> 
    /// <response code="400">If the item is null</response> 
    /// <response code="422">If the model is invalid</response> 
    [HttpPut("{id:int}")] 
    [ProducesResponseType(200)]
    [ProducesResponseType(400)] 
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCustomer(int id, CustomerForUpdateDto customerForUpdateDto)
    { 
        if (customerForUpdateDto is null) 
            return BadRequest("CustomerForUpdateDto object is null"); 
        
        var response = await _sender.Send(new UpdateCustomerCommand(id, customerForUpdateDto, TrackChanges: true)); 
        
        return Ok(response); 
    }

    /// <summary> 
    /// Delete the specified customer 
    /// </summary> 
    /// <param name="id"></param>
    /// <returns>Nothing</returns> 
    /// <response code="200">Nothing</response> 
    /// <response code="400">If the id is null</response> 
    [HttpDelete("{id:int}")] 
    [ProducesResponseType(200)]
    [ProducesResponseType(400)] 
    public async Task<IActionResult> DeleteCustomer(int id) 
    { 
        var response = await _sender.Send(new DeleteCustomerCommand(id, TrackChanges: false)); 
        // await _publisher.Publish(new CustomerDeletedNotification(id, TrackChanges: false));

        return Ok(response); 
    }
}

