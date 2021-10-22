namespace Persistence
{
    public class Shoes
    {
        public int? ShoeId { set; get; }
        public string ShoeName { set; get; }
        public double ShoePrice { set; get; }
        public string BrandName { set; get; }
        public int? ShoeQuantity { set; get; }
        public string ShoeDesception { set; get; }
        public SizesColors sc = new SizesColors();
       
        public override bool Equals(object obj)
        {
            if (obj is Shoes)
            {
                return ((Shoes)obj).ShoeId.Equals(ShoeId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ShoeId.GetHashCode();
        }

    }
    public class SizesColors
    {
        public int SizeId { set; get; }
        public int SizeNumber { set; get; }
        public int ColorId { set; get; }
        public string ColorName { set; get; }
        public int Quantity { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is SizesColors)
            {
                return ((SizesColors)obj).ColorId.Equals(ColorId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ColorId.GetHashCode();
        }
    }

    // public class ShoeDetails
    // {
    //     public Shoes ShoeId { set; get; }
    //     public Sizes SizeId { set; get; }
    //     public Colors ColorId { set; get; }
    //     public int Quantity { set; get; }
    // }
}