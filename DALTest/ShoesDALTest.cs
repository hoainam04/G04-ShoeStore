using System.ComponentModel;
using System;
using Xunit;
using DAL;
using Persistence;
using System.Collections.Generic;

namespace DALTest
{
    public class ShoesDALTest
    {
        private ShoesDAL dal = new ShoesDAL();
        private Shoes shoe = new Shoes();
<<<<<<< HEAD
        
=======
        // private List<Shoes> lsdal = new List<Shoes>();
>>>>>>> 24f2cf5f1bd5fb74bb32717abf9c23fe6675ab5e
        private const int GET_ALL = 0;
        private const int FILTER_BY_SHOE_NAME = 1;
        private const int FILTER_BY_BRAND_NAME = 2;

<<<<<<< HEAD
       
=======

>>>>>>> 24f2cf5f1bd5fb74bb32717abf9c23fe6675ab5e
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
<<<<<<< HEAD
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void GetbyIDTest(int shoeId)
=======
        public void GetbyIDTest(int shoeId)//,string shoeName)
>>>>>>> 24f2cf5f1bd5fb74bb32717abf9c23fe6675ab5e
        {
            Shoes result = dal.GetbyID(shoeId);
            Assert.True(result != null);
            Assert.True(result.ShoeId == shoeId);
            // Assert.True(result.ShoeName.Equals(shoeName));
        }
        [Theory]
        [InlineData("jordan 1")]
        [InlineData("Triple S")]
        [InlineData("UrBas The Gang")]
        [InlineData("GD limitted")]
        [InlineData("jordan 4")]
        [InlineData("Classic")]
        [InlineData("Old Skool")]
        [InlineData("JD Off White")]
        [InlineData("Off White 1")]
        [InlineData("PG 1")]
        [InlineData("Saigon 1980s NE")]
        [InlineData("Super Star")]
        [InlineData("Ultra Boost 20B")]
        [InlineData("EQT 91.18")]
        [InlineData("Bumper Gum Mule")]

<<<<<<< HEAD
        public void TestGetname(string name)
        {
            Shoes  shoe = new Shoes(){ ShoeName = name };
            List<Shoes> list = dal.GetShoes(ShoeFilter.FILTER_BY_SHOE_NAME,shoe);
            if (list.Count== 0){
                Assert.True(list==null);
            }else{
                Assert.True(list!=null);
                Assert.True(list.Count!=0);
                foreach(Shoes sh in list){
                    Assert.Contains(name.ToLower(),sh.ShoeName.ToLower());
                }
            }
        }
        [Theory]
        [InlineData("Nike")]
        [InlineData("Baleciaga")]
        [InlineData("vintas")]
        [InlineData("Vans")]
        [InlineData("adidas")]
        [InlineData("Converse")]
        [InlineData("basas")]
        public void TestGetBrand(string brand)
        {
            Shoes  shoe = new Shoes(){ BrandName = brand };
            List<Shoes> list = dal.GetShoes(ShoeFilter.FILTER_BY_BRAND_NAME,shoe);
            if (list.Count== 0){
                Assert.True(list==null);
            }else{
                Assert.True(list!=null);
                Assert.True(list.Count!=0);
                foreach(Shoes sh in list){
                    Assert.Contains(brand.ToLower(),sh.BrandName.ToLower());
                }
            }
=======

        public void GetbyNameTest(string shoeName)
        {
            Shoes shoe1 = new Shoes() { ShoeName = shoeName };
            // List<Shoes> result = dal.GetShoes(ShoeFilter.FILTER_BY_SHOE_NAME, shoe1);
        }
        public void GetBrandTest(string brandName)
        {
            Shoes sh = new Shoes(){BrandName  = brandName};
>>>>>>> 24f2cf5f1bd5fb74bb32717abf9c23fe6675ab5e
        }
    }
}
