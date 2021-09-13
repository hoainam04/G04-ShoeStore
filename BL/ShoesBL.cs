using System.Collections.Generic;
using Persistence;
using DAL;
namespace BL
{
    public class ShoesBL
    {
        private ShoesDAL dal = new ShoesDAL();
        public Shoes SearchById(int shoeId)
        {
            return dal.GetbyID(shoeId);
        }
        public List<Shoes> GetAll()
        {
            return dal.GetShoes(ShoeFilter.GET_ALL, null);
        }
        public List<Shoes> SearchByName(string shoeName)
        {
            return dal.GetShoes(ShoeFilter.FILTER_BY_SHOE_NAME, new Shoes { ShoeName = shoeName });
        }
        
        public List<Shoes> SearchByBrand(string brandName)
        {
            return dal.GetShoes(ShoeFilter.FILTER_BY_BRAND_NAME, new Shoes { BrandName = brandName });
        }
    }
}