using System;
// using System.Linq;
// using System.Threading.Tasks;

namespace Persistence
{
    public class Customer
    {
        public int? CustomerId { set; get; }
        public string CustomerName { set; get; }
        public int CustomerPhone { set; get; }
        public string CustomerAddress { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is Customer)
            {
                return ((Customer)obj).CustomerId.Equals(CustomerId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }
    }
}