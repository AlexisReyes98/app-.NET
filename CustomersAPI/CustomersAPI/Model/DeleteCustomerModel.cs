using CustomersAPI.Repositories;

namespace CustomersAPI.Model
{
    public interface IDeleteCustomerModel
    {
        Task<bool> Execute(long id);
    }
    public class DeleteCustomerModel : IDeleteCustomerModel
    {
        private readonly CustomersDatabaseContext _customerDatabaseContext;

        public DeleteCustomerModel(CustomersDatabaseContext customerDatabaseContext)
        {
            _customerDatabaseContext = customerDatabaseContext;
        }

        public async Task<bool> Execute(long id)
        {
            var entity = await _customerDatabaseContext.Get(id);

            if (entity == null)
                return false;

            await _customerDatabaseContext.Delete(entity);
            return true;
        }
    }
}
