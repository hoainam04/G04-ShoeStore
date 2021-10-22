using System;
using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class InvoiceBL
    {
        private InvoiceDAL odl = new InvoiceDAL();
        public bool CreateInvoice(Invoice invoice)
        {
            bool result = odl.CreateInvoice(invoice);
            return result;
        }
        public Invoice PrintInvoice(Invoice invoice)
        {
            return odl.GetInvoice(invoice);
        }

        public Invoice PrintInvoiceNo(int invoiceNo)
        {
            return odl.GetInvoiceNo(invoiceNo);
        }

    }
}