namespace Persistence
{
    public class Shoes
    {
        public int ShoeId { set; get; }
        public string ShoeName { set; get; }
        public string ShoePrice { set; get; }
        public string Brand { set; get; }
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
}