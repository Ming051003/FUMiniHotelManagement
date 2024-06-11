
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

    public static async Task UpdateCustomer(CustomerDTO customer)
    {
        using var db = new FuminiHotelManagementContext();
        var existingCustomer = await db.Customers.FindAsync(customer.CustomerId);
        if (existingCustomer != null)
        {
            existingCustomer.CustomerFullName = customer.CustomerFullName;
            existingCustomer.Telephone = customer.Telephone;
            existingCustomer.EmailAddress = customer.EmailAddress;
            existingCustomer.CustomerBirthday = customer.CustomerBirthday;
            existingCustomer.CustomerStatus = customer.CustomerStatus;
            existingCustomer.Password = customer.Password;

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

    public static async Task AddCustomer(CustomerDTO customer)
    {
        using var db = new FuminiHotelManagementContext();
        var newCustomer = new Customer
        {
            CustomerFullName = customer.CustomerFullName,
            Telephone = customer.Telephone,
            EmailAddress = customer.EmailAddress,
            CustomerBirthday = customer.CustomerBirthday,
            CustomerStatus = customer.CustomerStatus,
            Password = customer.Password
        };

        db.Customers.Add(newCustomer);
        await db.SaveChangesAsync();
    }
}
