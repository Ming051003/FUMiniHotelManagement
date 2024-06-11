using BusinessObjects;
using BusinessObjects.Models;
using DataAccessLayer.DTO;

namespace Repositories.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerById(int id);
        Task<Customer?> GetCustomerByEmail(string email);
        Task<bool> UpdateCustomer(Customer customer);
        Task AddCustomer(CustomerDTO customer);
        Task DeleteCustomer(int id);
        Task UpdateCustomer(CustomerDTO customer);
        List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate);
        int CountCustomers();
    }
}
