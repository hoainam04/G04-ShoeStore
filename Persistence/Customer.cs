namespace Persistence
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }

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