using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public static class InvoiceStatus
    {
        public const int CREATE_NEW_INVOICE = 1;
        public const int COMPLETE_NEW_INVOICE = 2;
        public const int UNPAID = 3;
    }
    public class Invoice
    {
        public int InvoiceNo { set; get; }
        public DateTime InvoiceDate { get; set; }
        public Customer Customer { get; set; }
        public int? Status { get; set; }
        public List<Shoes> ShoesList { get; set; }

        public Shoes this[int index]
        {
            get
            {
                if (ShoesList == null || ShoesList.Count == 0 || index < 0 || ShoesList.Count < index) return null;
                return ShoesList[index];
            }
            set
            {
                if (ShoesList == null) ShoesList = new List<Shoes>();
                ShoesList.Add(value);
            }
        }

        public Invoice()
        {
            ShoesList = new List<Shoes>();
        }

        public override bool Equals(object obj)
        {
            if (obj is Invoice)
            {
                return ((Invoice)obj).InvoiceNo.Equals(InvoiceNo);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return InvoiceNo.GetHashCode();
        }
    }
}

