namespace Persistence
{
    public class Shoes
    {
        public int ShoeId { set; get; }
        public string ShoeName { set; get; }
        public string ShoePrice { set; get; }
        public string BrandName { set; get; }
        public string ShoeQuanlity { set; get; }
        public string ShoeDesception { set; get; }

    }
    public class Sizes
    {
        public int SizeId { set; get; }
        public string SizeName { set; get; }
    }
    public class Colors
    {
        public int ColorId { set; get; }
        public string ColorName { set; get; }
    }
    public class ShoeDetails{ 
        public Shoes ShoeId { set; get; }
        public Sizes SizeId { set; get; }
        public Colors ColorId { set; get; }
        public int Quanlity { set; get; }
    }
}