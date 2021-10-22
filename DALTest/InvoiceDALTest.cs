using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DAL;
using Persistence;
namespace DALTest
{
    public class InvoiceDALTest
    {
        private InvoiceDAL dal = new InvoiceDAL();
        private Invoice invoice = new Invoice();

        public void TestCreateInvoice(int shoeId,int colorId, int sizeId, int Amount)
        {
            
        }

    }
}