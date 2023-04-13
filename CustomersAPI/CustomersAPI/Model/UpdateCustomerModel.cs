using CustomersAPI.Repositories;

namespace CustomersAPI.Model
{
    public interface IUpdateCustomerModel
    {
        Task<Dto.CustomerDto?> Execute(Dto.CustomerDto customer);
    }
    public class UpdateCustomerModel : IUpdateCustomerModel
    {
        private readonly CustomersDatabaseContext _customerDatabaseContext;

        public UpdateCustomerModel(CustomersDatabaseContext customerDatabaseContext)
        {
            _customerDatabaseContext = customerDatabaseContext;
        }

        public async Task<Dto.CustomerDto?> Execute(Dto.CustomerDto customer)
        {
            var entity = await _customerDatabaseContext.Get(customer.Id);

            if (entity == null)
                return null;

            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;

            await _customerDatabaseContext.Actualizar(entity);
            return entity.ToDto();
        }
    }
}
