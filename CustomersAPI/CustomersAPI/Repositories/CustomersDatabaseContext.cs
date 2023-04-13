using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CustomersAPI.Dto;

namespace CustomersAPI.Repositories
{
    public class CustomersDatabaseContext : DbContext
    {
        public CustomersDatabaseContext(DbContextOptions<CustomersDatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<CustomerEntity> Customers { get; set; }

        public async Task<CustomerEntity?> Get(long id)
        {
            return await Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(CustomerEntity customerEntity)
        {
            Customers.Remove(customerEntity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address,
            };

            EntityEntry<CustomerEntity> response = await Customers.AddAsync(entity);
            await SaveChangesAsync();

            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar"));
        }

        public async Task<bool> Actualizar(CustomerEntity customerEntity)
        {
            Customers.Update(customerEntity);
            await SaveChangesAsync();

            return true;
        }
    }

    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                Id = Id ?? throw new Exception("El id no puede ser null"),
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Email = Email,
                Address = Address
            };
        }
    }
}
