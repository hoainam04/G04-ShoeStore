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
        private const int GET_ALL = 0;
        private const int FILTER_BY_SHOE_NAME = 1;
        private const int FILTER_BY_BRAND_NAME = 2;

        
        [Theory]
        [InlineData(1, "Jordan 1")]
        // [InlineData("trandat", "trandat123", 2)]
        public void GetbyIDTest(int shoeId,string shoeName)
        {
            Shoes  result = dal.GetbyID(shoeId);
            Assert.True(result != null);
            Assert.True(result.ShoeId == shoeId);
            Assert.True(result.ShoeName.Equals(shoeName));
        }

    }
}