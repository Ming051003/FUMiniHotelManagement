
using BusinessObjects.Models;
using DataAccessLayer.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DAO;

public class CustomerDAO
{
    public static async Task<Customer?> GetCustomerById(int id)
    {
        using var db = new FuminiHotelManagementContext();
        return await db.Customers.FirstOrDefaultAsync(c => c.CustomerId.Equals(id));
    }

    public static async Task<Customer?> GetCustomerByEmail(string email)
    {
        using var db = new FuminiHotelManagementContext();
        return await db.Customers.FirstOrDefaultAsync(c => c.EmailAddress.Equals(email));
    }

    public static List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate)
    {
        using var db = new FuminiHotelManagementContext();
        return db.Customers
            .Where(predicate)
            .Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                CustomerFullName = c.CustomerFullName,
                Telephone = c.Telephone,
                EmailAddress = c.EmailAddress,
                CustomerBirthday = c.CustomerBirthday,
                CustomerStatus = c.CustomerStatus,
                Password = c.Password,
            })
            .ToList();
    }

    public static int CountCustomers()
    {
        using var db = new FuminiHotelManagementContext();
        return db.Customers
            .Where(c => c.CustomerStatus == 1)
            .Count();
    }

    public static async Task<bool> UpdateCustomer(Customer customer)
    {
        using var db = new FuminiHotelManagementContext();
        db.Customers.Update(customer);
        var success = await db.SaveChangesAsync();
        return success == 1 ? true : false;
    }

    public static async Task UpdateCustomer(CustomerDTO customerDTO)
    {
        using var db = new FuminiHotelManagementContext();
        var existingCustomer = await db.Customers.FindAsync(customerDTO.CustomerId);
        if (existingCustomer != null)
        {
            existingCustomer.CustomerFullName = customerDTO.CustomerFullName;
            existingCustomer.Telephone = customerDTO.Telephone;
            existingCustomer.EmailAddress = customerDTO.EmailAddress;
            existingCustomer.CustomerBirthday = customerDTO.CustomerBirthday;
            existingCustomer.CustomerStatus = customerDTO.CustomerStatus;
            existingCustomer.Password = customerDTO.Password;

            db.Customers.Update(existingCustomer);
            await db.SaveChangesAsync();
        }
    }

    public static async Task DeleteCustomer(int customerId)
    {
        using var db = new FuminiHotelManagementContext();
        var customer = await db.Customers.FindAsync(customerId);
        if (customer != null)
        {
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
        }
    }

    public static async Task AddCustomer(CustomerDTO customerDTO)
    {
        using var db = new FuminiHotelManagementContext();
        var newCustomer = new Customer
        {
            CustomerFullName = customerDTO.CustomerFullName,
            Telephone = customerDTO.Telephone,
            EmailAddress = customerDTO.EmailAddress,
            CustomerBirthday = customerDTO.CustomerBirthday,
            CustomerStatus = customerDTO.CustomerStatus,
            Password = customerDTO.Password
        };

        db.Customers.Add(newCustomer);
        await db.SaveChangesAsync();
    }
}
