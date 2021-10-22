using System;
using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL cdal = new CustomerDAL();
        public Customer GetCustomerById(int customerId)
        {
            return cdal.GetCustomerById(customerId);
        }
        public Customer GetCustomerByPhone(int customerPhone){
            return cdal.GetCustomerByPhone(customerPhone);
        }

        public int AddCustomer(Customer customer)
        {
            return cdal.AddCustomer(customer) ?? 0;
        }
    }
}