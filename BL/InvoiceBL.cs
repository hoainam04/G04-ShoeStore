using System;
using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class InvoiceBL
    {
        private InvoiceDAL odl = new InvoiceDAL();
        public bool CreateOrder(Invoice order)
        {
            bool result = odl.CreateOrder(order);
            return result;
        }
    }
}