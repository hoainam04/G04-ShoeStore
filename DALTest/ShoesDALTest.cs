using System.ComponentModel;
using System;
using Xunit;
using DAL;
using Persistence;

namespace DALTest
{
    public class ShoesDALTest
    {
        private ShoesDAL dal = new ShoesDAL();
        private Shoes shoe = new Shoes();
        // private List<Shoes> lsdal = new List<Shoes>();
        private const int GET_ALL = 0;
        private const int FILTER_BY_SHOE_NAME = 1;
        private const int FILTER_BY_BRAND_NAME = 2;


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GetbyIDTest(int shoeId)//,string shoeName)
        {
            Shoes result = dal.GetbyID(shoeId);
            Assert.True(result != null);
            Assert.True(result.ShoeId == shoeId);
            // Assert.True(result.ShoeName.Equals(shoeName));
        }


        public void GetbyNameTest(string shoeName)
        {
            Shoes shoe1 = new Shoes() { ShoeName = shoeName };
            // List<Shoes> result = dal.GetShoes(ShoeFilter.FILTER_BY_SHOE_NAME, shoe1);
        }
        public void GetBrandTest(string brandName)
        {
            Shoes sh = new Shoes(){BrandName  = brandName};
        }
    }
}