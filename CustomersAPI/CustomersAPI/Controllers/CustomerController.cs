using Microsoft.AspNetCore.Mvc;
using CustomersAPI.Dto;
using CustomersAPI.Repositories;
using CustomersAPI.Model;

namespace CustomersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomersDatabaseContext _customerDatabaseContext;
        private readonly IUpdateCustomerModel _updateCustomerModel;
        private readonly IDeleteCustomerModel _deleteCustomerModel;

        public CustomerController(CustomersDatabaseContext customerDatabaseContext,
            IUpdateCustomerModel updateCustomerUseCase, IDeleteCustomerModel deleteCustomerModel)
        {
            _customerDatabaseContext = customerDatabaseContext;
            _updateCustomerModel = updateCustomerUseCase;
            _deleteCustomerModel = deleteCustomerModel;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomers()
        {
            var result = _customerDatabaseContext.Customers
                .Select(c => c.ToDto()).ToList();

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(long id)
        {
            CustomerEntity result = await _customerDatabaseContext.Get(id);

            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var result = await _deleteCustomerModel.Execute(id);

            if (result == false)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await _customerDatabaseContext.Add(customer);

            return new CreatedResult($"https://localhost:7150/api/customer/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await _updateCustomerModel.Execute(customer);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}
