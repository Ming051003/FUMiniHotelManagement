using BusinessObjects.Models;
using DataAccessLayer.DTO;

namespace Services.Interface
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerById(int id);
        Task<Customer?> GetCustomerByEmail(string email);
        Task<Customer?> CheckLogin(string email, string password);
        Task<bool> UpdateProfile(Customer customer);
        Task AddCustomer(CustomerDTO customer);
        Task DeleteCustomer(int id);
        Task UpdateCustomer(CustomerDTO customer);
        List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate);
        int CountCustomers();
    }
}
